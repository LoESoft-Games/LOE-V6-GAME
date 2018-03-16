#region

using System;
using System.Collections.Generic;

#endregion

namespace gameserver.realm.entity
{
    public interface IProjectileOwner
    {
        Projectile[] Projectiles { get; }
        Entity Self { get; }
    }

    public class Projectile : Entity
    {
        private readonly HashSet<Entity> hitted = new HashSet<Entity>();

        public Projectile(RealmManager manager, ProjectileDesc desc)
            : base(manager, manager.GameData.IdToObjectType[desc.ObjectId])
        {
            ProjDesc = desc;
        }

        public IProjectileOwner ProjectileOwner { get; set; }
        public new byte ProjectileId { get; set; }
        public short Container { get; set; }
        public int Damage { get; set; }

        public long BeginTime { get; set; }
        public Position BeginPos { get; set; }
        public float Angle { get; set; }

        public ProjectileDesc ProjDesc { get; set; }

        public void Destroy() => Owner?.LeaveWorld(this);

        public Position GetPosition(long elapsedTicks)
        {
            double x = BeginPos.X;
            double y = BeginPos.Y;

            double dist = (elapsedTicks / 1000.0) * (ProjDesc.Speed / 10.0);
            double period = ProjectileId % 2 == 0 ? 0 : Math.PI;
            if (ProjDesc.Wavy)
            {
                double theta = Angle + (Math.PI * 64) * Math.Sin(period + 6 * Math.PI * (elapsedTicks / 1000));
                x += dist * Math.Cos(theta);
                y += dist * Math.Sin(theta);
            }
            else if (ProjDesc.Parametric)
            {
                double theta = (double)elapsedTicks / ProjDesc.LifetimeMS * 2 * Math.PI;
                double a = Math.Sin(theta) * (ProjectileId % 2 != 0 ? 1 : -1);
                double b = Math.Sin(theta * 2) * (ProjectileId % 4 < 2 ? 1 : -1);
                double c = Math.Sin(Angle);
                double d = Math.Cos(Angle);
                x += (a * d - b * c) * ProjDesc.Magnitude;
                y += (a * c + b * d) * ProjDesc.Magnitude;
            }
            else
            {
                if (ProjDesc.Boomerang)
                {
                    double d = (ProjDesc.LifetimeMS / 1000.0) * (ProjDesc.Speed / 10.0) / 2;
                    if (dist > d)
                        dist = d - (dist - d);
                }
                x += dist * Math.Cos(Angle);
                y += dist * Math.Sin(Angle);
                if (ProjDesc.Amplitude != 0)
                {
                    double d = ProjDesc.Amplitude *
                               Math.Sin(period +
                                        (double)elapsedTicks / ProjDesc.LifetimeMS * ProjDesc.Frequency * 2 * Math.PI);
                    x += d * Math.Cos(Angle + Math.PI / 2);
                    y += d * Math.Sin(Angle + Math.PI / 2);
                }
            }
            return new Position { X = (float)x, Y = (float)y };
        }

        public override void Tick(RealmTime time)
        {
            if ((time.TotalElapsedMs - BeginTime) > ProjDesc.LifetimeMS)
            {
                Destroy();
                return;
            }

            base.Tick(time);
        }

        public void ForceHit(Entity entity, RealmTime time)
        {
            bool penetrateObsta = ProjDesc.PassesCover;
            bool penetrateEnemy = ProjDesc.MultiHit;
            Move(entity.X, entity.Y);
            if (entity.HitByProjectile(this, time))
            {
                if ((entity is Enemy && penetrateEnemy) ||
                    (entity is GameObject && (entity as GameObject).Static && !(entity is Wall) && penetrateObsta))
                    hitted.Add(entity);
                else
                    Destroy();
                ProjectileOwner.Self.ProjectileHit(this, entity);
            }
        }
    }
}