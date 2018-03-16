#region

using System;
using gameserver.networking.outgoing;
using gameserver.realm;
using gameserver.realm.entity;

#endregion

namespace gameserver.logic.behaviors
{
    public class Shoot : CycleBehavior
    {
        //State storage: cooldown timer

        protected readonly double angleOffset;
        protected readonly int coolDownOffset;
        protected readonly int count;
        protected readonly double predictive;
        protected readonly int projectileIndex;
        protected readonly double radius;
        protected readonly double shootAngle;
        protected float? fixedAngle;
        protected Cooldown coolDown;
        protected double? defaultAngle;
        protected bool rotateEffect;
        protected readonly int rotateRadius;
        protected readonly uint rotateColor;
        protected double? rotateAngle;

        public Shoot(
            double radius = 8,
            int count = 1,
            double? shootAngle = null,
            int projectileIndex = 0,
            double? fixedAngle = null,
            double angleOffset = 0,
            double? defaultAngle = null,
            double predictive = 0,
            int coolDownOffset = 0,
            Cooldown coolDown = new Cooldown(),
            bool rotateEffect = false,
            int rotateRadius = 2,
            uint rotateColor = 0xFFFFFF,
            double? rotateAngle = null)
        {
            this.radius = radius;
            this.count = count;
            this.projectileIndex = projectileIndex;
            this.predictive = predictive;
            this.coolDownOffset = coolDownOffset;
            this.coolDown = coolDown.Normalize();
            this.rotateEffect = rotateEffect;
            this.rotateRadius = rotateRadius;
            this.rotateColor = rotateColor;
            this.shootAngle = count == 1 ? 0 : (shootAngle ?? 360.0 / count) * Math.PI / 180;
            this.fixedAngle = (float?)(fixedAngle * Math.PI / 180);
            this.angleOffset = (float)(angleOffset * Math.PI / 180);
            this.defaultAngle = (float?)(defaultAngle * Math.PI / 180);
            this.rotateAngle = (float?)(rotateAngle * Math.PI / 180);
        }

        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            state = coolDownOffset;
        }

        private static Position EnemyShootHistory(Entity host)
        {
            Position? history = host.TryGetHistory(1);

            if (history == null)
                return new Position { X = host.X, Y = host.Y };

            return new Position { X = history.Value.X, Y = history.Value.Y };
        }

        private static double Predict(Entity host, Entity target, ProjectileDesc desc)
        {
            Position? history = target.TryGetHistory(1);
            if (history == null)
                return 0;

            double originalAngle = Math.Atan2(history.Value.Y - host.Y, history.Value.X - host.X);
            double newAngle = Math.Atan2(target.Y - host.Y, target.X - host.X);


            float bulletSpeed = desc.Speed / 100;
            double angularVelo = (newAngle - originalAngle) / (100 / 1000f);
            return angularVelo * bulletSpeed;
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
            int cool = (int?)state ?? -1;
            Status = CycleStatus.NotStarted;

            if (cool <= 0)
            {
                if (host.HasConditionEffect(ConditionEffectIndex.Stunned)) return;
                int count = this.count;
                if (host.HasConditionEffect(ConditionEffectIndex.Dazed))
                    count = Math.Max(1, count / 2);

                Entity player = host.GetNearestEntity(radius, null);
                if (player != null || defaultAngle != null || fixedAngle != null)
                {
                    ProjectileDesc desc = host.ObjectDesc.Projectiles[projectileIndex];

                    double a = fixedAngle ??
                               (player == null ? defaultAngle.Value : Math.Atan2(player.Y - host.Y, player.X - host.X));
                    a += angleOffset;
                    if (predictive != 0 && player != null)
                        a += Predict(host, player, desc) * predictive;

                    int dmg;
                    if (host is Character)
                        dmg = (host as Character).Random.Next(desc.MinDamage, desc.MaxDamage);
                    else
                        dmg = Random.Next(desc.MinDamage, desc.MaxDamage);

                    double startAngle = a - shootAngle * (count - 1) / 2;
                    byte prjId = 0;
                    Position prjPos = EnemyShootHistory(host);
                    for (int i = 0; i < count; i++)
                    {
                        Projectile prj = host.CreateProjectile(
                            desc, host.ObjectType, dmg, time.TotalElapsedMs,
                            prjPos, (float)(startAngle + shootAngle * i));
                        host.Owner.EnterWorld(prj);
                        if (i == 0)
                            prjId = prj.ProjectileId;
                    }
                    if (rotateEffect)
                    {
                        Position target;
                        if (rotateAngle != null)
                            target = new Position
                            {
                                X = host.X + (float)(Math.Cos(rotateAngle.Value)),
                                Y = host.Y + (float)(Math.Sin(rotateAngle.Value)),
                            };
                        else
                            target = new Position
                            {
                                X = player.X,
                                Y = player.Y,
                            };

                        host.Owner.BroadcastPacket(new SHOWEFFECT
                        {
                            EffectType = EffectType.Coneblast,
                            Color = new ARGB(rotateColor),
                            TargetId = host.Id,
                            PosA = target,
                            PosB = new Position { X = rotateRadius }, //radius
                        }, null);
                    }

                    host.Owner.BroadcastPacket(new ENEMYSHOOT
                    {
                        BulletId = prjId,
                        OwnerId = host.Id,
                        Position = prjPos,
                        Angle = (float)startAngle,
                        Damage = (short)dmg,
                        BulletType = (byte)desc.BulletType,
                        AngleInc = (float)shootAngle,
                        NumShots = (byte)count,
                    }, null);
                }
                cool = coolDown.Next(Random);
                Status = CycleStatus.Completed;
            }
            else
            {
                cool -= time.ElapsedMsDelta;
                Status = CycleStatus.InProgress;
            }

            state = cool;
        }
    }
}