#region

using gameserver.networking.incoming;
using gameserver.realm;
using gameserver.realm.entity;

#endregion

namespace gameserver.networking.handlers
{
    internal class BuyHandler : MessageHandlers<BUY>
    {
        public override MessageID ID => MessageID.BUY;

        protected override void HandlePacket(Client client, BUY packet)
        {
            client.Manager.Logic.AddPendingAction(t =>
            {
                if (client.Player.Owner == null) return;
                SellableObject obj = client.Player.Owner.GetEntity(packet.ObjectId) as SellableObject;
                if (obj != null)
                    for (int i = 0; i < packet.Quantity; i++)
                        obj.Buy(client.Player);
            }, PendingPriority.Networking);
        }
    }
}