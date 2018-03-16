#region

using System;
using gameserver.networking.outgoing;
using gameserver.realm;
using gameserver.realm.entity.player;

#endregion

namespace gameserver.logic.behaviors
{
    public class Grenade : Behavior
    {
        //State storage: cooldown timer

        private readonly int damage;
        private readonly float radius;
        private readonly double range;
        private Cooldown coolDown;
        private double? fixedAngle;
        private readonly uint color;
        private ConditionEffectIndex effect;
        private int effectDuration;

        public Grenade(double radius, int damage, double range = 5,
            double? fixedAngle = null, Cooldown coolDown = new Cooldown(), uint color = 0xFF0000, ConditionEffectIndex effect = ConditionEffectIndex.Hidden, int effectDuration = -1)
        {
            this.radius = (float)radius;
            this.damage = damage;
            this.range = range;
            this.fixedAngle = fixedAngle * Math.PI / 180;
            this.coolDown = coolDown.Normalize();
            this.color = color;
            this.effect = effect;
            this.effectDuration = effectDuration;
        }

        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            state = 0;
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
            int cool = (int)state;

            if (cool <= 0)
            {
                if (host.HasConditionEffect(ConditionEffectIndex.Stunned)) return;

                Entity player = host.GetNearestEntity(range, null);
                if (player != null || fixedAngle != null)
                {
                    Position target;
                    if (fixedAngle != null)
                        target = new Position
                        {
                            X = host.X + (float)(range * Math.Cos(fixedAngle.Value)),
                            Y = host.Y + (float)(range * Math.Sin(fixedAngle.Value)),
                        };
                    else
                        target = new Position
                        {
                            X = player.X,
                            Y = player.Y,
                        };
                    host.Owner.BroadcastPacket(new SHOWEFFECT
                    {
                        EffectType = EffectType.Throw,
                        Color = new ARGB(color),
                        TargetId = host.Id,
                        PosA = target
                    }, null);
                    host.Owner.Timers.Add(new WorldTimer(1500, (world, t) =>
                    {
                        world.BroadcastPacket(new AOE
                        {
                            Position = target,
                            Radius = radius,
                            Damage = (ushort)damage,
                            EffectDuration = 0,
                            Effects = 0,
                            OriginType = (short)host.ObjectType,
                            Color = new ARGB(color)
                        }, null);
                        world.Aoe(target, radius, true, p =>
                        {
                            if (effect != ConditionEffectIndex.Hidden && effectDuration != -1)
                                (p as Player).ApplyConditionEffect(new ConditionEffect
                                {
                                    Effect = effect,
                                    DurationMS = effectDuration
                                });
                            (p as IPlayer).Damage(damage, host, false);
                        });
                    }));
                }
                cool = coolDown.Next(Random);
            }
            else
                cool -= time.ElapsedMsDelta;

            state = cool;
        }
    }
}