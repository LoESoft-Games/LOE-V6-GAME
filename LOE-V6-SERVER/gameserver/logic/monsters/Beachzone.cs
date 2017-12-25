using gameserver.logic.behaviors;
using gameserver.logic.loot;
using gameserver.logic.transitions;

namespace gameserver.logic
{
    partial class BehaviorDb
    {
        private _ Beachzone = () => Behav()
            .Init("Masked Party God",
                new State(
                    new Heal(1, "Self", 5000),
                    new State("MainAlt1",
                         new Taunt(1, 1000,
                        "Oh no, Mixcoatl is my brother, I prefer partying to fighting.",
                        "Lets have a fun-time in the sun-shine!",
                        "Nothing like relaxin' on the beach.",
                        "Chillin' is the name of the game!"
                       ),

                        new SetAltTexture(1),
                        new TimedTransition(2000, "MainAlt2")
                    ),
                    new State("MainAlt2",
                          new Taunt(1, 1000,
                        "I hope you're having a good time!",
                        "How do you like my shades?",
                        "EVERYBODY BOOGEY!",
                        "What a beautiful day!"
                       ),
                        new SetAltTexture(2),
                        new TimedTransition(2000, "MainAlt3")
                    ),
                    new State("MainAlt3",
                    new Taunt(1, 1000,
                        "Whoa there!",
                        "OH SNAP",
                        "Ho!",
                        "This is pretty 'Secret', get it?"
                       ),
                        new SetAltTexture(3),
                        new TimedTransition(2000, "MainAlt1")
                    )

                ),
                new Threshold(0.1,
                    new ItemLoot("Blue Paradise", 0.2),
                    new ItemLoot("Pink Passion Breeze", 0.2),
                    new ItemLoot("Bahama Sunrise", 0.2),
                    new ItemLoot("Lime Jungle Bay", 0.2)
                )
            );
    }
}
