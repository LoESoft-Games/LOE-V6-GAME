namespace core.config
{
    public partial class Settings
    {
        public static class APPENGINE_MONITOR
        {
            private static int MAIN_VERSION = 1;
            private static int MINOR_VERSION = 0;

            public static int PORT = 3001;

            public static string BUILD_VERSION = $"{MAIN_VERSION}.{MINOR_VERSION}";

            public static string TITLE = $"[LoESoft] AppEngine Monitor v{MAIN_VERSION}.{MINOR_VERSION}";

            public static string TOKEN = "97C0109A73A5DB5C";
        }
    }
}