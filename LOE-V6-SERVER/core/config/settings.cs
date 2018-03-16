namespace core.config
{
    public partial class Settings
    {
        public static bool IS_PRODUCTION = false;

        public static bool ENABLE_RESTART = true;

        public static int RESTART_DELAY_MINUTES = 60;

        public static string ProcessFile(string path) => $"_{path}_only.bat";

        public static class STARTUP
        {
            public static readonly int GOLD = 999999999;
            public static readonly int FAME = 999999999;
            public static readonly int TOTAL_FAME = 999999999;
            public static readonly int TOKENS = 0;
            public static readonly bool VERIFIED = true;
            public static readonly int MAX_CHAR_SLOTS = 3;
            public static readonly int IS_AGE_VERIFIED = 1;
        }
    }
}