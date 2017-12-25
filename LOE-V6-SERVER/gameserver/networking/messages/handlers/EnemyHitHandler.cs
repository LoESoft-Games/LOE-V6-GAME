#region

using gameserver.networking.incoming;
using gameserver.realm;
using gameserver.realm.entity;
using gameserver.realm.entity.player;

#endregion

namespace gameserver.networking.handlers
{
    internal class EnemyHitHandler : MessageHandlers<ENEMYHIT>
    {
        public override MessageID ID => MessageID.ENEMYHIT;

        protected override void HandlePacket(Client client, ENEMYHIT packet) => client.Manager.Logic.AddPendingAction(t => Handle(client.Player, t, packet));

        void Handle(Player player, RealmTime time, ENEMYHIT pkt)
        {
            Entity entity = player?.Owner?.GetEntity(pkt.TargetId);

            if (entity?.Owner == null)
                return;

            Projectile prj = (player as IProjectileOwner).Projectiles[pkt.BulletId];

            prj?.ForceHit(entity, time);

            if (pkt.Killed)
                player.ClientKilledEntity.Enqueue(entity);
        }
    }
}