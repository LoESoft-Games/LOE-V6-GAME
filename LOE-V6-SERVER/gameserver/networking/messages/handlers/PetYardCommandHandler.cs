#region

using gameserver.networking.incoming;

#endregion

namespace gameserver.networking.handlers
{
    internal class PetYardCommandHandler : MessageHandlers<PETUPGRADEREQUEST>
    {
        public override MessageID ID => MessageID.PETUPGRADEREQUEST;

        protected override void HandlePacket(Client client, PETUPGRADEREQUEST packet)
        {
            return;
        }
    }
}