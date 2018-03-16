#region

using core;
using System.Linq;
using gameserver.networking.incoming;
using gameserver.networking.outgoing;
using gameserver.realm.entity.player;

#endregion

namespace gameserver.networking.handlers
{
    internal class ChooseNameHandler : MessageHandlers<CHOOSENAME>
    {
        public override MessageID ID => MessageID.CHOOSENAME;

        protected override void HandlePacket(Client client, CHOOSENAME packet)
        {
            string name = packet.Name;
            if (name.Length < 3 || name.Length > 15 || !name.All(x => char.IsLetter(x) || char.IsNumber(x)))
                client.SendMessage(new NAMERESULT
                {
                    Success = false,
                    ErrorText = "Error.nameIsNotAlpha"
                });
            else
            {
                string key = Database.NAME_LOCK;
                string lockToken = null;
                try
                {
                    while ((lockToken = client.Manager.Database.AcquireLock(key)) == null) ;

                    if (client.Manager.Database.Hashes.Exists(0, "names", name.ToUpperInvariant()).Exec())
                    {
                        client.SendMessage(new NAMERESULT
                        {
                            Success = false,
                            ErrorText = "Error.nameAlreadyInUse"
                        });
                        return;
                    }

                    if (client.Account.NameChosen && client.Account.Credits < 1000)
                        client.SendMessage(new NAMERESULT
                        {
                            Success = false,
                            ErrorText = "server.not_enough_gold"
                        });
                    else
                    {
                        if (client.Account.NameChosen)
                            client.Manager.Database.UpdateCredit(client.Account, -1000);
                        while (!client.Manager.Database.RenameIGN(client.Account, name, lockToken)) ;
                        client.Player.Name = client.Account.Name;
                        client.Player.UpdateCount++;
                        client.SendMessage(new NAMERESULT
                        {
                            Success = true,
                            ErrorText = "server.buy_success"
                        });
                    }
                }
                finally
                {
                    if (lockToken != null)
                        client.Manager.Database.ReleaseLock(key, lockToken);
                }
            }
        }

        protected void Handle(Player player)
        {
            player.Credits = player.Client.Account.Credits;
            player.Name = player.Client.Account.Name;
            player.NameChosen = true;
            player.UpdateCount++;
        }
    }
}