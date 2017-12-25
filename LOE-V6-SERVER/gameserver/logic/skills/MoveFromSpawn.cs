using Mono.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wServer.realm;
using wServer.realm.entities;


namespace wServer.logic.behaviors
{
    public class MoveFromSpawn : CycleBehavior
    {
        private readonly float speed;
        private readonly bool instant;
        private readonly float baseX;
        private readonly float baseY;
        private bool once;
        private bool returned;
        private float X;
        private float Y;

        public MoveFromSpawn(float x, float y, double speed = 2, bool once = false, bool instant = false)
        {
            this.speed = (float)speed;
            this.baseX = x;
            this.baseY = y;
            this.once = once;
            this.instant = instant;
        }

        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            Position pos = (host as Enemy).SpawnPoint;
            {
                X = baseX + pos.X;
                Y = baseY + pos.Y;
            }

            if (instant)
            {
                host.Move(X, Y);
                host.UpdateCount++;
            }
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
            if (instant) return;
            if (!returned)
            {
                if (host.HasConditionEffect(ConditionEffectIndex.Paralyzed)) return;
                var spd = host.GetSpeed(speed) * (time.thisTickTimes / 1000f);

                if (Math.Abs(X - host.X) > 0.5 || Math.Abs(Y - host.Y) > 0.5)
                {
                    Vector2 vect = new Vector2(X, Y) - new Vector2(host.X, host.Y);
                    vect.Normalize();
                    vect *= spd;
                    host.Move(host.X + vect.X, host.Y + vect.Y);
                    host.UpdateCount++;

                    if (host.X == X && host.Y == Y && once)
                    {
                        once = true;
                        returned = true;
                    }
                }
            }
        }
    }
}
