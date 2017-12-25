using gameserver.networking.incoming;

namespace gameserver.networking.handlers
{
    internal class TinkerQuestHandler : MessageHandlers<QUEST_REDEEM_RESPONSE>
    {
        public override MessageID ID => MessageID.QUEST_REDEEM_RESPONSE;

        protected override void HandlePacket(Client client, QUEST_REDEEM_RESPONSE packet)
        {
            return;
        }
    }
}