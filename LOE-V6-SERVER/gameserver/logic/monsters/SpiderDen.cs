#region

using gameserver.logic.behaviors;
using gameserver.logic.transitions;
using gameserver.logic.loot;

#endregion
//by GhostMaree
namespace gameserver.logic
{
    partial class BehaviorDb
    {
        private _ SpiderDen = () => Behav()
            .Init("Arachna the Spider Queen",
                 new State(
                     new TransformOnDeath(target: "Realm Portal", returnToSpawn: true),
                     new State("start_the_fun",
                         new AddCond(ConditionEffectIndex.Invulnerable),
                         new PlayerWithinTransition(dist: 11, targetState: "set_web"),
                         new HpLessTransition(threshold: 0.9999, targetState: "set_web")
                         ),
                     new State("set_web",
                         new TossObject(child: "Arachna Web Spoke 1", range: 11, angle: 240, coolDown: 10000, ignoreStun: true),
                         new TossObject(child: "Arachna Web Spoke 2", range: 11, angle: 0, coolDown: 10000, ignoreStun: true),
                         new TossObject(child: "Arachna Web Spoke 3", range: 11, angle: 120, coolDown: 10000, ignoreStun: true),
                         new TossObject(child: "Arachna Web Spoke 4", range: 11, angle: 300, coolDown: 10000, ignoreStun: true),
                         new TossObject(child: "Arachna Web Spoke 5", range: 11, angle: 60, coolDown: 10000, ignoreStun: true),
                         new TossObject(child: "Arachna Web Spoke 6", range: 11, angle: 180, coolDown: 10000, ignoreStun: true),
                         new TossObject(child: "Arachna Web Spoke 7", range: 6.5, angle: 240, coolDown: 10000, ignoreStun: true),
                         new TossObject(child: "Arachna Web Spoke 8", range: 6.5, angle: 0, coolDown: 10000, ignoreStun: true),
                         new TossObject(child: "Arachna Web Spoke 9", range: 6.5, angle: 120, coolDown: 10000, ignoreStun: true),
                         new TimedTransition(time: 3000, targetState: "eat_flies")
                         ),
                     new State("eat_flies",
                         new RemCond(ConditionEffectIndex.Invulnerable),
                         new Shoot(radius: 15, count: 1, projectileIndex: 0, predictive: 0.15, coolDown: 950),
                         new Shoot(radius: 15, count: 1, projectileIndex: 1, coolDown: 1500, coolDownOffset: 1100),
                         new Shoot(radius: 99, count: 12, projectileIndex: 0, shootAngle: 30, coolDown: 2600),
                         new StayCloseToSpawn(speed: 1, range: 7),
                         new StayBack(speed: 0.9, distance: 7),
                         new Wander(speed: 0.8)
                         )
                     ),
                     new Drops(
                         new ItemLoot("Healing Ichor", 1),
                         new ItemLoot("Healing Ichor", 0.25),
                         new ItemLoot("Spider's Eye Ring", 0.08),
                         new ItemLoot("Poison Fang Dagger", 0.08),
                         new ItemLoot("Golden Dagger", 0.3)
                         )
            )
        .Init("Arachna Web Spoke 1",
            new State(
                new AddCond(ConditionEffectIndex.Invulnerable),
                new State("idle",
                    new Shoot(radius: 99, count: 1, projectileIndex: 0, shootAngle: 0, fixedAngle: 0, coolDown: 200),
                    new Shoot(radius: 99, count: 1, projectileIndex: 0, shootAngle: 60, fixedAngle: 60, coolDown: 200),
                    new Shoot(radius: 99, count: 1, projectileIndex: 0, shootAngle: 120, fixedAngle: 120, coolDown: 200),
                    new EntityNotExistsTransition(target: "Arachna the Spider Queen", dist: 99, targetState: "die")
                    ),
                new State("die",
                    new Suicide()
                    )
                )
            )
        .Init("Arachna Web Spoke 2",
            new State(
                new AddCond(ConditionEffectIndex.Invulnerable),
                new State("idle",
                    new Shoot(radius: 99, count: 1, projectileIndex: 0, shootAngle: 120, fixedAngle: 120, coolDown: 200),
                    new Shoot(radius: 99, count: 1, projectileIndex: 0, shootAngle: 180, fixedAngle: 180, coolDown: 200),
                    new Shoot(radius: 99, count: 1, projectileIndex: 0, shootAngle: 240, fixedAngle: 240, coolDown: 200),
                    new EntityNotExistsTransition(target: "Arachna the Spider Queen", dist: 99, targetState: "die")
                    ),
                new State("die",
                    new Suicide()
                    )
                )
            )
        .Init("Arachna Web Spoke 3",
            new State(
                new AddCond(ConditionEffectIndex.Invulnerable),
                new State("idle",
                    new Shoot(radius: 99, count: 1, projectileIndex: 0, shootAngle: 0, fixedAngle: 0, coolDown: 200),
                    new Shoot(radius: 99, count: 1, projectileIndex: 0, shootAngle: 240, fixedAngle: 240, coolDown: 200),
                    new Shoot(radius: 99, count: 1, projectileIndex: 0, shootAngle: 300, fixedAngle: 300, coolDown: 200),
                    new EntityNotExistsTransition(target: "Arachna the Spider Queen", dist: 99, targetState: "die")
                    ),
                new State("die",
                    new Suicide()
                    )
                )
            )
        .Init("Arachna Web Spoke 4",
            new State(
                new AddCond(ConditionEffectIndex.Invulnerable),
                new State("idle",
                    new Shoot(radius: 99, count: 1, projectileIndex: 0, shootAngle: 120, fixedAngle: 120, coolDown: 200),
                    new EntityNotExistsTransition(target: "Arachna the Spider Queen", dist: 99, targetState: "die")
                    ),
                new State("die",
                    new Suicide()
                    )
                )
            )
        .Init("Arachna Web Spoke 5",
            new State(
                new AddCond(ConditionEffectIndex.Invulnerable),
                new State("idle",
                    new Shoot(radius: 99, count: 1, projectileIndex: 0, shootAngle: 240, fixedAngle: 240, coolDown: 200),
                    new EntityNotExistsTransition(target: "Arachna the Spider Queen", dist: 99, targetState: "die")
                    ),
                new State("die",
                    new Suicide()
                    )
                )
            )
        .Init("Arachna Web Spoke 6",
            new State(
                new AddCond(ConditionEffectIndex.Invulnerable),
                new State("idle",
                    new Shoot(radius: 99, count: 1, projectileIndex: 0, shootAngle: 0, fixedAngle: 0, coolDown: 200),
                    new EntityNotExistsTransition(target: "Arachna the Spider Queen", dist: 99, targetState: "die")
                    ),
                new State("die",
                    new Suicide()
                    )
                )
            )
        .Init("Arachna Web Spoke 7",
            new State(
                new AddCond(ConditionEffectIndex.Invulnerable),
                new State("idle",
                    new Shoot(radius: 99, count: 1, projectileIndex: 0, shootAngle: 0, fixedAngle: 0, coolDown: 200),
                    new Shoot(radius: 99, count: 1, projectileIndex: 0, shootAngle: 120, fixedAngle: 120, coolDown: 200),
                    new EntityNotExistsTransition(target: "Arachna the Spider Queen", dist: 99, targetState: "die")
                    ),
                new State("die",
                    new Suicide()
                    )
                )
            )
        .Init("Arachna Web Spoke 8",
            new State(
                new AddCond(ConditionEffectIndex.Invulnerable),
                new State("idle",
                    new Shoot(radius: 99, count: 1, projectileIndex: 0, shootAngle: 120, fixedAngle: 120, coolDown: 200),
                    new Shoot(radius: 99, count: 1, projectileIndex: 0, shootAngle: 240, fixedAngle: 240, coolDown: 200),
                    new EntityNotExistsTransition(target: "Arachna the Spider Queen", dist: 99, targetState: "die")
                    ),
                new State("die",
                    new Suicide()
                    )
                )
            )
        .Init("Arachna Web Spoke 9",
            new State(
                new AddCond(ConditionEffectIndex.Invulnerable),
                new State("idle",
                    new Shoot(radius: 99, count: 1, projectileIndex: 0, shootAngle: 0, fixedAngle: 0, coolDown: 200),
                    new Shoot(radius: 99, count: 1, projectileIndex: 0, shootAngle: 240, fixedAngle: 240, coolDown: 200),
                    new EntityNotExistsTransition(target: "Arachna the Spider Queen", dist: 99, targetState: "die")
                    ),
                new State("die",
                    new Suicide()
                    )
                )
            )
       .Init("Spider Egg Sac",
            new State(
                new TransformOnDeath(target: "Green Den Spider Hatchling", min: 2, max: 7),
                new State("idle",
                    new PlayerWithinTransition(dist: 2, targetState: "Explode")
                    ),
                new State("Explode",
                    new Suicide()
                    )
                )
            )
       .Init("Green Den Spider Hatchling",
            new State(
                new Shoot(radius: 9, predictive: 0.5, coolDown: 1000),
                new Chase(speed: 0.8, acquireRange: 10, range: 8),
                new Wander(speed: 0.4)
                )
             )
        .Init("Black Den Spider",
            new State(
                new Shoot(radius: 3, predictive: 0.5, coolDown: 500),
                new Charge(speed: 2, range: 9, coolDown: 2000),
                new Wander(speed: 0.8)
                ),
            new Drops(
                new ItemLoot("Healing Ichor", 0.05)
                )
            )
       .Init("Red Spotted Den Spider",
            new State(
                new Shoot(radius: 9, predictive: 0.5, coolDown: 500),
                new Chase(speed: 1, acquireRange: 10, range: 8),
                new Wander(speed: 0.4)
                ),
            new Drops(
                new ItemLoot("Healing Ichor", 0.05)
                )
            )
        .Init("Black Spotted Den Spider",
            new State(
                new Shoot(radius: 9, predictive: 0.5, coolDown: 500),
                new Charge(speed: 4, range: 9, coolDown: 2000),
                new Wander(speed: 0.8)
                ),
            new Drops(
                new ItemLoot("Healing Ichor", 0.05)
                )
            )
       .Init("Brown Den Spider",
            new State(
                new Shoot(radius: 9, count: 3, shootAngle: 9, predictive: 0.5, coolDown: 500),
                new Chase(speed: 0.8, acquireRange: 10, range: 3),
                new Wander(speed: 0.8)
                ),
            new Drops(
                new ItemLoot("Healing Ichor", 0.05)
                )
           );
    }
}
