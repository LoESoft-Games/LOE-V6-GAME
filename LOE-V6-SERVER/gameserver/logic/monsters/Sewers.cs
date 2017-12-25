#region

using gameserver.logic.behaviors;
using gameserver.logic.loot;
using gameserver.logic.transitions;

#endregion

namespace gameserver.logic
{
    partial class BehaviorDb
    {
        private _ ToxicSewers = () => Behav()
        .Init("DS Alligator",
            new State(
                new State("idle",
                    new Wander(0.195),
                    new StayCloseToSpawn(0.125, 6),
                    new Shoot(5.25, 3, shootAngle: 6, coolDown: 800)
                    )
                )
            )
        .Init("DS Bat",
            new State(
                new State("idle",
                    new Wander(0.5),
                    new Chase(1.9, 5, 0.1),
                    new Shoot(4, 1, coolDown: 400)
                    )
                )
            )
        .Init("DS Brown Slime Trail",
            new State(
                new State("idle",
                    new Shoot(2, 1, coolDown: 75),
                    new TimedTransition(6000, "die")
                    ),
                new State("die",
                    new Suicide()
                    )
                )
            )
        .Init("DS Brown Slime",
            new State(
                new State("idle",
                    new Wander(0.3),
                    new Chase(1.3, 4.15, 0.1),
                    //new Spawn("DS Yellow Slime Trail", 9999, 0, coolDown: 1250),
                    new Shoot(8, 5, coolDown: 2000)
                    )
                )
            )
        .Init("DS Yellow Slime Trail",
            new State(
                new State("idle",
                    new Shoot(2, 1, coolDown: 75),
                    new TimedTransition(4500, "die")
                    ),
                new State("die",
                    new Suicide()
                    )
                )
            )
        .Init("DS Yellow Slime",
            new State(
                new State("idle",
                    new Wander(0.3),
                    new Chase(1.3, 4.15, 0.1),
                    //new Spawn("DS Yellow Slime Trail", 9999, 0, coolDown: 999),
                    new Shoot(10, 6, coolDown: 2600)
                    )
                )
            )
         .Init("DS Natural Slime God",
                new State(
                    new Prioritize(
                        new StayAbove(1, 200),
                        new Chase(1, range: 7),
                        new Wander(0.4)
                        ),
                    new Shoot(12, projectileIndex: 0, count: 5, shootAngle: 10, predictive: 1, coolDown: 1000),
                    new Shoot(10, projectileIndex: 1, predictive: 1, coolDown: 650)
                    ),
                new TierLoot(6, ItemType.Weapon, 0.04),
                new TierLoot(7, ItemType.Weapon, 0.02),
                new TierLoot(8, ItemType.Weapon, 0.01),
                new TierLoot(7, ItemType.Armor, 0.04),
                new TierLoot(8, ItemType.Armor, 0.02),
                new TierLoot(9, ItemType.Armor, 0.01),
                new TierLoot(4, ItemType.Ability, 0.02),
                new Threshold(0.018,
                    new ItemLoot("Potion of Defense", 0.0725)
                    )
            )
        .Init("DS Rat",
            new State(
                new State("idle",
                    new Wander(0.6),
                    new Chase(1.2, 7.15, 3.5),
                    new Shoot(5.6, 3, shootAngle: 12, coolDown: 825)
                    )
                )
            )
        .Init("DS Golden Rat",
            new State(
                new State("idle",
                    new Wander(0.195),
                    new PlayerWithinTransition(12, "talkaboutyourlittleratspeech")
                    ),
                new State("talkaboutyourlittleratspeech",
                    new Taunt("Squeek!"),
                    new TimedTransition(2000, "run")
                    ),
                new State("run",
                    new StayBack(1.0, 12),
                    new TimedTransition(15000, "die")
                    ),
                new State("die",
                    new Suicide()
                    )
                ),
                  new Threshold(0.31,
                      new ItemLoot("Potion of Defense", 0.1),
                      new ItemLoot("Murky Toxin", 0.01)
                )
            )
        .Init("DS Goblin Warlock",
            new State(
                new State("idle",
                    new StayBack(0.9, 3.2),
                    new Wander(0.195),
                    new Shoot(8, 2, 11, 0, coolDown: 125),
                    new Shoot(10, 1, projectileIndex: 1, coolDown: 1000)
                    )
                )
            )
        .Init("DS Goblin Knight",
            new State(
                new State("idle",
                    new PlayerWithinTransition(5, "activate")
                    ),
                new State("activate",
                    new Wander(0.15),
                    new Chase(0.9, 7.15, 2),
                    new Shoot(5.5, 1, coolDown: 1250)
                    )
                ),
            new Threshold(0.017,
                new TierLoot(7, ItemType.Armor, 0.1)
                )
            )
        .Init("DS Goblin Brute",
            new State(
            new Wander(0.225),
                new State("idle",
                    new Charge(1.6, 6, 1600),
                    new Shoot(3, 4, 15, 0, coolDown: 1600)
                    )
                )
            )
        .Init("DS Goblin Peon",
            new State(
            new Wander(0.3),
            new Chase(1.6, 6, 2, coolDown: 500),
                new State("idle",
                    new Shoot(3.525, 2, 14.5, 0, predictive: 0.15, coolDown: 890)
                    )
                )
            )
        .Init("ds goblin sorcerer",
            new State(
            new Wander(0.3),
                new State("idle",
                    new Shoot(6, 5, 12, 0, coolDown: 1500),
                    new Grenade(3, 50, 6, coolDown: 1800)
                    )
                )
            )
        .Init("DS Boss Minion",
            new State(
                new State("idle",
                    new Wander(0.6),
                    new Grenade(3, 50, 10, coolDown: 5000)
                    )
                )
            )
        .Init("DS Gulpord the Slime God M",
            new State(
                new State("idle",
                    new Orbit(0.6, 3, target: "ds gulpord the slime god"),
                    new Shoot(10, 8, 45, 1, coolDown: 1500),
                    new Shoot(10, 4, 60, 0, coolDown: 4500)
                    )
                )
            )
        .Init("ds gulpord the slime god s",
            new State(
                new State("idle",
                    new Orbit(0.6, 3, target: "ds gulpord the slime god"),
                    new Shoot(10, 4, 20, 1, coolDown: 2000)
                    )
                )
            )
        .Init("ds Gulpord the Slime God",
            new State(
                new RealmPortalDrop(),
                new State("idle",
                    new PlayerWithinTransition(12, "begin")
                    ),
                new State("begin",
                    new TimedTransition(500, "shoot")
                    ),
                new State("shoot",
                    new HpLessTransition(0.90, "randomshooting"),
                    new AddCond(ConditionEffectIndex.Invulnerable, duration: 1500),
                    new Shoot(10, 8, 45, 1, coolDown: 2000),
                    new Shoot(10, 5, 72, 0, 0, coolDown: 400, angleOffset: 0.2f),
                    new Shoot(10, 5, 72, 0, 3, coolDown: 400, angleOffset: 0.2f)
                    ),
                new State("randomshooting",
                    new AddCond(ConditionEffectIndex.Invulnerable, duration: 1500),
                    new Shoot(10, 8, 360 / 8, 0, fixedAngle: 0, coolDown: 300, angleOffset: 0.5f),
                    new ReturnToSpawn(true, 1),
                    new TimedTransition(6000, "tossnoobs")
                    ),
                new State("tossnoobs",
                    new TossObject("DS Boss Minion", 3, 0, coolDown: 99999999, randomToss: false),
                    new TossObject("DS Boss Minion", 3, 45, coolDown: 99999999, randomToss: false),
                    new TossObject("DS Boss Minion", 3, 90, coolDown: 99999999, randomToss: false),
                    new TossObject("DS Boss Minion", 3, 135, coolDown: 99999999, randomToss: false),
                    new TossObject("DS Boss Minion", 3, 180, coolDown: 99999999, randomToss: false),
                    new TossObject("DS Boss Minion", 3, 225, coolDown: 99999999, randomToss: false),
                    new TossObject("DS Boss Minion", 3, 270, coolDown: 99999999, randomToss: false),
                    new TossObject("DS Boss Minion", 3, 315, coolDown: 99999999, randomToss: false),
                    new TimedTransition(100, "derp")
                    ),
                new State("derp",
                    new AddCond(ConditionEffectIndex.Invulnerable, duration: 1500),
                    new HpLessTransition(0.50, "baibaiscrubs"),
                    new Shoot(10, 6, 12, 0, coolDown: 3000),
                    new Wander(0.5),
                    new StayCloseToSpawn(0.5, 7)
                    ),
                new State("baibaiscrubs",
                    new ReturnToSpawn(speed: 2),
                    new AddCond(ConditionEffectIndex.Invulnerable),
                    new TimedTransition(800, "seclol")
                    ),
                new State("seclol",
                    new ChangeSize(20, 0),
                    new TimedTransition(1000, "nubs")
                    ),
                new State("nubs",
                    new TossObject("DS Gulpord the Slime God M", 3, 32, coolDown: 9999999, invisiToss: true),
                    new TossObject("DS Gulpord the Slime God M", 3, 15, coolDown: 9999999, invisiToss: true),
                    new TimedTransition(100, "idleeeee")
                    ),
                new State("idleeeee",
                    new EntityNotExistsTransition("ds gulpord the slime god m", 10, "nubs2")
                    ),
                new State("nubs2",
                    new TossObject("DS Gulpord the Slime God s", 3, 32, coolDown: 9999999, invisiToss: true),
                    new TossObject("DS Gulpord the Slime God s", 3, 15, coolDown: 9999999, invisiToss: true),
                    new TossObject("DS Gulpord the Slime God s", 3, 26, coolDown: 9999999, invisiToss: true),
                    new TossObject("DS Gulpord the Slime God s", 3, 21, coolDown: 9999999, invisiToss: true),
                    new TimedTransition(100, "idleeeeee")
                    ),
                new State("idleeeeee",
                    new AddCond(ConditionEffectIndex.Invulnerable),
                    new EntityNotExistsTransition("ds gulpord the slime god s", 10, "seclolagain")
                    ),
                new State("seclolagain",
                    new ChangeSize(20, 120),
                    new TimedTransition(1000, "GO ANGRY!!!!111!!11")
                    ),
                new State("GO ANGRY!!!!111!!11",
                    new Flash(0xFF0000, 1, 1),
                    new TimedTransition(1000, "FOLLOW")
                    ),
                new State("FOLLOW",
                new RealmPortalDrop(),
                new AddCond(ConditionEffectIndex.ParalyzeImmune),
                new AddCond(ConditionEffectIndex.StunImmune),
                new RemCond(ConditionEffectIndex.Invulnerable),
                new Shoot(10, 8, 45, 2, coolDown: 2000),
                new Shoot(3, 1, 0, 1, coolDown: 1000),
                new Chase(0.6, 10, 0),
                    new State("xdshoot",
                        new Shoot(10, 2, 10, 0, coolDown: 150, angleOffset: 0.1f)
                        )
                    )
                )
            //    ),
            //new Threshold(0.28,
            //    new ItemLoot("Potion of Defense", 1),
            //    new ItemLoot("Void Blade", 0.02),
            //    new ItemLoot("Murky Toxin", 0.02),
            //    new ItemLoot("Wine Cellar Incantation", 0.01)
            //    )
            );
    }
}