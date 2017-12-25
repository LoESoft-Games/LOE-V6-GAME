#region

using common.config;
using System.Net;

#endregion

namespace appengine.app
{
    internal class globalNews : RequestHandler
    {
        protected override void HandleRequest()
        {
            WebClient client = new WebClient();
            string file = Context.Request.Url.LocalPath + "/globalNews.json";
            string appengine = Settings.NETWORKING.APPENGINE_URL;
            string response = client.DownloadString(appengine + file);
            WriteLine(response, false);
            client.Dispose();
        }
    }
}