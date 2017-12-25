namespace gameserver.realm.world
{
    public class DailyQuestRoom : World
    {
        public DailyQuestRoom()
        {
            Name = "Daily Quest Room";
            ClientWorldName = "nexus.Daily_Quest_Room";
            Background = 0;
            AllowTeleport = false;
            Difficulty = -1;
        }

        protected override void Init()
        {
            LoadMap("dailyQuest", MapType.Wmap);
        }
    }
}