#region

using System.IO;
using System.Text.RegularExpressions;

#endregion

namespace appengine.app
{
    internal class globalNews : RequestHandler
    {
        protected override void HandleRequest() => WriteLine(Regex.Replace(File.ReadAllText("app/globalNews/globalNews.json"), @"\r\n?|\n", string.Empty), false);
    }
}