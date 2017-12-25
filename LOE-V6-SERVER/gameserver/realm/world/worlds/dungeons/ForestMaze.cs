namespace gameserver.realm.world
{
    public class ForestMaze : World
    {
        public ForestMaze()
        {
            Name = "Forest Maze";
            ClientWorldName = "dungeons.Forest_Maze";
            Background = 0;
            Difficulty = 1;
            AllowTeleport = true;
        }

        protected override void Init()
        {
            LoadMap("forestmaze", MapType.Wmap);
        }
    }
}