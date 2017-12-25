#region

using gameserver.networking.incoming;
using gameserver.networking.outgoing;
using gameserver.realm;
using gameserver.realm.entity;
using gameserver.realm.entity.player;

#endregion

namespace gameserver.networking.handlers
{
    internal class PlayerShootPacketHandler : MessageHandlers<PLAYERSHOOT>
    {
        public override MessageID ID => MessageID.PLAYERSHOOT;

        protected override void HandlePacket(Client client, PLAYERSHOOT packet) => Handle(client.Player, packet);

        void Handle(Player player, PLAYERSHOOT packet)
        {
            Item item;
            if (!player.Manager.GameData.Items.TryGetValue((ushort)packet.ContainerType, out item))
                return;

            if (item == player.Inventory[1])
                return;

            // create projectile and show other players
            var prjDesc = item.Projectiles[0]; //Assume only one
            Projectile prj = player.PlayerShootProjectile(
                packet.BulletId, prjDesc, item.ObjectType,
                packet.Time, packet.Position, packet.Angle);
            player.Owner.EnterWorld(prj);
            player.BroadcastSync(new ALLYSHOOT()
            {
                OwnerId = player.Id,
                Angle = packet.Angle,
                ContainerType = packet.ContainerType,
                BulletId = packet.BulletId
            }, p => p != player && p.Dist(player) <= 12);
            player.FameCounter.Shoot(prj);
        }
    }
}