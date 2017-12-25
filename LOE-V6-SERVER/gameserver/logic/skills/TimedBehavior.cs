#region

using wServer.realm;

#endregion

namespace wServer.logic.behaviors
{
    public class TimedBehavior : CycleBehavior
    {

        private readonly Behavior behavior;
        private readonly int period;

        public TimedBehavior(int period, Behavior behavior)
        {
            this.behavior = behavior;
            this.period = period;
        }

        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            state = period;
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
            int period = (int)state;

            behavior.Tick(host, time);
            Status = CycleStatus.InProgress;

            period -= time.thisTickTimes;
            if (period <= 0)
            {
                behavior.OnStateEntry(host, time);
                period = 10000000;
                Status = CycleStatus.Completed;
            }

            state = period;
        }
    }
}