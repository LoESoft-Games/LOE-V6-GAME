#region

using core;

#endregion

namespace appengine.account
{
    // guid (possibly more)
    // no parameters -> list index out of range
    internal class sendVerifyEmail : RequestHandler
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