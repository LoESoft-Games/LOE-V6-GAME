#region

using gameserver.networking.incoming;

#endregion

namespace gameserver.networking.handlers
{
    internal class GuildInvitePacketHandler : MessageHandlers<GUILDINVITE>
    {
        public override MessageID ID => MessageID.GUILDINVITE;

        protected override void HandlePacket(Client client, GUILDINVITE packet)
        {
            return;
        }
    }
}