#region

using System.Collections.Generic;
using gameserver.realm.entity.player;
using static gameserver.networking.Client;

#endregion

namespace gameserver.realm.world
{
    public class Test : World
    {
        public string js = null;

        public Test()
        {
            Id = TEST_ID;
            Name = "Test";
            Background = 0;
            Dungeon = true;
        }

        public void LoadJson(string json)
        {
            js = json;
            LoadMap(json);
        }

        public override void Tick(RealmTime time)
        {
            base.Tick(time);

            foreach (KeyValuePair<int, Player> i in Players)
            {
                if (i.Value.Client.Account.AccType != core.config.AccountType.ULTIMATE_ACCOUNT || !i.Value.Client.Account.Admin)
                {
                    i.Value.SendError(string.Format("[Admin: {0}] You cannot access Test world with rank {1}.", i.Value.Client.Account.Admin ? "true" : "false", i.Value.Client.Account.Rank));
                    i.Value.Client.Disconnect(DisconnectReason.ACCESS_DENIED);
                }
            }
        }

        protected override void Init() { }
    }
}