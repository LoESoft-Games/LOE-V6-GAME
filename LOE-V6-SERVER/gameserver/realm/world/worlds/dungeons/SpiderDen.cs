namespace gameserver.realm.world
{
    public class SpiderDen : World
    {
        public SpiderDen()
        {
            Name = "Spider Den";
            ClientWorldName = "dungeons.Spider_Den";
            Background = 0;
            Difficulty = 2;
            AllowTeleport = true;
        }

        protected override void Init()
        {
            LoadMap("spiderden", MapType.Wmap);
        }
    }
}