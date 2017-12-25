namespace gameserver.realm.world
{
    public class Beachzone : World
    {
        public Beachzone()
        {
            Name = "Beachzone";
            ClientWorldName = "dungeons.Beachzone";
            Background = 0;
            Difficulty = 0;
            ShowDisplays = true;
            AllowTeleport = false;
        }

        protected override void Init()
        {
            LoadMap("beachzone", MapType.Wmap);
        }
    }
}