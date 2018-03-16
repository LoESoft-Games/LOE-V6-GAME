#region

using System.IO;

#endregion

namespace appengine.app
{
    internal class init : RequestHandler
    {
        protected override void HandleRequest() => WriteLine(File.ReadAllText("app/init.xml"));
    }
}