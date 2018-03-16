#region

using System;
using Mono.Game;
using gameserver.realm;

#endregion

namespace gameserver.logic.behaviors
{
    public class Orbit : CycleBehavior
    {
        //State storage: orbit state
        private readonly float acquireRange;
        private readonly float radius;
        private readonly float radiusVariance;
        private readonly float speed;
        private readonly float speedVariance;
        private readonly ushort? target;
        private readonly int _;

        public Orbit(double speed, double radius, double acquireRange = 10,
            string target = null, double? speedVariance = null, double? radiusVariance = null)
        {
            this.speed = (float)speed;
            this.radius = (float)radius;
            this.acquireRange = (float)acquireRange;
            this.target = target == null ? null : (ushort?)BehaviorDb.InitGameData.IdToObjectType[target];
            this.speedVariance = (float)(speedVariance ?? speed * 0.1);
            this.radiusVariance = (float)(radiusVariance ?? speed * 0.1);
            Random rnd = new Random();
            _ = rnd.Next(0, 1);
        }

        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            state = new OrbitState
            {
                Speed = (speed - (speed * 2.5f / (time.TickCount / (time.TotalElapsedMs / 1000f))) + speedVariance * (float)(Random.NextDouble() * 2 - 1)),
                Radius = radius + radiusVariance * (float)(Random.NextDouble() * 2 - 1)
            };
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
            OrbitState s = (OrbitState)state;

            Status = CycleStatus.NotStarted;

            if (host.HasConditionEffect(ConditionEffectIndex.Paralyzed)) return;

            Entity entity = host.GetNearestEntity(acquireRange, target);

            if (entity != null)
            {
                double angle;
                if (host.Y == entity.Y && host.X == entity.X)
                    angle = Math.Atan2(host.Y - entity.Y + (Random.NextDouble() * 2 - 1), host.X - entity.X + (Random.NextDouble() * 2 - 1));
                else
                    angle = Math.Atan2(host.Y - entity.Y, host.X - entity.X);

                float angularSpd = host.GetSpeed(s.Speed, time) / s.Radius;

                angle += angularSpd;

                double x = entity.X + (_ == 0 ? Math.Cos(angle) * radius : Math.Sin(angle) * radius);
                double y = entity.Y + (_ == 0 ? Math.Sin(angle) * radius : Math.Cos(angle) * radius);

                Vector2 vect = new Vector2((float)x, (float)y) - new Vector2(host.X, host.Y);

                vect.Normalize();
                vect *= host.GetSpeed(s.Speed, time);

                host.ValidateAndMove(host.X + vect.X, host.Y + vect.Y);
                host.UpdateCount++;

                Status = CycleStatus.InProgress;
            }

            state = s;
        }

        private class OrbitState
        {
            public float Radius;
            public float Speed;
        }
    }
}