#region

using core;
using System;
using System.IO;

#endregion

namespace appengine.guild
{
    class listMembers : RequestHandler
    {
        protected override void HandleRequest()
        {
            using (StreamWriter wtr = new StreamWriter(Context.Response.OutputStream))
            {
                DbAccount acc;
                var status = Database.Verify(Query["guid"], Query["password"], out acc);
                if (status == LoginStatus.OK)
                {
                    if (Convert.ToInt32(acc.GuildId) <= 0)
                    {
                        wtr.Write("<Error>Not in guild</Error>");
                        return;
                    }

                    var guild = Database.GetGuild(Convert.ToInt32(acc.GuildId));
                    wtr.Write(Guild.FromDb(Database, guild).ToXml().ToString());
                }
                else
                    wtr.Write("<Error>" + status.GetInfo() + "</Error>");
            }
        }
    }
}
