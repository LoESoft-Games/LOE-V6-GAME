#region

using gameserver.networking.incoming;

#endregion

namespace gameserver.networking.handlers
{
    internal class GuildRemovePacketHandler : MessageHandlers<GUILDREMOVE>
    {
        public override MessageID ID => MessageID.GUILDREMOVE;

        protected override void HandlePacket(Client client, GUILDREMOVE packet)
        {
            return;
        }
    }
}