#region

using gameserver.logic.behaviors;
using gameserver.logic.loot;
using gameserver.logic.transitions;

#endregion
//made by ghostmaree
namespace gameserver.logic
{
    partial class BehaviorDb
    {
        private _ PirateCave = () => Behav()
        .Init("Dreadstump the Pirate King",
            new State(
                new TransformOnDeath(target: "Realm Portal"),
                new State("Start",
                    new Wander(speed: 0.65),
                    new StayCloseToSpawn(speed: 0.65, range: 6),
                    new HpLessTransition(threshold: 0.9999, targetState: "Boast")
                ),
                new State("Boast",
                    new Taunt("Hah! I'll drink my rum out of your skull!"),
                    new Wander(speed: 0.65),
                    new StayCloseToSpawn(speed: 0.65, range: 6),
                    new Shoot(radius: 8, projectileIndex: 0, coolDown: 500),
                    new TimedTransition(time: 3000, targetState: "Normal")
                    ),
                new State("Normal",
                    new Taunt("Arrrr..."),
                    new Shoot(radius: 8, projectileIndex: 0, coolDown: 500),
                    new Shoot(radius: 10, projectileIndex: 1, coolDown: 2500, rotateEffect: true),
                    new Wander(speed: 0.65),
                    new StayCloseToSpawn(speed: 0.65, range: 6),
                    new TimedTransition(time: 6500, targetState: "Cannon_Cluster")
                    ),
                new State("Cannon_Cluster",
                    new Taunt("Eat cannonballs!"),
                    new StayCloseToSpawn(speed: 0.4, range: 6),
                    new Orbit(speed: 0.4, radius: 5, acquireRange: 5),
                    new Shoot(radius: 8, projectileIndex: 1, coolDown: 999999, coolDownOffset: 1800, rotateEffect: true),
                    new Shoot(radius: 8, projectileIndex: 1, coolDown: 999999, coolDownOffset: 2200, rotateEffect: true),
                    new Shoot(radius: 8, projectileIndex: 1, coolDown: 999999, coolDownOffset: 2600, rotateEffect: true),
                    new TimedTransition(time: 4000, targetState: "Normal")
                    )
                ),
            new Drops(
                new TierLoot(2, ItemType.Weapon, 0.20),
                new TierLoot(3, ItemType.Weapon, 0.10),
                new TierLoot(4, ItemType.Weapon, 0.05),
                new TierLoot(1, ItemType.Armor, 0.2),
                new TierLoot(2, ItemType.Armor, 0.1),
                new TierLoot(3, ItemType.Armor, 0.05),
                new TierLoot(4, ItemType.Armor, 0.02),
                new TierLoot(1, ItemType.Ring, 0.05),
                new ItemLoot("Pirate Rum", 0.01)
                )
          )
          .Init("Pirate Lieutenant",
              new State(
                  new Shoot(radius: 8, projectileIndex: 0, coolDown: 1500, rotateEffect: true),
                  new Wander(speed: 0.4),
                  new Protect(speed: 0.8, protectee: "Dreadstump the Pirate King", protectionRange: 6, reprotectRange: 6)
                  ),
              new Drops(
                  new TierLoot(2, ItemType.Weapon, 0.2),
                  new TierLoot(3, ItemType.Weapon, 0.10),
                  new TierLoot(1, ItemType.Armor, 0.2),
                  new TierLoot(2, ItemType.Armor, 0.1),
                  new TierLoot(1, ItemType.Ring, 0.05),
                  new ItemLoot("Pirate Rum", 0.001)
                  )
         )
         .Init("Pirate Commander",
              new State(
                  new Shoot(radius: 8, projectileIndex: 0, coolDown: 1500, rotateEffect: true),
                  new Wander(speed: 0.4),
                  new Protect(speed: 0.8, protectee: "Dreadstump the Pirate King", protectionRange: 6, reprotectRange: 6)
                  ),
              new Drops(
                  new TierLoot(2, ItemType.Weapon, 0.2),
                  new TierLoot(3, ItemType.Weapon, 0.10),
                  new TierLoot(1, ItemType.Armor, 0.2),
                  new TierLoot(2, ItemType.Armor, 0.1),
                  new TierLoot(1, ItemType.Ring, 0.05),
                  new ItemLoot("Pirate Rum", 0.001)
                  )
          )
          .Init("Pirate Captain",
              new State(
                  new Shoot(radius: 8, projectileIndex: 0, coolDown: 1500, rotateEffect: true),
                  new Wander(speed: 0.4),
                  new Protect(speed: 0.8, protectee: "Dreadstump the Pirate King", protectionRange: 6, reprotectRange: 6)
                  ),
              new Drops(
                  new TierLoot(2, ItemType.Weapon, 0.2),
                  new TierLoot(3, ItemType.Weapon, 0.10),
                  new TierLoot(1, ItemType.Armor, 0.2),
                  new TierLoot(2, ItemType.Armor, 0.1),
                  new TierLoot(1, ItemType.Ring, 0.05),
                  new ItemLoot("Pirate Rum", 0.001)
                  )
          )
          .Init("Pirate Admiral",
              new State(
                  new Shoot(radius: 8, projectileIndex: 0, coolDown: 1500, rotateEffect: true),
                  new Wander(speed: 0.4),
                  new Protect(speed: 0.8, protectee: "Dreadstump the Pirate King", protectionRange: 6, reprotectRange: 6)
                  ),
              new Drops(
                  new TierLoot(2, ItemType.Weapon, 0.2),
                  new TierLoot(3, ItemType.Weapon, 0.10),
                  new TierLoot(1, ItemType.Armor, 0.2),
                  new TierLoot(2, ItemType.Armor, 0.1),
                  new TierLoot(1, ItemType.Ring, 0.05),
                  new ItemLoot("Pirate Rum", 0.001)
                  )
          )
          .Init("Cave Pirate Brawler",
              new State(
                  new Shoot(projectileIndex: 0, coolDown: 1000),
                  new Chase(speed: 0.8, range: 1),
                  new Wander(speed: 0.4)
                  ),
              new Drops(
                  new ItemLoot("Health Potion", 0.03)
                  )
          )
          .Init("Cave Pirate Sailor",
              new State(
                  new Shoot(projectileIndex: 0, coolDown: 1000),
                  new Chase(speed: 0.8, range: 1),
                  new Wander(speed: 0.4)
                  ),
              new Drops(
                  new ItemLoot("Health Potion", 0.03)
                  )
          )
          .Init("Cave Pirate Veteran",
              new State(
                  new Shoot(projectileIndex: 0, coolDown: 1000),
                  new Chase(speed: 0.8, range: 1),
                  new Wander(speed: 0.4)
                  ),
              new Drops(
                  new ItemLoot("Health Potion", 0.03)
                  )
          )
          .Init("Cave Pirate Moll",
              new State(
                  new Wander(speed: 0.4, avoidGround: true, ground: "Shallow Water")
                  ),
              new Drops(
                  new TierLoot(1, ItemType.Ability, 0.2)
                  )
          )
          .Init("Cave Pirate Parrot",
              new State(
                  new Wander(speed: 0.4, avoidGround: true, ground: "Shallow Water")
                  ),
              new Drops(
                  new TierLoot(1, ItemType.Ability, 0.2)
                  )
          )
          .Init("Cave Pirate Macaw",
              new State(
                  new Wander(speed: 0.4, avoidGround: true, ground: "Shallow Water")
                  ),
              new Drops(
                  new TierLoot(1, ItemType.Ability, 0.2)
                  )
          )
          .Init("Cave Pirate Monkey",
              new State(
                  new Wander(speed: 0.4, avoidGround: true, ground: "Shallow Water")
                  ),
              new Drops(
                  new TierLoot(1, ItemType.Ability, 0.2)
                  )
          )
          .Init("Cave Pirate Hunchback",
              new State(
                  new Wander(speed: 0.4, avoidGround: true, ground: "Shallow Water")
                  ),
              new Drops(
                  new TierLoot(1, ItemType.Ability, 0.2)
                  )
          )
          .Init("Cave Pirate Cabin Boy",
              new State(
                  new Wander(speed: 0.4, avoidGround: true, ground: "Shallow Water")
                  ),
              new Drops(
                  new TierLoot(1, ItemType.Ability, 0.2)
                  )
          );
    }
}