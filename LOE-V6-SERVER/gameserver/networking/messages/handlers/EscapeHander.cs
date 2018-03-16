#region

using core.config;
using gameserver.networking.incoming;
using gameserver.networking.outgoing;
using gameserver.realm;

#endregion

namespace gameserver.networking.handlers
{
    internal class EscapeHandler : MessageHandlers<ESCAPE>
    {
        public override MessageID ID => MessageID.ESCAPE;

        protected override void HandlePacket(Client client, ESCAPE packet)
        {
            if (client.Player.Owner == null) return;
            var world = client.Manager.GetWorld(client.Player.Owner.Id);
            if (world.Id == World.NEXUS_ID)
            {
                client.SendMessage(new TEXT
                {
                    Stars = -1,
                    BubbleTime = 0,
                    Name = "",
                    Text = "server.already_nexus",
                    NameColor = 0x123456,
                    TextColor = 0x123456
                });
                return;
            }
            client.Reconnect(new RECONNECT
            {
                Host = "",
                Port = Settings.GAMESERVER.PORT,
                GameId = World.NEXUS_ID,
                Name = "Nexus",
                Key = Empty<byte>.Array,
            });
        }
    }
}