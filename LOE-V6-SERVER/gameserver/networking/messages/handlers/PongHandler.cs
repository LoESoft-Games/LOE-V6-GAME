#region

using gameserver.networking.incoming;

#endregion

namespace gameserver.networking.handlers
{
    internal class PongHandler : MessageHandlers<PONG>
    {
        public override MessageID ID => MessageID.PONG;

        protected override void HandlePacket(Client client, PONG packet) => client.Player.Pong(packet.Time, packet);
    }
}