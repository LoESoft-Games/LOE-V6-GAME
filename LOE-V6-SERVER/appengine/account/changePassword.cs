#region

using core;

#endregion

namespace appengine.account
{
    internal class changePassword : RequestHandler
    {
        // guid, password, newPassword
        protected override void HandleRequest()
        {
            DbAccount acc;
            if (Query["guid"] == null || Query["password"] == null || Query["newPassword"] == null)
                WriteErrorLine("Error.incorrectEmailOrPassword");
            else
            {
                LoginStatus status = Database.Verify(Query["guid"], Query["password"], out acc);
                if (status == LoginStatus.OK)
                {
                    Database.ChangePassword(Query["guid"], Query["newPassword"]);
                    WriteLine("<Success />");
                }
                else
                    WriteErrorLine(status.GetInfo());
            }
        }
    }
}