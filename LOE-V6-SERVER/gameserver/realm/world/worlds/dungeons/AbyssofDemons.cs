#region

using gameserver.networking;

#endregion

namespace gameserver.realm.world
{
    public class AbyssofDemons : World
    {
        public AbyssofDemons()
        {
            Name = "Abyss of Demons";
            ClientWorldName = "dungeons.Abyss_of_Demons";
            Dungeon = true;
            Background = 0;
            AllowTeleport = true;
        }

        protected override void Init()
        {
            LoadMap("abyss", MapType.Wmap);
        }

        public override World GetInstance(Client psr) => Manager.AddWorld(new AbyssofDemons());
    }
}