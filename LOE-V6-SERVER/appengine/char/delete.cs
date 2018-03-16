#region

using core;

#endregion

namespace appengine.@char
{
    internal class delete : RequestHandler
    {
        // guid, password, charId
        protected override void HandleRequest()
        {
            DbAccount acc;
            var status = Database.Verify(Query["guid"], Query["password"], out acc);
            if (status == LoginStatus.OK)
            {
                using (var l = Database.Lock(acc))
                    if (Database.LockOk(l))
                    {
                        Database.DeleteCharacter(acc, int.Parse(Query["charId"]));
                        WriteLine("<Success />");
                    }
                    else
                        WriteErrorLine("Account in use");
            }
            else
                WriteErrorLine(status.GetInfo());
        }
    }
}