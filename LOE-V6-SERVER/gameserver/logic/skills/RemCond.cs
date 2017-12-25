#region

using gameserver.realm;

#endregion

namespace gameserver.logic.behaviors
{
    public class RemCond : Behavior
    {
        //State storage: none

        private readonly ConditionEffectIndex effect;
        private readonly bool perm;

        public RemCond(ConditionEffectIndex effect, bool perm = false)
        {
            this.effect = effect;
            this.perm = perm;
        }

        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            host.ApplyConditionEffect(new ConditionEffect
            {
                Effect = effect,
                DurationMS = 0
            });
        }

        protected override void OnStateExit(Entity host, RealmTime time, ref object state)
        {
            if (!perm)
            {
                host.ApplyConditionEffect(new ConditionEffect
                {
                    Effect = effect,
                    DurationMS = 0
                });
            }
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
        }
    }
}