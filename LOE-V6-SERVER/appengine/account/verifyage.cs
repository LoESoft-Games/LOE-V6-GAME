#region

using core;

#endregion

namespace appengine.account
{
    internal class verifyage : RequestHandler
    {
        // guid, password (possibly more)
        // <Error>Error.accountNotFound</Error>
        protected override void HandleRequest()
        {
            DbAccount acc;
            if (Query["guid"] == null || Query["password"] == null)
                WriteErrorLine("Error.incorrectEmailOrPassword");
            else
            {
                LoginStatus status = Database.Verify(Query["guid"], Query["password"], out acc);
                if (status == LoginStatus.OK)
                {
                    Database.VerifyAge(acc);
                    WriteLine("<Success />");
                }
                else
                {
                    WriteErrorLine("");
                }
            }
        }
    }
}