#region

using gameserver.networking.incoming;

#endregion

namespace gameserver.networking.handlers
{
    internal class PlayerTextHandler : MessageHandlers<PLAYERTEXT>
    {
        public override MessageID ID => MessageID.PLAYERTEXT;

        protected override void HandlePacket(Client client, PLAYERTEXT packet)
        {
            client.Manager.Logic.AddPendingAction(t =>
            {
                if (client.Player.Owner == null) return;

                if (packet.Text[0] == '/')
                    client.Player.Manager.Commands.Execute(client.Player, t, packet.Text);
                else
                {
                    if (client.Player.Muted)
                    {
                        client.Player.SendInfo("{\"key\":\"server.muted\"}");
                        return;
                    }
                    if (!client.Player.NameChosen)
                    {
                        client.Player.SendInfo("{\"key\":\"server.must_be_named\"}");
                        return;
                    }
                    if (!string.IsNullOrWhiteSpace(packet.Text))
                        client.Player.Manager.Chat.Say(client.Player, packet.Text);
                    else
                        client.Player.SendInfo("{\"key\":\"server.invalid_chars\"}");
                }
            });
        }
    }
}