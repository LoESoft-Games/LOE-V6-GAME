namespace gameserver.realm.world
{
    public class SpriteWorld : World
    {
        public SpriteWorld()
        {
            Name = "Sprite World";
            ClientWorldName = "dungeons.Sprite_World";
            Background = 0;
            Difficulty = 2;
            AllowTeleport = true;
        }

        protected override void Init()
        {
            LoadMap("spriteworld", MapType.Wmap);
        }
    }
}