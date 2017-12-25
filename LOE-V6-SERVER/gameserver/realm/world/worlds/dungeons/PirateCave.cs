namespace gameserver.realm.world
{
    public class PirateCave : World
    {
        public PirateCave()
        {
            Name = "Pirate Cave";
            ClientWorldName = "dungeons.Pirate_Cave";
            Background = 0;
            Difficulty = 1;
            AllowTeleport = true;
        }

        protected override void Init()
        {
            LoadMap("pcave", MapType.Wmap);
        }
    }
}