#region

using gameserver.networking;

#endregion

namespace gameserver.realm.world
{
    public class OryxCastle : World
    {
        public OryxCastle()
        {
            Name = "Oryx's Castle";
            ClientWorldName = "server.Oryx's_Castle";
            Background = 0;
            AllowTeleport = false;
        }

        public override bool NeedsPortalKey => true;

        protected override void Init()
        {
            LoadMap("OryxCastle", MapType.Wmap);
        }

        public override World GetInstance(Client psr)
        {
            return Manager.AddWorld(new OryxCastle());
        }
    }
}