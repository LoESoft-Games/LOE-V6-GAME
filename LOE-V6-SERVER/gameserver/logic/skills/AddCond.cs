#region

using gameserver.realm;

#endregion

namespace gameserver.logic.behaviors
{
    public class AddCond : Behavior
    {
        //State storage: none

        private readonly ConditionEffectIndex effect;
        private readonly int duration;

        public AddCond(ConditionEffectIndex effect, int duration = 0)
        {
            this.effect = effect;
            this.duration = duration;
        }

        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            host.ApplyConditionEffect(new ConditionEffect
            {
                Effect = effect,
                DurationMS = -1
            });
        }

        protected override void OnStateExit(Entity host, RealmTime time, ref object state)
        {
            if (duration > 0)
            {
                host.ApplyConditionEffect(new ConditionEffect
                {
                    Effect = effect,
                    DurationMS = duration
                });
            }
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
        }
    }
}