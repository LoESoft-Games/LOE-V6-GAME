#region

using common.config;
using System.Text;

#endregion

namespace appengine
{
    internal class crossdomain : RequestHandler
    {
        protected override void HandleRequest()
        {
            byte[] status = Encoding.UTF8.GetBytes(Settings.IS_PRODUCTION ? Settings.NETWORKING.INTERNAL.SELECTED_DOMAINS : Settings.NETWORKING.INTERNAL.LOCALHOST_DOMAINS);
            Context.Response.ContentType = "text/*";
            Context.Response.OutputStream.Write(status, 0, status.Length);
        }
    }
}