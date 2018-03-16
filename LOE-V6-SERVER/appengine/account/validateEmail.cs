#region

using core;

#endregion

namespace appengine.account
{
    // no longer on prod webserver?
    internal class validateEmail : RequestHandler
    {
        protected override void HandleRequest()
        {
            DbAccount acc;
            if (Query["guid"] == null || Query["password"] == null)
                WriteErrorLine("Error.incorrectEmailOrPassword");
            else
            {
                LoginStatus status = Database.Verify(Query["guid"], Query["password"], out acc);
                if (status == LoginStatus.OK)
                    WriteErrorLine("Not Implemented Exception");
                else
                    WriteErrorLine(status.GetInfo());
            }
        }
    }
}