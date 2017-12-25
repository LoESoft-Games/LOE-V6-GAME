#region

using gameserver.networking.incoming;

#endregion

namespace gameserver.networking.handlers
{
    internal class AOEAckHandler : MessageHandlers<AOEACK>
    {
        public override MessageID ID => MessageID.AOEACK;

        protected override void HandlePacket(Client client, AOEACK packet)
        {
            return;
        }
    }
}