#region

using gameserver.realm;

#endregion

namespace gameserver.logic.transitions
{
    public class NoPlayerWithinTransition : Transition
    {
        //State storage: none

        private readonly double dist;

        public NoPlayerWithinTransition(double dist, string targetState)
            : base(targetState)
        {
            this.dist = dist;
        }

        protected override bool TickCore(Entity host, RealmTime time, ref object state)
        {
            return host.GetNearestEntity(dist, objType: null) == null;
        }
    }
}