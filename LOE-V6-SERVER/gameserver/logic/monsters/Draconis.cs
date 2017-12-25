using gameserver.logic.behaviors;

namespace gameserver.logic
{
    partial class BehaviorDb
    {
        private _ LairOfDraconis = () => Behav()
            .Init("NM Black Dragon God",
                new State(
                    new Shoot(10, projectileIndex: 1, coolDown: 1000)
                )
            );
    }
}
