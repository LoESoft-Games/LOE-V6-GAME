using gameserver.logic.behaviors;
using gameserver.logic.transitions;

namespace gameserver.logic
{
    partial class BehaviorDb
    {
        /// <summary>
        /// Abyss of Demons [Mini Boss]
        /// Production version made by Devwarlt
        /// 15th September 2017 20:19
        /// </summary>
        private _ Abyss_of_Demons_MiniBoss = () => Behav()

        #region "Abyss Idol"
        .Init("Abyss Idol",
            new State(
                new State("main",
                    new AddCond(ConditionEffectIndex.Invulnerable),
                    new PlayerWithinTransition(12, "begin")
                    ),
                new State("begin",
                    new RemCond(ConditionEffectIndex.Invulnerable),
                    new DamageTakenTransition(1, "start")
                    ),
                new State("start",
                    new Shoot(radius: 1.2, count: 6, shootAngle: 360 / 6, projectileIndex: 0, coolDownOffset: 1000, coolDown: 500),
                    new Shoot(radius: 8, count: 4, shootAngle: 360 / 4, projectileIndex: 1, predictive: 1, coolDownOffset: 1500, coolDown: 1500),
                    new Shoot(radius: 9.6, count: 3, shootAngle: 10, projectileIndex: 2, coolDownOffset: 2000, coolDown: 750),
                    new TossSetpiece(setpiece: "AbyssIdol_LavaBomb", range: 6, color: 0xFFA500, coolDown: 3000, cooldownOffset: 3000, special: SpecialSetPiece.ABYSS_IDOL),
                    new TimedTransition(12000, "stop toss")
                    ),
                new State("stop toss",
                    new AddCond(ConditionEffectIndex.Invulnerable),
                    new Shoot(radius: 1.2, count: 6, shootAngle: 360 / 6, projectileIndex: 0, coolDownOffset: 1000, coolDown: 500),
                    new Shoot(radius: 8, count: 4, shootAngle: 360 / 4, projectileIndex: 2, predictive: 1, coolDownOffset: 1500, coolDown: 1500),
                    new Shoot(radius: 9.6, count: 3, shootAngle: 10, projectileIndex: 3, coolDownOffset: 2000, coolDown: 750),
                    new TimedTransition(12000, "spawn minions")
                    ),
                new State("spawn minions",
                    new RemCond(ConditionEffectIndex.Invulnerable),
                    new Shoot(radius: 1.2, count: 6, shootAngle: 360 / 6, projectileIndex: 0, coolDownOffset: 1000, coolDown: 500),
                    new Shoot(radius: 8, count: 4, shootAngle: 360 / 4, projectileIndex: 2, predictive: 1, coolDownOffset: 1500, coolDown: 1500),
                    new Shoot(radius: 9.6, count: 3, shootAngle: 10, projectileIndex: 3, coolDownOffset: 2000, coolDown: 750),
                    new Reproduce(children: "Imp of the Abyss", densityRadius: 12, densityMax: 3, spawnRadius: 3, coolDown: 4000),
                    new Reproduce(children: "Brute Warrior of the Abyss", densityRadius: 12, densityMax: 2, spawnRadius: 3, coolDown: 4750),
                    new Reproduce(children: "Brute of the Abyss", densityRadius: 12, densityMax: 2, spawnRadius: 3, coolDown: 4750),
                    new Reproduce(children: "Demon Mage of the Abyss", densityRadius: 12, densityMax: 2, spawnRadius: 3, coolDown: 7000),
                    new Reproduce(children: "Demon Warrior of the Abyss", densityRadius: 12, densityMax: 2, spawnRadius: 3, coolDown: 7000),
                    new Reproduce(children: "Demon of the Abyss", densityRadius: 12, densityMax: 3, spawnRadius: 3, coolDown: 8000),
                    new TimedTransition(14000, "start")
                    )
                )
            )
        #endregion "Abyss Idol"
        ;
    }
}