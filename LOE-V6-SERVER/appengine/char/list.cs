#region

using core;
using core.config;
using System;
using System.Collections.Generic;
using System.IO;

#endregion

namespace appengine.@char
{
    internal class list : RequestHandler
    {
        protected override void HandleRequest()
        {
            DbAccount acc;
            var status = Database.Verify(Query["guid"], Query["password"], out acc);
            if (status == LoginStatus.OK || status == LoginStatus.AccountNotExists)
            {
                if (status == LoginStatus.AccountNotExists)
                    acc = Database.CreateGuestAccount(Query["guid"]);
                if (acc.Banned)
                {
                    using (StreamWriter wtr = new StreamWriter(Context.Response.OutputStream))
                        wtr.WriteLine("<Error>Account under maintenance</Error>");
                    Context.Response.Close();
                }

                var ca = new DbClassAvailability(acc);
                if (ca.IsNull)
                    ca.Init(GameData);
                ca.Flush();

                var list = CharList.FromDb(Database, acc);
                list.Servers = GetServerList();
                WriteLine(list.ToXml(Program.GameData, acc));
            }
            else
                WriteErrorLine(status.GetInfo());
        }

        private Lazy<List<Settings.APPENGINE.ServerItem>> svrList { get; set; }

        public list()
        {
            svrList = new Lazy<List<Settings.APPENGINE.ServerItem>>(GetServerList, true);
        }

        private List<Settings.APPENGINE.ServerItem> GetServerList()
        {
            var ret = Settings.APPENGINE.returnServerItem();
            return ret;
        }
    }
}