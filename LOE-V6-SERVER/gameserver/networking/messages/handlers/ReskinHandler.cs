#region

using gameserver.networking.incoming;

#endregion

namespace gameserver.networking.handlers
{
    internal class ReskinHandler : MessageHandlers<RESKIN>
    {
        public override MessageID ID => MessageID.RESKIN;

        protected override void HandlePacket(Client client, RESKIN packet)
        {
            if (client.Player.Owner == null) return;

            client.Manager.Logic.AddPendingAction(t =>
            {
                if (packet.SkinId == 0)
                    client.Player.PlayerSkin = 0;
                else
                    client.Player.PlayerSkin = packet.SkinId;
                client.Player.UpdateCount++;
                client.Player.SaveToCharacter();
            });
        }
    }
}