#region

using common.config;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace appengine.app
{
    internal class getLanguageStrings : RequestHandler
    {
        private static string appengine = Settings.NETWORKING.APPENGINE_URL;

        protected Dictionary<string, string> _ = new Dictionary<string, string>
        {
            { "en", "english" },
            { "test", "test" }
        };

        protected override void HandleRequest()
        {
            string _lt = Query["languageType"];

            if (_lt == null)
                return;

            string language = null;
            string data = null;

            WebClient client = new WebClient();

            if (_.TryGetValue(_lt, out language))
                data = client.DownloadString($"{appengine}/app/language/{language}.json");
            else
                data = client.DownloadString($"{appengine}/app/language/english.json");

            string newData = Regex.Replace(data, @"\r\n?|\n", string.Empty);

            byte[] response = Encoding.UTF8.GetBytes(newData);

            Context.Response.OutputStream?.Write(response, 0, response.Length);

            client.Dispose();
        }
    }
}