#region

using gameserver.networking.incoming;
using gameserver.realm;

#endregion

namespace gameserver.networking.handlers
{
    internal class TeleportHandler : MessageHandlers<TELEPORT>
    {
        public override MessageID ID => MessageID.TELEPORT;

        protected override void HandlePacket(Client client, TELEPORT packet)
        {
            if (client.Player.Owner == null) return;

            client.Manager.Logic.AddPendingAction(t => client.Player.Teleport(t, packet),
                PendingPriority.Networking);
        }
    }
}