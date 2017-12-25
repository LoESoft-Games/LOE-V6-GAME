#region

using gameserver.networking.incoming;
using gameserver.realm;

#endregion

namespace gameserver.networking.handlers
{
    internal class CheckCreditsHandler : MessageHandlers<CHECKCREDITS>
    {
        public override MessageID ID => MessageID.CHECKCREDITS;

        protected override void HandlePacket(Client client, CHECKCREDITS packet)
        {
            client.Account.Flush();
            client.Account.Reload();
            client.Manager.Logic.AddPendingAction(t =>
            {
                client.Player.Credits = client.Player.Client.Account.Credits;
                client.Player.UpdateCount++;
            }, PendingPriority.Networking);
        }
    }
}