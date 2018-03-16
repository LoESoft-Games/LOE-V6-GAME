#region

using gameserver.networking.incoming;
using gameserver.networking.outgoing;
using gameserver.realm.entity.player;

#endregion

namespace gameserver.networking.handlers
{
    internal class EditAccountListHandler : MessageHandlers<EDITACCOUNTLIST>
    {
        public override MessageID ID => MessageID.EDITACCOUNTLIST;

        protected override void HandlePacket(Client client, EDITACCOUNTLIST packet)
        {
            Player target;
            if (client.Player.Owner == null) return;
            client.Manager.Logic.AddPendingAction(t =>
            {
                target = client.Player.Owner.GetEntity(packet.ObjectId) is Player ? client.Player.Owner.GetEntity(packet.ObjectId) as Player : null;
                if (target == null) return;
                if (client.Account.AccountId == target.AccountId)
                {
                    SendFailure("You cannot do that with yourself.");
                    return;
                }
                switch (packet.AccountListId)
                {
                    case ACCOUNTLIST.LOCKED_LIST_ID:
                        client.Account.Database.LockAccount(client.Account, int.Parse(target.AccountId));
                        break;
                    case ACCOUNTLIST.IGNORED_LIST_ID:
                        client.Account.Database.IgnoreAccount(client.Account, int.Parse(target.AccountId));
                        break;
                }
            });
        }
    }
}