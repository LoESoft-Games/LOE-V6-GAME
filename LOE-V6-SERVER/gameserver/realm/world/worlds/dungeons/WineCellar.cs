#region

using gameserver.networking;

#endregion

namespace gameserver.realm.world
{
    public class WineCellar : World
    {
        public WineCellar()
        {
            Name = "Wine Cellar";
            ClientWorldName = "server.wine_cellar";
            Background = 0;
            AllowTeleport = false;
            Dungeon = true;
        }

        protected override void Init()
        {
            LoadMap("winecellar", MapType.Wmap);
        }

        public override World GetInstance(Client psr)
        {
            return Manager.AddWorld(new WineCellar());
        }
    }
}