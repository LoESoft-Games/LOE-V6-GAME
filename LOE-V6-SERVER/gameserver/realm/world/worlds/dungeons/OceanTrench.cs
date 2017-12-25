#region

using gameserver.networking;

#endregion

namespace gameserver.realm.world
{
    public class OceanTrench : World
    {
        public OceanTrench()
        {
            Name = "Ocean Trench";
            ClientWorldName = "server.Ocean_Trench";
            Dungeon = true;
            Background = 0;
            AllowTeleport = true;
        }

        protected override void Init()
        {
            LoadMap("oceantrench", MapType.Wmap);
        }

        public override World GetInstance(Client psr)
        {
            return Manager.AddWorld(new OceanTrench());
        }
    }
}