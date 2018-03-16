#region

using core;

#endregion

namespace appengine.account
{
    // guid, password
    // <Error>WebChangePasswordDialog.passwordError</Error>
    // <Error>Error.invalidEmail</Error>
    internal class verify : RequestHandler
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
                    WriteLine(Account.FromDb(acc).ToXml());
                else
                    WriteErrorLine(status.GetInfo());
            }
        }
    }
}