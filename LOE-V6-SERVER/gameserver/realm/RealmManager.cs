#region

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using core;
using log4net;
using gameserver.logic;
using gameserver.networking;
using gameserver.realm.commands;
using gameserver.realm.entity.player;
using gameserver.realm.world;
using core.config;
using gameserver.realm.entity.merchant;
using static gameserver.networking.Client;

#endregion

namespace gameserver.realm
{
    public enum PendingPriority
    {
        Emergent,
        Destruction,
        Networking,
        Normal,
        Creation,
    }

    public struct RealmTime
    {
        public long TickCount { get; set; }
        public long TotalElapsedMs { get; set; }
        public int TickDelta { get; set; }
        public int ElapsedMsDelta { get; set; }
    }

    public class RealmManager
    {
        public static List<string> Realms = new List<string>(44)
        {
            "Djinn",
            "Medusa",
            "Beholder",
        };
        public static List<string> CurrentRealmNames = new List<string>();
        public const int MAX_REALM_PLAYERS = 85;

        private static readonly ILog log = LogManager.GetLogger(typeof(RealmManager));

        public ConcurrentDictionary<string, Client> Clients { get; private set; }
        public ConcurrentDictionary<int, World> Worlds { get; private set; }
        public ConcurrentDictionary<string, World> LastWorld { get; private set; }

        private ConcurrentDictionary<string, Vault> vaults;

        public Random Random { get; }

        private Thread logic;
        private Thread network;
        private int nextClientId;

        private int nextWorldId;

        public RealmManager(Database db)
        {
            MaxClients = Settings.NETWORKING.MAX_CONNECTIONS;
            TPS = Settings.GAMESERVER.TICKETS_PER_SECOND;
            Clients = new ConcurrentDictionary<string, Client>();
            Worlds = new ConcurrentDictionary<int, World>();
            LastWorld = new ConcurrentDictionary<string, World>();
            vaults = new ConcurrentDictionary<string, Vault>();
            Random = new Random();
            Database = db;
        }

        public BehaviorDb Behaviors { get; private set; }

        public ChatManager Chat { get; private set; }

        public ISManager InterServer { get; private set; }

        public CommandManager Commands { get; private set; }

        public EmbeddedData GameData { get; private set; }

        public string InstanceId { get; private set; }

        public LogicTicker Logic { get; private set; }

        public int MaxClients { get; private set; }

        public RealmPortalMonitor Monitor { get; private set; }

        public NetworkTicker Network { get; private set; }

        public Database Database { get; private set; }

        public bool Terminating { get; private set; }

        public int TPS { get; private set; }

        public World AddWorld(int id, World world)
        {
            if (world.Manager != null)
                throw new InvalidOperationException("World already added.");
            world.Id = id;
            Worlds[id] = world;
            OnWorldAdded(world);
            return world;
        }

        public World AddWorld(World world)
        {
            if (world.Manager != null)
                throw new InvalidOperationException("World already added.");
            world.Id = Interlocked.Increment(ref nextWorldId);
            Worlds[world.Id] = world;
            OnWorldAdded(world);
            return world;
        }

        public void CloseWorld(World world)
        {
            Monitor.WorldRemoved(world);
        }

        public void Disconnect(Client client)
        {
            client?.Disconnect(DisconnectReason.REALM_MANAGER_DISCONNECT);
            Clients.TryRemove(client?.Id.ToString(), out client);
            client?.Dispose();
        }

        public Player FindPlayer(string name)
        {
            if (name.Split(' ').Length > 1)
                name = name.Split(' ')[1];

            return (from i in Worlds
                    where i.Key != 0
                    from e in i.Value.Players
                    where string.Equals(e.Value.Client.Account.Name, name, StringComparison.CurrentCultureIgnoreCase)
                    select e.Value).FirstOrDefault();
        }

        public Player FindPlayerRough(string name)
        {
            Player dummy;
            foreach (KeyValuePair<int, World> i in Worlds)
                if (i.Key != 0)
                    if ((dummy = i.Value.GetUniqueNamedPlayerRough(name)) != null)
                        return dummy;
            return null;
        }

        public World GetWorld(int id)
        {
            World ret;
            if (!Worlds.TryGetValue(id, out ret)) return null;
            if (ret.Id == 0) return null;
            return ret;
        }

