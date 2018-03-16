#region

using core;

#endregion

namespace appengine.account
{
    internal class initCAL : RequestHandler
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
                {
                    if (acc.Admin)
                    {
                        var ca = new DbClassAvailability(acc);
                        ca.Init(GameData);
                        ca.Flush();
                        WriteLine("<Success />");
                    }
                    else
                        WriteLine("<Failure />");
                }
                else
                    WriteErrorLine("<Failure />");
            }
        }
    }
}