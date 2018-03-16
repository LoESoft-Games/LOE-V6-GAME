namespace core.config
{
    public partial class Settings
    {
        public static class GAMESERVER
        {
            public static string TITLE = "[LoESoft] GameServer";

            public static string FILE = ProcessFile("gameserver");

            public static int PORT = 2050;
            public static int TICKETS_PER_SECOND = 5;
            public static int MAX_IN_REALM = 85;
            public static int TTL = 5;
        }
    }
}