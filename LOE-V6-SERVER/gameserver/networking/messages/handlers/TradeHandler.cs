#region

using gameserver.networking.incoming;

#endregion

namespace gameserver.networking.handlers
{
    internal class RequestTradeHandler : MessageHandlers<REQUESTTRADE>
    {
        public override MessageID ID => MessageID.REQUESTTRADE;

        protected override void HandlePacket(Client client, REQUESTTRADE packet) => client.Manager.Logic.AddPendingAction(t => client.Player.RequestTrade(t, packet));
    }

    internal class ChangeTradeHandler : MessageHandlers<CHANGETRADE>
    {
        public override MessageID ID => MessageID.CHANGETRADE;

        protected override void HandlePacket(Client client, CHANGETRADE packet)
        {
            client.Manager.Logic.AddPendingAction(t => client.Player.ChangeTrade(t, packet));
        }
    }

    internal class AcceptTradeHandler : MessageHandlers<ACCEPTTRADE>
    {
        public override MessageID ID => MessageID.ACCEPTTRADE;

        protected override void HandlePacket(Client client, ACCEPTTRADE packet)
        {
            client.Manager.Logic.AddPendingAction(t => client.Player.AcceptTrade(t, packet));
        }
    }

    internal class CancelTradeHandler : MessageHandlers<CANCELTRADE>
    {
        public override MessageID ID => MessageID.CANCELTRADE;

        protected override void HandlePacket(Client client, CANCELTRADE packet)
        {
            client.Manager.Logic.AddPendingAction(t => client.Player.CancelTrade(t, packet));
        }
    }
}