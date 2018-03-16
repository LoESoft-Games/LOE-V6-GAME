#region

using gameserver.networking.incoming;
using gameserver.realm;
using gameserver.realm.entity;
using gameserver.realm.entity.player;

#endregion

namespace gameserver.networking.handlers
{
    internal class PlayerHitHandler : MessageHandlers<PLAYERHIT>
    {
        public override MessageID ID => MessageID.PLAYERHIT;

        protected override void HandlePacket(Client client, PLAYERHIT packet) => client.Manager.Logic.AddPendingAction(t => Handle(client.Player, packet));

        private void Handle(Player player, PLAYERHIT packet)
        {
            if (player == null)
                return;

            if (player.Owner == null)
                return;

            Entity enemy = player.Owner.GetEntity(packet.ObjectId);

            if (enemy == null)
                return;

            Projectile proj = (enemy as IProjectileOwner).Projectiles[packet.BulletId];

            if (proj == null)
                return;

            foreach (ConditionEffect effect in proj.ProjDesc.Effects)
            {
                if (effect.Target == 1)
                    continue;
                else
                    player.ApplyConditionEffect(effect);
            }

            player.ForceDamage(proj.Damage, proj.ProjectileOwner.Self, proj.ProjDesc.ArmorPiercing);
        }
    }
}
