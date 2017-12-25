#region

using gameserver.networking.incoming;

#endregion

namespace gameserver.networking.handlers
{
    internal class CreateGuildHandler : MessageHandlers<CREATEGUILD>
    {
        public override MessageID ID => MessageID.CREATEGUILD;

        protected override void HandlePacket(Client client, CREATEGUILD packet)
        {
            return;
        }
    }
}