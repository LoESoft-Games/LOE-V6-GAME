#region

using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using log4net;
using System.Collections.Generic;
using gameserver.realm.entity.player;

#endregion

namespace gameserver.realm
{
    public class LogicTicker
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LogicTicker));
        public static RealmTime CurrentTime;
        private readonly ConcurrentQueue<Action<RealmTime>>[] pendings;

        public int MsPT;
        public int TPS;

        public LogicTicker(RealmManager manager)
        {
            Manager = manager;
            pendings = new ConcurrentQueue<Action<RealmTime>>[5];
            for (int i = 0; i < 5; i++)
                pendings[i] = new ConcurrentQueue<Action<RealmTime>>();

            TPS = manager.TPS;
            MsPT = 1000 / TPS;
        }

        public RealmManager Manager { get; private set; }

        public void AddPendingAction(Action<RealmTime> callback) => AddPendingAction(callback, PendingPriority.Normal);

        public void AddPendingAction(Action<RealmTime> callback, PendingPriority priority) => pendings[(int)priority].Enqueue(callback);

        public void TickLoop()
        {
            log.Info("Logic loop started.");
            Stopwatch watch = new Stopwatch();
            long dt = 0;
            long count = 0;

            watch.Start();
            RealmTime t = new RealmTime();
            do
            {
                if (Manager.Terminating) break;

                long times = dt / MsPT;
                dt -= times * MsPT;
                times++;

                long b = watch.ElapsedMilliseconds;

                count += times;
                if (times > 3)
                    log.Warn("LAGGED!| time:" + times + " dt:" + dt + " count:" + count + " time:" + b + " tps:" +
                             count / (b / 1000.0));

                t.TotalElapsedMs = b;
                t.TickCount = count;
                t.TickDelta = (int)times;
                t.ElapsedMsDelta = (int)(times * MsPT);

                foreach (ConcurrentQueue<Action<RealmTime>> i in pendings)
                {
                    Action<RealmTime> callback;
                    while (i.TryDequeue(out callback))
                    {
                        try
                        {
                            callback(t);
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex);
                        }
                    }
                }
                TickWorlds1(t);
                Manager.InterServer.Tick(t);

                Player[] tradingPlayers = TradeManager.TradingPlayers.Where(_ => _.Owner == null).ToArray();
                foreach (var player in tradingPlayers)
                    TradeManager.TradingPlayers.Remove(player);

                KeyValuePair<Player, Player>[] requestPlayers = TradeManager.CurrentRequests.Where(_ => _.Key.Owner == null || _.Value.Owner == null).ToArray();
                foreach (var players in requestPlayers)
                    TradeManager.CurrentRequests.Remove(players);

                Thread.Sleep(MsPT);

                dt += Math.Max(0, watch.ElapsedMilliseconds - b - MsPT);
            } while (true);
            log.Info("Logic loop stopped.");
        }

        private void TickWorlds1(RealmTime t) //Continous simulation
        {
            CurrentTime = t;
            foreach (World i in Manager.Worlds.Values.Distinct())
                i.Tick(t);
        }
    }
}