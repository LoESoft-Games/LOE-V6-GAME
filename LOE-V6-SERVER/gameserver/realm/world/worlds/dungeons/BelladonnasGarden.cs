namespace gameserver.realm.world
{
    public class BelladonnasGarden : World
    {
        public BelladonnasGarden()
        {
            Name = "Belladonna's Garden";
            ClientWorldName = "dungeons.BelladonnaAPOSs_Garden";
            Background = 0;
            AllowTeleport = false;
            Difficulty = 5;
        }

        protected override void Init()
        {
            LoadMap("belladonnasGarden", MapType.Wmap);
        }
    }
}
