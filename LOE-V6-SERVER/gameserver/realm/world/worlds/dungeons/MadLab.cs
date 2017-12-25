namespace gameserver.realm.world
{
    public class MadLab : World
    {
        public MadLab()
        {
            Name = "Mad Lab";
            ClientWorldName = "dungeons.Mad_Lab";
            Background = 0;
            Difficulty = 5;
            AllowTeleport = true;
        }

        protected override void Init()
        {
            LoadMap("vault", MapType.Wmap);
        }
    }
}