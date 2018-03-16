using core;
using System;
using System.IO;


namespace appengine.guild
{
    class setBoard : RequestHandler
    {
        protected override void HandleRequest()
        {
            using (StreamWriter wtr = new StreamWriter(Context.Response.OutputStream))
            {
                DbAccount acc;
                var status = Database.Verify(Query["guid"], Query["password"], out acc);
                if (status == LoginStatus.OK)
                {
                    if (Convert.ToInt32(acc.GuildId) <= 0 || acc.GuildRank < 20)
                    {
                        wtr.Write("<Error>No permission</Error>");
                        return;
                    }

                    var guild = Database.GetGuild(Convert.ToInt32(acc.GuildId));
                    var text = Query["board"];
                    if (Database.SetGuildBoard(guild, text))
                    {
                        wtr.Write(text);
                        return;
                    }

                    wtr.Write("<Error>Failed to set board</Error>");
                }
                else
                    wtr.Write("<Error>" + status.GetInfo() + "</Error>");
            }
        }
    }
}
