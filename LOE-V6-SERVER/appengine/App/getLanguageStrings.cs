#region

using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

#endregion

namespace appengine.app
{
    internal class getLanguageStrings : RequestHandler
    {
        protected Dictionary<string, string> _ = new Dictionary<string, string>
        {
            { "en", "english" }
        };

        protected override void HandleRequest()
        {
            string _lt = Query["languageType"];

            if (_lt == null)
                return;

            string language = null;
            string path = null;

            if (_.TryGetValue(_lt, out language))
                path = language;
            else
                path = "english";

            WriteLine(Regex.Replace(File.ReadAllText($"app/language/{path}.json"), @"\r\n?|\n", string.Empty), false);
        }
    }
}