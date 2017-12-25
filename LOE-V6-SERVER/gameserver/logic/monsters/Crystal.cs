#region

using gameserver.logic.behaviors;
using gameserver.logic.loot;
using gameserver.logic.transitions;

#endregion

namespace gameserver.logic
{
    partial class BehaviorDb
    {
        private _ Crystal = () => Behav()
            .Init("Mysterious Crystal",
                new State(
                    new DropPortalOnDeath("Deadwater Docks", 100),
                    new State("Idle",
                        new Taunt(0.1, "Break the crystal for great rewards..."),
                        new Taunt(0.1, "Help me..."),
                        new HpLessTransition(0.9999, "PreEval"),
                        new TimedTransition(10000, "Idle")
                        ),
                    new State("PreEval",
                        new Flash(0xffffffff, 2, 100),
                        new Taunt(0.8, "Fire upon this crystal with all your might for 5 seconds"),
                        new Taunt(0.8, "If your attacks are weak, the crystal magically heals"),
                        new Taunt(0.8, "Gather a large group to smash it open"),
                        new HpLessTransition(0.998, "Evaluation")
                         ),
                    new State("Evaluation",
                        new State("Comments",
                            new Taunt(true, "Sweet treasure awaits for powerful adventurers!"),
                            new Taunt(0.4, "Yes!  Smash my prison for great rewards!"),
                            new Taunt(0.3, "If you are not very strong, this could kill you",
                                "If you are not yet powerful, stay away from the Crystal",
                                "New adventurers should stay away",
                                "That's the spirit. Lay your fire upon me.",
                                "So close...",
                               "Yes! Smash my prison for great rewards!",
                               "Sweet treasure awaits for powerful adventurers!",
                               "I think you need more people...",
                                "Call all your friends to help you break the crystal!",
                                "I think you need more people...",
                                "Call all your friends to help you break the crystal!"
                                ),
                            new TimedTransition(5000, "Comments")
                            ),

                        new Heal(1, "Crystals", 5000),
                        new HpLessTransition(0.95, "StartBreak"),
                        new TimedTransition(60000, "Fail")
                        ), // Evaluation 
                    new State("Fail",
                        new Taunt("Perhaps you need a bigger group. Ask others to join you!"),
                        new Flash(0xff000000, 5, 1),
                        new Shoot(10, 16, 22.5, fixedAngle: 0, coolDown: 100000),
                        new Heal(1, "Crystals", 1000),
                        new TimedTransition(5000, "Idle")
                        ),
                    new State("StartBreak",
                        new Taunt("You cracked the crystal! Soon we shall emerge!"),
                        new ChangeSize(-2, 80),
                        new AddCond(ConditionEffectIndex.Invulnerable),
                        new Flash(0xff000000, 2, 10),
                        new TimedTransition(4000, "BreakCrystal")
                        ),
                    new State("BreakCrystal",
                        new Taunt("This your reward! Imagine what evil even Oryx needs to keep locked up!"),
                        new Shoot(0, 16, 22.5, fixedAngle: 0, coolDown: 100000),
                        new Spawn("Crystal Prisoner", 1, 1, 100000),
                        new Decay(0)
                        )
                    )
            )
            .Init("Crystal Prisoner",
                new State(
                    new Spawn("Crystal Prisoner Steed", 5, 0, 200),
                    new State("pause",
                        new AddCond(ConditionEffectIndex.Invulnerable),
                        new TimedTransition(2000, "start_the_fun")
                        ),
                    new State("start_the_fun",
                        new AddCond(ConditionEffectIndex.Invulnerable),
                        new Taunt("I'm finally free! Yesss!!!"),
                        new TimedTransition(1500, "Daisy_attack")
                        ),
                    new State("Daisy_attack",
                        new Shoot(10, projectileIndex: 3, coolDown: 1000),
                        new Prioritize(
                            new StayCloseToSpawn(0.3, 7),
                            new Wander(0.3)
                            ),
                        new State("QuadforceI",
                            new AddCond(ConditionEffectIndex.Invulnerable),
                            new Shoot(0, projectileIndex: 0, count: 4, shootAngle: 90, fixedAngle: 0, coolDown: 300),
                            new Shoot(0, projectileIndex: 0, count: 4, shootAngle: 90, fixedAngle: 15, coolDown: 300, coolDownOffset: 300),
                            new Shoot(0, projectileIndex: 0, count: 4, shootAngle: 90, fixedAngle: 30, coolDown: 300, coolDownOffset: 600),
                            new Shoot(0, projectileIndex: 0, count: 4, shootAngle: 90, fixedAngle: 45, coolDown: 300, coolDownOffset: 900),
                            new TimedTransition(200, "QuadforceV")
                            ),
                        new State("QuadforceV",
                            new AddCond(ConditionEffectIndex.Invulnerable),
                            new Shoot(0, projectileIndex: 0, count: 4, shootAngle: 90, fixedAngle: 60, coolDown: 300),
                            new Shoot(0, projectileIndex: 0, count: 4, shootAngle: 90, fixedAngle: 75, coolDown: 300, coolDownOffset: 300),
                            new Shoot(0, projectileIndex: 0, count: 4, shootAngle: 90, fixedAngle: 90, coolDown: 300, coolDownOffset: 600),
                            new Shoot(0, projectileIndex: 0, count: 4, shootAngle: 90, fixedAngle: 105, coolDown: 300, coolDownOffset: 900),
                            new TimedTransition(200, "QuadforceI")
                            ),
                        new HpLessTransition(0.3, "Whoa_nelly"),
                        new TimedTransition(18000, "Warning")
                        ),
                    new State("Warning",
                        new Prioritize(
                            new StayCloseToSpawn(0.5, 7),
                            new Wander(0.5)
                            ),
                        new AddCond(ConditionEffectIndex.Invulnerable),
                        new Flash(0xff00ff00, 0.2, 15),
                        new Chase(0.4, 9, 2),
                        new TimedTransition(3000, "Summon_the_clones")
                        ),
                    new State("Summon_the_clones",
                        new Prioritize(
                            new StayCloseToSpawn(0.85, 7),
                            new Wander(0.85)
                            ),
                        new Shoot(10, projectileIndex: 0, coolDown: 1000),
                        new Spawn("Crystal Prisoner Clone", 4, 0, 200),
                        new TossObject("Crystal Prisoner Clone", 5, 0, 100000),
                        new TossObject("Crystal Prisoner Clone", 5, 240, 100000),
                        new TossObject("Crystal Prisoner Clone", 7, 60, 100000),
                        new TossObject("Crystal Prisoner Clone", 7, 300, 100000),
                        new State("invulnerable_clone",
                            new AddCond(ConditionEffectIndex.Invulnerable),
                            new TimedTransition(3000, "vulnerable_clone")
                            ),
                        new State("vulnerable_clone",
                            new TimedTransition(1200, "invulnerable_clone")
                            ),
                        new TimedTransition(16000, "Warning2")
                        ),
                    new State("Warning2",
                        new Prioritize(
                            new StayCloseToSpawn(0.85, 7),
                            new Wander(0.85)
                            ),
                        new AddCond(ConditionEffectIndex.Invulnerable),
                        new Flash(0xff00ff00, 0.2, 25),
                        new TimedTransition(5000, "Whoa_nelly")
                        ),
                    new State("Whoa_nelly",
                        new Prioritize(
                            new StayCloseToSpawn(0.6, 7),
                            new Wander(0.6)
                            ),
                        new Shoot(10, projectileIndex: 3, count: 3, shootAngle: 120, coolDown: 900),
                        new Shoot(10, projectileIndex: 2, count: 3, shootAngle: 15, fixedAngle: 40, coolDown: 1600,
                            coolDownOffset: 0),
                        new Shoot(10, projectileIndex: 2, count: 3, shootAngle: 15, fixedAngle: 220, coolDown: 1600,
                            coolDownOffset: 0),
                        new Shoot(10, projectileIndex: 2, count: 3, shootAngle: 15, fixedAngle: 130, coolDown: 1600,
                            coolDownOffset: 800),
                        new Shoot(10, projectileIndex: 2, count: 3, shootAngle: 15, fixedAngle: 310, coolDown: 1600,
                            coolDownOffset: 800),
                        new State("invulnerable_whoa",
                            new AddCond(ConditionEffectIndex.Invulnerable),
                            new TimedTransition(2600, "vulnerable_whoa")
                            ),
                        new State("vulnerable_whoa",
                            new TimedTransition(1200, "invulnerable_whoa")
                            ),
                        new TimedTransition(10000, "Absolutely_Massive")
                        ),
                    new State("Absolutely_Massive",
                        new ChangeSize(13, 260),
                        new Prioritize(
                            new StayCloseToSpawn(0.2, 7),
                            new Wander(0.2)
                            ),
                        new Shoot(10, projectileIndex: 1, count: 9, shootAngle: 40, fixedAngle: 40, coolDown: 2000,
                            coolDownOffset: 400),
                        new Shoot(10, projectileIndex: 1, count: 9, shootAngle: 40, fixedAngle: 60, coolDown: 2000,
                            coolDownOffset: 800),
                        new Shoot(10, projectileIndex: 1, count: 9, shootAngle: 40, fixedAngle: 50, coolDown: 2000,
                            coolDownOffset: 1200),
                        new Shoot(10, projectileIndex: 1, count: 9, shootAngle: 40, fixedAngle: 70, coolDown: 2000,
                            coolDownOffset: 1600),
                        new State("invulnerable_mass",
                            new AddCond(ConditionEffectIndex.Invulnerable),
                            new TimedTransition(2600, "vulnerable_mass")
                            ),
                        new State("vulnerable_mass",
                            new TimedTransition(1000, "invulnerable_mass")
                            ),
                        new TimedTransition(14000, "Start_over_again")
                        ),
                    new State("Start_over_again",
                        new ChangeSize(-20, 100),
                        new AddCond(ConditionEffectIndex.Invulnerable),
                        new Flash(0xff00ff00, 0.2, 15),
                        new TimedTransition(3000, "Daisy_attack")
                        )
                    ),
                new Threshold(1,
                    new ItemLoot("Potion of Vitality", 1)
                ),
                new Threshold(0.015,
                    new TierLoot(2, ItemType.Potion, 0.07)
                    ),
                new Threshold(0.03,
                    new ItemLoot("Crystal Wand", 0.005),
                    new ItemLoot("Crystal Sword", 0.006)
                    )
            )
            .Init("Crystal Prisoner Clone",
                new State(
                    new Prioritize(
                        new StayCloseToSpawn(0.85, range: 5),
                        new Wander(0.85)
                        ),
                    new Shoot(10, coolDown: 1400),
                    new State("taunt",
                        new Taunt(0.09, "I am everywhere and nowhere!"),
                        new TimedTransition(1000, "no_taunt")
                        ),
                    new State("no_taunt",
                        new TimedTransition(1000, "taunt")
                        ),
                    new Decay(17000)
                    )
            )
            .Init("Crystal Prisoner Steed",
                new State(
                    new State("change_position_fast",
                        new AddCond(ConditionEffectIndex.Invulnerable),
                        new Prioritize(
                            new StayCloseToSpawn(3.6, 12),
                            new Wander(3.6)
                            ),
                        new TimedTransition(800, "attack")
                        ),
                    new State("attack",
                        new Shoot(10, predictive: 0.3, coolDown: 500),
                        new State("keep_distance",
                            new Prioritize(
                                new StayCloseToSpawn(1, 12),
                                new Orbit(1, 9, target: "Crystal Prisoner", radiusVariance: 0)
                                ),
                            new TimedTransition(2000, "go_anywhere")
                            ),
                        new State("go_anywhere",
                            new Prioritize(
                                new StayCloseToSpawn(1, 12),
                                new Wander(1)
                                ),
                            new TimedTransition(2000, "keep_distance")
                            )
                        )
                    )
            )
            ;
    }
}