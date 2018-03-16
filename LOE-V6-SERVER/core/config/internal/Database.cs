namespace core.config
{
    public partial class Settings
    {
        public static class REDIS_DATABASE
        {
            public static string HOST = "localhost";
            public static int PORT = 6379;
            public static int IO_TIMEOUT = -1;
            public static string PASSWORD = "";
            public static int MAX_UNSENT = int.MaxValue;
            public static bool ALLOW_ADMIN = false;
            public static int SYNC_TIMEOUT = 120000;
        }
    }
}