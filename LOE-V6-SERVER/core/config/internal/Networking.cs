using System.Collections.Generic;

namespace core.config
{
    public partial class Settings
    {
        public static class NETWORKING
        {
            public static class RESTART
            {
                public static bool ENABLE_RESTART = Settings.ENABLE_RESTART;
                public static int RESTART_DELAY_MINUTES = Settings.RESTART_DELAY_MINUTES;
            }

            public static string BUILD_VERSION = "v6-1";
            public static string MINOR_VERSION = "0";
            public static string FULL_BUILD = BUILD_VERSION + "." + MINOR_VERSION;

            public static readonly List<string> INTERNAL_BUILD = new List<string> //internal server build (for client only) / support multiple versions c:
            {
                "1.0-129", "1.0-161", "1.0-178", "1.1"
            };

            public static string APPENGINE_URL = "https://devwarlt.github.io"; //"http://appengine.loesoft.org";
            public static int CPU_HANDLER = 4096;
            public static int MAX_CONNECTIONS = 25;
            public static bool DISABLE_NAGLES_ALGORITHM = IS_PRODUCTION;

            public static class INTERNAL
            {
                public static readonly List<string> PRODUCTION_DDNS = new List<string>{
                    "testing.loesoft.org", "localhost"
                };

                public static readonly string SELECTED_DOMAINS =
                    @"<cross-domain-policy>
                        <policy-file-request/>
                        <site-control permitted-cross-domain-policies=""master-only""/>
                        <allow-access-from domain=""devwarlt.github.io"" secure=""true""/>
                        <allow-access-from domain=""devwarlt.github.io"" to-ports=""*""/>
                        <allow-http-request-headers-from domain=""devwarlt.github.io"" headers=""*"" secure=""true""/>
                        <allow-access-from domain=""loesoft.org"" secure=""false""/>
                        <allow-access-from domain=""loesoft.org"" to-ports=""*""/>
                        <allow-http-request-headers-from domain=""loesoft.org"" headers=""*"" secure=""false""/>
                        <allow-access-from domain=""testing.loesoft.org"" secure=""false""/>
                        <allow-access-from domain=""testing.loesoft.org"" to-ports=""*""/>
                        <allow-http-request-headers-from domain=""testing.loesoft.org"" headers=""*"" secure=""false""/>
                        <allow-access-from domain=""appengine.loesoft.org"" secure=""false""/>
                        <allow-access-from domain=""appengine.loesoft.org"" to-ports=""*""/>
                        <allow-http-request-headers-from domain=""appengine.loesoft.org"" headers=""*"" secure=""false""/>
                    </cross-domain-policy>";

                public static readonly string LOCALHOST_DOMAINS =
                    @"<cross-domain-policy>
                        <policy-file-request/>
                        <allow-access-from domain=""*""/>
                    </cross-domain-policy>";
            }
        }
    }
}