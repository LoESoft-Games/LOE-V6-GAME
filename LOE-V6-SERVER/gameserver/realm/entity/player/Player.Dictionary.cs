#region

using System;
using System.Collections.Generic;

#endregion

namespace gameserver.realm.entity.player
{
    partial class Player
    {
        private static readonly Dictionary<string, Tuple<int, int, int>> questPortraits =
            new Dictionary<string, Tuple<int, int, int>> //Priority, Min, Max
            {
                {"Scorpion Queen", Tuple.Create(1, 1, 6)},
                {"Bandit Leader", Tuple.Create(1, 1, 6)},
                {"Hobbit Mage", Tuple.Create(3, 3, 8)},
                {"Undead Hobbit Mage", Tuple.Create(3, 3, 8)},
                {"Giant Crab", Tuple.Create(3, 3, 8)},
                {"Desert Werewolf", Tuple.Create(3, 3, 8)},
                {"Sandsman King", Tuple.Create(4, 4, 9)},
                {"Goblin Mage", Tuple.Create(4, 4, 9)},
                {"Elf Wizard", Tuple.Create(4, 4, 9)},
                {"Dwarf King", Tuple.Create(5, 5, 10)},
                {"Swarm", Tuple.Create(6, 6, 11)},
                {"Shambling Sludge", Tuple.Create(6, 6, 11)},
                {"Great Lizard", Tuple.Create(7, 7, 12)},
                {"Wasp Queen", Tuple.Create(8, 7, 1000)},
                {"Horned Drake", Tuple.Create(8, 7, 1000)},
                {"Deathmage", Tuple.Create(5, 6, 11)},
                {"Great Coil Snake", Tuple.Create(6, 6, 12)},
                {"Lich", Tuple.Create(9, 6, 1000)},
                {"Actual Lich", Tuple.Create(9, 7, 1000)},
                {"Ent Ancient", Tuple.Create(10, 7, 1000)},
                {"Actual Ent Ancient", Tuple.Create(10, 7, 1000)},
                {"Oasis Giant", Tuple.Create(11, 8, 1000)},
                {"Phoenix Lord", Tuple.Create(11, 9, 1000)},
                {"Ghost King", Tuple.Create(12, 10, 1000)},
                {"Actual Ghost King", Tuple.Create(12, 10, 1000)},
                {"Cyclops God", Tuple.Create(13, 10, 1000)},
                {"Red Demon", Tuple.Create(15, 15, 1000)},
                {"Lucky Djinn", Tuple.Create(15, 15, 1000)},
                {"Lucky Ent", Tuple.Create(15, 15, 1000)},
                {"Skull Shrine", Tuple.Create(16, 15, 1000)},
                {"Pentaract", Tuple.Create(16, 15, 1000)},
                {"Cube God", Tuple.Create(16, 15, 1000)},
                {"Grand Sphinx", Tuple.Create(16, 15, 1000)},
                {"Lord of the Lost Lands", Tuple.Create(16, 15, 1000)},
                {"Hermit God", Tuple.Create(16, 15, 1000)},
                {"Ghost Ship", Tuple.Create(16, 15, 1000)},
                {"Unknown Giant Golem", Tuple.Create(16, 15, 1000)},
                {"Evil Chicken God", Tuple.Create(20, 1, 1000)},
                {"Bonegrind The Butcher", Tuple.Create(20, 1, 1000)},
                {"Dreadstump the Pirate King", Tuple.Create(20, 1, 1000)},
                {"Arachna the Spider Queen", Tuple.Create(20, 1, 1000)},
                {"Stheno the Snake Queen", Tuple.Create(20, 1, 1000)},
                {"Mixcoatl the Masked God", Tuple.Create(20, 1, 1000)},
                {"Limon the Sprite God", Tuple.Create(20, 1, 1000)},
                {"Septavius the Ghost God", Tuple.Create(20, 1, 1000)},
                {"Davy Jones", Tuple.Create(20, 1, 1000)},
                {"Lord Ruthven", Tuple.Create(20, 1, 1000)},
                {"Archdemon Malphas", Tuple.Create(20, 1, 1000)},
                {"Elder Tree", Tuple.Create(20, 1, 1000)},
                {"Thessal the Mermaid Goddess", Tuple.Create(20, 1, 1000)},
                {"Dr. Terrible", Tuple.Create(20, 1, 1000)},
                {"Horrific Creation", Tuple.Create(20, 1, 1000)},
                {"Masked Party God", Tuple.Create(20, 1, 10000)},
                {"Stone Guardian Left", Tuple.Create(20, 1, 1000)},
                {"Stone Guardian Right", Tuple.Create(20, 1, 1000)},
                {"Oryx the Mad God 1", Tuple.Create(20, 1, 1000)},
                {"Oryx the Mad God 2", Tuple.Create(20, 1, 1000)},
            };
    }
}
