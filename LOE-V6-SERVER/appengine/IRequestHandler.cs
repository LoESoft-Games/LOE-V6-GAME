#region

using System.Net;

#endregion

namespace appengine
{
    internal interface IRequestHandler
    {
        void HandleRequest(HttpListenerContext context);
    }
}