        public void Initialize()
        {
            log.Info("Initializing Realm Manager...");

            GameData = new EmbeddedData();
            Behaviors = new BehaviorDb(this);
            Merchant.InitMerchatLists(GameData);

            AddWorld(World.NEXUS_ID, Worlds[0] = new Nexus());
            AddWorld(World.MARKET, new ClothBazaar());
            AddWorld(World.TEST_ID, new Test());
            AddWorld(World.TUT_ID, new Tutorial(true));
            AddWorld(World.DAILY_QUEST_ID, new DailyQuestRoom());
            Monitor = new RealmPortalMonitor(this);

            Task.Factory.StartNew(() => GameWorld.AutoName(1, true)).ContinueWith(_ => AddWorld(_.Result), TaskScheduler.Default);

            InterServer = new ISManager(this);

            Chat = new ChatManager(this);

            Commands = new CommandManager(this);

            log.Info("Realm Manager initialized.");
        }

        public Vault PlayerVault(Client processor)
        {
            Vault v;
            if (!vaults.TryGetValue(processor.Account.AccountId, out v))
                vaults.TryAdd(processor.Account.AccountId, v = (Vault)AddWorld(new Vault(false, processor)));
            else
                v.Reload(processor);
            return v;
        }

        public bool RemoveVault(string accountId)
        {
            Vault dummy;
            return vaults.TryRemove(accountId, out dummy);
        }

        public bool RemoveWorld(World world)
        {
            if (world.Manager == null)
                throw new InvalidOperationException("World is not added.");
            World dummy;
            if (Worlds.TryRemove(world.Id, out dummy))
            {
                try
                {
                    OnWorldRemoved(world);
                    world.Dispose();
                    GC.Collect();
                }
                catch (Exception e)
                { log.Fatal(e); }
                return true;
            }
            return false;
        }

        public void Run()
        {
            log.Info("Starting Realm Manager...");

            Logic = new LogicTicker(this);
            var logic = new Task(() => Logic.TickLoop(), TaskCreationOptions.LongRunning);
            logic.ContinueWith(Program.Stop, TaskContinuationOptions.OnlyOnFaulted);
            logic.Start();

            Network = new NetworkTicker(this);
            var network = new Task(() => Network.TickLoop(), TaskCreationOptions.LongRunning);
            network.ContinueWith(Program.Stop, TaskContinuationOptions.OnlyOnFaulted);
            network.Start();

            log.Info("Realm Manager started.");
        }

        public void Stop()
        {
            log.Info("Stopping Realm Manager...");

            Terminating = true;
            List<Client> saveAccountUnlock = new List<Client>();
            foreach (Client c in Clients.Values)
            {
                saveAccountUnlock?.Add(c);
                c?.Disconnect(DisconnectReason.STOPPING_REALM_MANAGER);
            }

            GameData?.Dispose();
            logic?.Join();
            network?.Join();

            log.Info("Realm Manager stopped.");
        }

        public Tuple<bool, ErrorIDs> TryConnect(Client client)
        {
            //UNKNOWN ErrorID is declared in normal connections to handle if client get any error during reconnec/disconnect.
            DbAccount acc = client.Account;
            //Dispatch ErrorID: when server is full.
            if (Clients.Count >= MaxClients) return Tuple.Create(false, ErrorIDs.SERVER_FULL);
            //Dispatch ErrorID: when client is banned.
            //if (acc.Banned) return Tuple.Create(false, ErrorIDs.ACCOUNT_BANNED);
            client.Id = Interlocked.Increment(ref nextClientId);
            if (Clients.ContainsKey(client.Id.ToString()))
            {
                Client c = Clients[client.Id.ToString()];
                if (c != null)
                {
                    Disconnect(Clients[client.Id.ToString()]);
                    //Dispatch ErrorID: normal connection with reconnect type.
                    return Tuple.Create(Clients.TryAdd(client.Id.ToString(), client), ErrorIDs.UNKNOWN);
                }
                //Dispatch ErrorID: user drop connection to reconnect again.
                return Tuple.Create(false, ErrorIDs.LOST_CONNECTION);
            }
            //Dispatch ErrorID: normal connection.
            return Tuple.Create(Clients.TryAdd(client.Id.ToString(), client), ErrorIDs.UNKNOWN);
        }

        private void OnWorldAdded(World world)
        {
            if (world.Manager == null)
                world.Manager = this;
            if (world is GameWorld)
                Monitor.WorldAdded(world);
            log.InfoFormat("World {0}({1}) added.", world.Id, world.Name);
        }

        private void OnWorldRemoved(World world)
        {
            world.Manager = null;
            if (world is GameWorld)
                Monitor.WorldRemoved(world);
            log.InfoFormat("World {0}({1}) removed.", world.Id, world.Name);
        }
    }

    public class TimeEventArgs : EventArgs
    {
        public TimeEventArgs(RealmTime time)
        {
            Time = time;
        }

        public RealmTime Time { get; private set; }
    }
}