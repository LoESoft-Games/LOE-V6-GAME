#region

using Mono.Game;
using gameserver.realm;
using gameserver.realm.entity.player;

#endregion

namespace gameserver.logic.behaviors
{
    public class StayBack : CycleBehavior
    {
        //State storage: cooldown timer

        private readonly float distance;
        private readonly float speed;

        public StayBack(double speed, double distance = 8)
        {
            this.speed = (float)speed;
            this.distance = (float)distance;
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
            int cooldown;
            if (state == null) cooldown = 1000;
            else cooldown = (int)state;

            Status = CycleStatus.NotStarted;

            if (host.HasConditionEffect(ConditionEffectIndex.Paralyzed)) return;

            Player player = (Player)host.GetNearestEntity(distance, null);
            if (player != null)
            {
                Vector2 vect;
                vect = new Vector2(player.X - host.X, player.Y - host.Y);
                vect.Normalize();
                float dist = host.GetSpeed(speed - (speed * 2.5f / (time.TickCount / (time.TotalElapsedMs / 1000f))), time);
                host.ValidateAndMove(host.X + (-vect.X) * (dist - (dist * 2.5f / (time.TickCount / (time.TotalElapsedMs / 1000f)))), host.Y + (-vect.Y) * (dist - (dist * 2.5f / (time.TickCount / (time.TotalElapsedMs / 1000f)))));
                host.UpdateCount++;

                if (cooldown <= 0)
                {
                    Status = CycleStatus.Completed;
                    cooldown = 1000;
                }
                else
                {
                    Status = CycleStatus.InProgress;
                    cooldown -= time.ElapsedMsDelta;
                }
            }

            state = cooldown;
        }
    }
}