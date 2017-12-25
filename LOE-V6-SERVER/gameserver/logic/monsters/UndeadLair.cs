using gameserver.logic.behaviors;
using gameserver.logic.loot;
using gameserver.logic.transitions;

namespace gameserver.logic
{
    partial class BehaviorDb
    {
        private _ UndeadLair = () => Behav()
            .Init("Septavius the Ghost God",
                new State(
                    new State("Idle",
                        new PlayerWithinTransition(15, "Cycle")
                    ),
                    new State("Cycle",
                        new Shoot(15, projectileIndex: 3, coolDown: 1000),
                        new State("Cycle Begin",
                            new State("Cycle V",
                                new Shoot(15, count: 3, fixedAngle: 0),
                                new Shoot(15, count: 3, fixedAngle: 18, coolDownOffset: 150, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 36, coolDownOffset: 300, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 54, coolDownOffset: 450, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 72, coolDownOffset: 600, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 90, coolDownOffset: 750, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 108, coolDownOffset: 900, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 126, coolDownOffset: 1050, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 144, coolDownOffset: 1200, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 162, coolDownOffset: 1350, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 180, coolDownOffset: 1500, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 198, coolDownOffset: 1650, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 216, coolDownOffset: 1800, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 234, coolDownOffset: 1950, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 252, coolDownOffset: 2100, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 270, coolDownOffset: 2250, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 288, coolDownOffset: 2400, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 306, coolDownOffset: 2550, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 324, coolDownOffset: 2700, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 342, coolDownOffset: 2850, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 360, coolDownOffset: 3000, coolDown: 100000),
                                                        new HpLessTransition(.5, "Ring Attack + Flashing"),
                                new TimedTransition(3045, "Cycle I")
                            ),
                            new State("Cycle I",
                                new AddCond(ConditionEffectIndex.Invulnerable),
                                new Shoot(15, count: 3, fixedAngle: 0, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 18, coolDownOffset: 150, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 36, coolDownOffset: 300, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 54, coolDownOffset: 450, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 72, coolDownOffset: 600, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 90, coolDownOffset: 750, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 108, coolDownOffset: 900, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 126, coolDownOffset: 1050, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 144, coolDownOffset: 1200, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 162, coolDownOffset: 1350, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 180, coolDownOffset: 1500, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 198, coolDownOffset: 1650, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 216, coolDownOffset: 1800, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 234, coolDownOffset: 1950, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 252, coolDownOffset: 2100, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 270, coolDownOffset: 2250, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 288, coolDownOffset: 2400, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 306, coolDownOffset: 2550, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 324, coolDownOffset: 2700, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 342, coolDownOffset: 2850, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 360, coolDownOffset: 3000, coolDown: 100000),
                                                        new HpLessTransition(.5, "Ring Attack + Flashing"),
                                new TimedTransition(3045, "Cycle V")
                            )

                        )

                    ),
                    new State("Ring Attack + Flashing",
                        new HpLessTransition(0.1, "Spiral + MSpawn"),
                        new State("Flash 1",
                            new AddCond(ConditionEffectIndex.Invulnerable),
                            new Flash(0x0000FF0C, 0.5, 4),
                            new TimedTransition(2000, "Ring Attack")
                        ),
                        new State("Ring Attack",
                            new Shoot(12, count: 10, fixedAngle: 12, projectileIndex: 3, coolDown: 1000),
                            new State("Ring Attack Idle",
                                new TimedTransition(2500, "SetEffect")
                            ),
                            new State("SetEffect",
                                new AddCond(ConditionEffectIndex.Invulnerable),
                                new TimedTransition(7500, "Flash 2")
                            )
                        ),
                        new State("Flash 2",
                            new Flash(0x0000FF0C, 0.2, 8),
                            new TimedTransition(1600, "Confuse + Quiet")
                        )
                    ),
                    new State("Confuse + Quiet",
                        new HpLessTransition(0.1, "Spiral + MSpawn"),
                        new State("Shoot",
                            new Shoot(15, count: 3, shootAngle: 15, projectileIndex: 2, coolDown: new Cooldown(750, 250)),
                            new Shoot(25, count: 12, fixedAngle: 0, projectileIndex: 1, coolDown: 500),
                            new State("Unset",
                                new TimedTransition(5000, "Stop Shooting")
                            )
                        ),
                        new State("Stop Shooting",
                            new AddCond(ConditionEffectIndex.Invulnerable),
                            new Flash(0x0000FF0C, 0.5, 4),
                            new TimedTransition(3000, "Spiral + MSpawn"))
                    ),
                    new State("Spiral + MSpawn",

                        new State("Spiral",
                            new Shoot(15, count: 3, shootAngle: 15, projectileIndex: 4, coolDown: new Cooldown(750, 250)),
                            new Spawn("Ghost Warrior of Septavius", 3, 0.7, coolDown: 950),
                            new Spawn("Ghost Mage of Septavius", 3, 0.7, coolDown: 950),
                            new Spawn("Ghost Rogue of Septavius", 3, 0.7, coolDown: 950),
                            new State("Cycle2",
                            new State("Cycle2 V",
                                new Shoot(15, count: 3, fixedAngle: 0),
                                new Shoot(15, count: 3, fixedAngle: 18, coolDownOffset: 150, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 36, coolDownOffset: 300, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 54, coolDownOffset: 450, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 72, coolDownOffset: 600, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 90, coolDownOffset: 750, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 108, coolDownOffset: 900, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 126, coolDownOffset: 1050, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 144, coolDownOffset: 1200, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 162, coolDownOffset: 1350, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 180, coolDownOffset: 1500, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 198, coolDownOffset: 1650, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 216, coolDownOffset: 1800, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 234, coolDownOffset: 1950, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 252, coolDownOffset: 2100, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 270, coolDownOffset: 2250, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 288, coolDownOffset: 2400, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 306, coolDownOffset: 2550, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 324, coolDownOffset: 2700, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 342, coolDownOffset: 2850, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 360, coolDownOffset: 3000, coolDown: 100000),
                                new TimedTransition(3, "Cycle2 I")
                            ),
                            new State("Cycle2 I",
                                new AddCond(ConditionEffectIndex.Invulnerable),
                                new Shoot(15, count: 3, fixedAngle: 0, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 18, coolDownOffset: 150, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 36, coolDownOffset: 300, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 54, coolDownOffset: 450, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 72, coolDownOffset: 600, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 90, coolDownOffset: 750, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 108, coolDownOffset: 900, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 126, coolDownOffset: 1050, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 144, coolDownOffset: 1200, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 162, coolDownOffset: 1350, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 180, coolDownOffset: 1500, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 198, coolDownOffset: 1650, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 216, coolDownOffset: 1800, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 234, coolDownOffset: 1950, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 252, coolDownOffset: 2100, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 270, coolDownOffset: 2250, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 288, coolDownOffset: 2400, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 306, coolDownOffset: 2550, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 324, coolDownOffset: 2700, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 342, coolDownOffset: 2850, coolDown: 100000),
                                new Shoot(15, count: 3, fixedAngle: 360, coolDownOffset: 3000, coolDown: 100000),
                                new TimedTransition(3045, "Cycle2 V")
                            )

                        )
                        )
                    )
                ),
                new Threshold(0.32, /* Maximum 3 wis, minimum 0 wis */
                    new ItemLoot("Potion of Wisdom", 1)
                ),
                new Threshold(0.1,
                    new ItemLoot("Bow of the Morning Star", 0.01),
                    new ItemLoot("Doom Bow", 0.005),
                    new ItemLoot("Wine Cellar Incantation", 0.005),
                    new TierLoot(3, ItemType.Ring, 0.2),
                    new TierLoot(4, ItemType.Ring, 0.1),
                    new TierLoot(7, ItemType.Weapon, 0.2),
                    new TierLoot(8, ItemType.Weapon, 0.1),
                    new TierLoot(3, ItemType.Ability, 0.2),
                    new TierLoot(4, ItemType.Ability, 0.15),
                    new TierLoot(5, ItemType.Ability, 0.1)
                ),
                new Threshold(0.2,
                    new EggLoot(EggRarity.Common, 0.1),
                    new EggLoot(EggRarity.Uncommon, 0.05),
                    new EggLoot(EggRarity.Rare, 0.01),
                    new EggLoot(EggRarity.Legendary, 0.002)
                )
            )
            .Init("Ghost Warrior of Septavius",
                new State(
                    new Shoot(10, coolDown: new Cooldown(2000, 1000)),
                    new State("Follow",
                        new Prioritize(
                            new Chase(.4, 7, 1),
                            new Protect(1, "Septavius the Ghost God", protectionRange: 1, reprotectRange: 2)
                        )
                    ),
                    new State("Wander",
                        new Wander(0.4)
                    )
                ),
                new ItemLoot("Health Potion", 0.2),
                new ItemLoot("Magic Potion", 0.2)
            )
            .Init("Ghost Mage of Septavius",
                new State(
                    new Shoot(10, coolDown: new Cooldown(2000, 1000)),
                    new State("Follow",
                        new Prioritize(
                            new Chase(.4, 7, 1),
                            new Protect(1, "Septavius the Ghost God", protectionRange: 1, reprotectRange: 2)
                        )
                    ),
                    new State("Wander",
                        new Wander(0.4)
                    )
                ),
                new ItemLoot("Health Potion", 0.2),
                new ItemLoot("Magic Potion", 0.2)
            )
            .Init("Ghost Rogue of Septavius",
                new State(
                    new Shoot(10, coolDown: new Cooldown(2000, 1000)),
                    new State("Follow",
                        new Prioritize(
                            new Chase(.4, 7, 1),
                            new Protect(1, "Septavius the Ghost God", protectionRange: 1, reprotectRange: 2)
                        )
                    ),
                    new State("Wander",
                        new Wander(0.4)
                    )
                ),
                new ItemLoot("Health Potion", 0.2),
                new ItemLoot("Magic Potion", 0.2)
            );
    }
}
