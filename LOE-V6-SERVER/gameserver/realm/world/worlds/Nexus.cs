#region

using System.Collections.Generic;
using gameserver.realm.entity;
using gameserver.realm.entity.player;
using static gameserver.networking.Client;

#endregion

namespace gameserver.realm.world
{
    public class Nexus : World
    {
        public const string WINTER_RESOURCE = "nexus_winter";
        public const string SUMMER_RESOURCE = "nexus_summer";

        public Nexus()
        {
            Id = NEXUS_ID;
            Name = "Nexus";
            ClientWorldName = "server.nexus";
            Background = 2;
            AllowTeleport = false;
            Difficulty = -1;
            Dungeon = false;
        }

        protected override void Init()
        {
            LoadMap(SUMMER_RESOURCE, MapType.Json);
        }

        public override void Tick(RealmTime time)
        {
            base.Tick(time); //normal world tick

            CheckDupers();
            UpdatePortals();
        }

        private void CheckDupers()
        {
            foreach (KeyValuePair<int, World> w in Manager.Worlds)
            {
                foreach (KeyValuePair<int, World> x in Manager.Worlds)
                {
                    foreach (KeyValuePair<int, Player> y in w.Value.Players)
                    {
                        foreach (KeyValuePair<int, Player> z in x.Value.Players)
                        {
                            if (y.Value.AccountId == z.Value.AccountId && y.Value != z.Value)
                            {
                                y.Value.Client.Disconnect(DisconnectReason.DUPER_DISCONNECT);
                                z.Value.Client.Disconnect(DisconnectReason.DUPER_DISCONNECT);
                            }
                        }
                    }
                }
            }
        }

        private void UpdatePortals()
        {
            foreach (var i in Manager.Monitor.portals)
            {
                foreach (var j in RealmManager.CurrentRealmNames)
                {
                    if (i.Value.Name.StartsWith(j))
                    {
                        if (i.Value.Name == j) (i.Value as Portal).PortalName = i.Value.Name;
                        i.Value.Name = j + " (" + i.Key.Players.Count + "/" + RealmManager.MAX_REALM_PLAYERS + ")";
                        i.Value.UpdateCount++;
                        break;
                    }
                }
            }
        }
    }
}