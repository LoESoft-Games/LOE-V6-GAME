#region

using System.IO;

#endregion

namespace appengine.app.inGameNews
{
    internal class getNews : RequestHandler
    {
        protected override void HandleRequest() => WriteLine(File.ReadAllText("app/inGameNews/getNews.json"), false);
    }
}