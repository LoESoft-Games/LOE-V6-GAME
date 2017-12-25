#region

using gameserver.networking;

#endregion

namespace gameserver.realm.world
{
    public class UndeadLair : World
    {
        public UndeadLair()
        {
            Name = "Undead Lair";
            ClientWorldName = "dungeons.Undead_Lair";
            Dungeon = true;
            Background = 0;
            AllowTeleport = true;
        }

        protected override void Init()
        {
            LoadMap("UDL1", MapType.Wmap);
        }

        public override World GetInstance(Client psr)
        {
            return Manager.AddWorld(new UndeadLair());
        }
    }
}