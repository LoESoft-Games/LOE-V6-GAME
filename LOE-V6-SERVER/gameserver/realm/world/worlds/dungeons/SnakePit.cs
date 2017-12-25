#region

using gameserver.networking;

#endregion

namespace gameserver.realm.world
{
    public class SnakePit : World
    {
        public SnakePit()
        {
            Name = "Snake Pit";
            ClientWorldName = "dungeons.Snake_Pit";
            Dungeon = true;
            Background = 0;
            AllowTeleport = true;
        }

        protected override void Init()
        {
            LoadMap("snakepit", MapType.Wmap);
        }

        public override World GetInstance(Client client)
        {
            return Manager.AddWorld(new SnakePit());
        }
    }
}
