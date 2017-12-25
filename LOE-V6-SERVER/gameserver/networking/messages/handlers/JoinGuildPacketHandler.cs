#region

using gameserver.networking.incoming;

#endregion

namespace gameserver.networking.handlers
{
    internal class JoinGuildPacketHandler : MessageHandlers<JOINGUILD>
    {
        public override MessageID ID => MessageID.JOINGUILD;

        protected override void HandlePacket(Client client, JOINGUILD packet)
        {
            return;
        }
    }
}