#region

using gameserver.networking.incoming;
using gameserver.networking.outgoing;
using gameserver.realm;
using core.config;

#endregion

namespace gameserver.networking.handlers
{
    internal class LeaveArenaHandler : MessageHandlers<ACCEPT_ARENA_DEATH>
    {
        public override MessageID ID => MessageID.ACCEPT_ARENA_DEATH;

        protected override void HandlePacket(Client client, ACCEPT_ARENA_DEATH packet)
        {
            if (client.Player.Owner == null) return;
            World world = client.Manager.GetWorld(client.Player.Owner.Id);
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
                Name = "nexus.Nexus",
                Key = Empty<byte>.Array,
            });
        }
    }
}