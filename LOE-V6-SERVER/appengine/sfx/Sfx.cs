#region

using core.config;

#endregion

namespace appengine.sfx
{
    internal class Sfx : RequestHandler
    {
        protected override void HandleRequest()
        {
            string file = Context.Request.Url.LocalPath;
            string appengine = Settings.NETWORKING.APPENGINE_URL;
            if (file.StartsWith("/music") || file.StartsWith("/sfx"))
                Context.Response.Redirect(appengine + file);
        }
    }
}