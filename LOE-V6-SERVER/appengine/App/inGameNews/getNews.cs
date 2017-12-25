#region

using common.config;
using System.Net;

#endregion

namespace appengine.app.inGameNews
{
    internal class getNews : RequestHandler
    {
        protected override void HandleRequest()
        {
            WebClient client = new WebClient();
            string file = Context.Request.Url.LocalPath + ".json";
            string appengine = Settings.NETWORKING.APPENGINE_URL;
            string response = client.DownloadString(appengine + file);
            WriteLine(response, false);
            client.Dispose();
        }
    }
}