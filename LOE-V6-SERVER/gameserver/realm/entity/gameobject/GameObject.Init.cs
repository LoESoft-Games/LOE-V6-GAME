#region

using gameserver.networking.outgoing;
using gameserver.realm.entity.player;

#endregion

namespace gameserver.realm.entity
{
    public partial class GameObject : Entity
    {
        //Stats
        public GameObject(RealmManager manager, ushort objType, int? lifeTime, bool stat, bool dying, bool hittestable)
            : base(manager, objType, IsInteractive(manager, objType))
        {
            if (Vulnerable = lifeTime.HasValue)
                HP = lifeTime.Value;
            Dying = dying;
            if (objType == 0x01c7)
                Static = true;
            else
                Static = stat;
            Hittestable = hittestable;
        }

        public override bool HitByProjectile(Projectile projectile, RealmTime time)
        {
            if ((Static && !ObjectDesc.Enemy) || !Vulnerable) return false;
            if (HasConditionEffect(ConditionEffectIndex.Invincible))
                return false;
            if (projectile.ProjectileOwner is Player &&
                !HasConditionEffect(ConditionEffectIndex.Paused) &&
                !HasConditionEffect(ConditionEffectIndex.Stasis))
            {
                int def = ObjectDesc.Defense;
                if (projectile.ProjDesc.ArmorPiercing)
                    def = 0;
                int dmg = (int)StatsManager.GetDefenseDamage(this, projectile.Damage, def);
                if (!HasConditionEffect(ConditionEffectIndex.Invulnerable))
                    HP -= dmg;
                foreach (ConditionEffect effect in projectile.ProjDesc.Effects)
                {
                    if (effect.Effect == ConditionEffectIndex.Stunned && ObjectDesc.StunImmune ||
                        effect.Effect == ConditionEffectIndex.Paralyzed && ObjectDesc.ParalyzedImmune ||
                        effect.Effect == ConditionEffectIndex.Dazed && ObjectDesc.DazedImmune) continue;
                    ApplyConditionEffect(effect);
                }
                Owner.BroadcastPacket(new DAMAGE
                {
                    TargetId = Id,
                    Effects = projectile.ConditionEffects,
                    Damage = (ushort)dmg,
                    Killed = HP <= 0 || dmg >= HP,
                    BulletId = projectile.ProjectileId,
                    ObjectId = projectile.ProjectileOwner.Self.Id
                }, projectile.ProjectileOwner as Player);

                if (HP <= 0 || dmg >= HP)
                {
                    HP = 0;
                    Owner.LeaveWorld(this);
                }

                UpdateCount++;
                return true;
            }
            return false;
        }

        public override void Tick(RealmTime time)
        {
            if (Vulnerable)
            {
                if (Dying)
                {
                    HP -= time.ElapsedMsDelta;
                    UpdateCount++;
                }
                CheckHP();
            }

            base.Tick(time);
        }
    }
}