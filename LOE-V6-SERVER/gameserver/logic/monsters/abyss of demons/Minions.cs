using gameserver.logic.behaviors;
using gameserver.logic.transitions;

namespace gameserver.logic
{
    partial class BehaviorDb
    {
        /// <summary>
        /// Abyss of Demons [Minions]
        /// Production version made by Devwarlt
        /// 15th September 2017 19:01
        /// </summary>
        private _ Abyss_of_Demons_Minions = () => Behav()

        #region "Imp of the Abyss"
        .Init("Imp of the Abyss",
            new State(
                new State("main",
                    new AddCond(ConditionEffectIndex.Invincible),
                    new PlayerWithinTransition(12, "begin")
                    ),
                new State("begin",
                    new RemCond(ConditionEffectIndex.Invincible),
                    new Wander(12),
                    new Shoot(radius: 21, count: 5, shootAngle: 10, coolDown: 1250),
                    new NoPlayerWithinTransition(18, "nothing")
                    ),
                new State("nothing",
                    new AddCond(ConditionEffectIndex.Invincible),
                    new PlayerWithinTransition(12, "begin")
                    )
                )
            )
        #endregion "Imp of the Abyss"

        #region "Brute Warrior of the Abyss"
        .Init("Brute Warrior of the Abyss",
            new State(
                new State("main",
                    new AddCond(ConditionEffectIndex.Invincible),
                    new PlayerWithinTransition(12, "begin")
                    ),
                new State("begin",
                    new RemCond(ConditionEffectIndex.Invincible),
                    new Chase(speed: 16, acquireRange: 10, range: 1),
                    new Wander(speed: 4),
                    new Shoot(radius: 4.8, count: 3, shootAngle: 10, coolDown: 750),
                    new NoPlayerWithinTransition(18, "nothing")
                    ),
                new State("nothing",
                    new AddCond(ConditionEffectIndex.Invincible),
                    new PlayerWithinTransition(12, "begin")
                    )
                )
            )
        #endregion "Brute Warrior of the Abyss"

        #region "Brute of the Abyss"
        .Init("Brute of the Abyss",
            new State(
                new State("main",
                    new AddCond(ConditionEffectIndex.Invincible),
                    new PlayerWithinTransition(12, "begin")
                    ),
                new State("begin",
                    new RemCond(ConditionEffectIndex.Invincible),
                    new Chase(speed: 16, acquireRange: 10, range: 1),
                    new Wander(speed: 4),
                    new Shoot(radius: 4.8, count: 3, shootAngle: 10, coolDown: 750),
                    new NoPlayerWithinTransition(18, "nothing")
                    ),
                new State("nothing",
                    new AddCond(ConditionEffectIndex.Invincible),
                    new PlayerWithinTransition(12, "begin")
                    )
                )
            )
        #endregion "Brute of the Abyss"

        #region "Demon Mage of the Abyss"
        .Init("Demon Mage of the Abyss",
            new State(
                new State("main",
                    new AddCond(ConditionEffectIndex.Invincible),
                    new PlayerWithinTransition(12, "begin")
                    ),
                new State("begin",
                    new RemCond(ConditionEffectIndex.Invincible),
                    new Chase(speed: 16, acquireRange: 10, range: 5),
                    new Wander(speed: 4),
                    new Shoot(radius: 24, count: 3, shootAngle: 10, coolDown: 1250),
                    new NoPlayerWithinTransition(18, "nothing")
                    ),
                new State("nothing",
                    new AddCond(ConditionEffectIndex.Invincible),
                    new PlayerWithinTransition(12, "begin")
                    )
                )
            )
        #endregion "Demon Mage of the Abyss"

        #region "Demon Warrior of the Abyss"
        .Init("Demon Warrior of the Abyss",
            new State(
                new State("main",
                    new AddCond(ConditionEffectIndex.Invincible),
                    new PlayerWithinTransition(12, "begin")
                    ),
                new State("begin",
                    new RemCond(ConditionEffectIndex.Invincible),
                    new Chase(speed: 16, acquireRange: 10, range: 5),
                    new Wander(speed: 4),
                    new Shoot(radius: 18, count: 3, shootAngle: 10, coolDown: 1250),
                    new NoPlayerWithinTransition(18, "nothing")
                    ),
                new State("nothing",
                    new AddCond(ConditionEffectIndex.Invincible),
                    new PlayerWithinTransition(12, "begin")
                    )
                )
            )
        #endregion "Demon Warrior of the Abyss"

        #region "Demon of the Abyss"
        .Init("Demon of the Abyss",
            new State(
                new State("main",
                    new AddCond(ConditionEffectIndex.Invincible),
                    new PlayerWithinTransition(12, "begin")
                    ),
                new State("begin",
                    new RemCond(ConditionEffectIndex.Invincible),
                    new Chase(speed: 16, acquireRange: 10, range: 5),
                    new Wander(speed: 4),
                    new Shoot(radius: 15, count: 3, shootAngle: 10, coolDown: 1250),
                    new NoPlayerWithinTransition(18, "nothing")
                    ),
                new State("nothing",
                    new AddCond(ConditionEffectIndex.Invincible),
                    new PlayerWithinTransition(12, "begin")
                    )
                )
            )
        #endregion "Demon of the Abyss"

        #region "White Demon of the Abyss"
        .Init("White Demon of the Abyss",
            new State(
                new State("main",
                    new AddCond(ConditionEffectIndex.Invincible),
                    new PlayerWithinTransition(12, "begin")
                    ),
                new State("begin",
                    new RemCond(ConditionEffectIndex.Invincible),
                    new Chase(speed: 8, acquireRange: 10, range: 5),
                    new Wander(speed: 4),
                    new Shoot(radius: 12, count: 3, shootAngle: 10, coolDown: 1000),
                    new NoPlayerWithinTransition(18, "nothing")
                    ),
                new State("nothing",
                    new AddCond(ConditionEffectIndex.Invincible),
                    new PlayerWithinTransition(12, "begin")
                    )
                )
            )
        #endregion "White Demon of the Abyss"
        ;
    }
}