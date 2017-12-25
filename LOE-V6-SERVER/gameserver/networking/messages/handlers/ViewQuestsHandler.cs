#region

using gameserver.networking.incoming;

#endregion

namespace gameserver.networking.handlers
{
    internal class ViewQuestsHandler : MessageHandlers<QUEST_FETCH_ASK>
    {
        public override MessageID ID => MessageID.QUEST_FETCH_ASK;

        protected override void HandlePacket(Client client, QUEST_FETCH_ASK packet)
        {
            return;
        }
    }
}