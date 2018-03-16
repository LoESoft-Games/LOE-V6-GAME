#region

using gameserver.realm;

#endregion

namespace gameserver.logic.behaviors
{
    public class BackAndForth : CycleBehavior
    {
        //State storage: remaining distance

        private readonly int distance;
        private readonly float speed;

        public BackAndForth(double speed, int distance = 5)
        {
            this.speed = (float)speed;
            this.distance = distance;
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
            float dist;
            if (state == null) dist = distance;
            else dist = (float)state;

            Status = CycleStatus.NotStarted;

            if (host.HasConditionEffect(ConditionEffectIndex.Paralyzed)) return;

            float moveDist = host.GetSpeed(speed - (speed * 2.5f / (time.TickCount / (time.TotalElapsedMs / 1000f))), time);
            if (dist > 0)
            {
                Status = CycleStatus.InProgress;
                host.ValidateAndMove(host.X + moveDist, host.Y);
                host.UpdateCount++;
                dist -= moveDist;
                if (dist <= 0)
                {
                    dist = -distance;
                    Status = CycleStatus.Completed;
                }
            }
            else
            {
                Status = CycleStatus.InProgress;
                host.ValidateAndMove(host.X - moveDist, host.Y);
                host.UpdateCount++;
                dist += moveDist;
                if (dist >= 0)
                {
                    dist = distance;
                    Status = CycleStatus.Completed;
                }
            }

            state = dist;
        }
    }
}