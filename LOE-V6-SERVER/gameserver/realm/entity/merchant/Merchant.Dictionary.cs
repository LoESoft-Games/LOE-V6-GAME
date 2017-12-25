using System;
using System.Collections.Generic;

namespace gameserver.realm.entity.merchant
{
    partial class Merchant
    {
        internal static class BLACKLIST
        {
            internal static readonly int[] keys =
            {
                1897, 12288, 12289, 12290, 29035, 3466, 538, 887, 2996, 2998, 1601, 2355, 5705, 3285, 1544, 1563, 1584, 1576, 3311,
                3133, 8848, 28645
            };

            internal static readonly string[] eggs =
            {
                ""
            };

            internal static readonly string[] weapons =
            {
                "Bow of Eternal Frost",
                "Frostbite",
                "Present Dispensing Wand",
                "An Icicle",
                "Staff of Yuletide Carols",
                "Salju"
            };

            internal static readonly string[] small =
            {
                "Small Ivory Dragon Scale Cloth",
                "Small Green Dragon Scale Cloth",
                "Small Midnight Dragon Scale Cloth",
                "Small Blue Dragon Scale Cloth",
                "Small Red Dragon Scale Cloth",
                "Small Jester Argyle Cloth",
                "Small Alchemist Cloth",
                "Small Mosaic Cloth",
                "Small Spooky Cloth",
                "Small Flame Cloth",
                "Small Heavy Chainmail Cloth"
            };

            internal static readonly string[] large =
            {
                "Large Ivory Dragon Scale Cloth",
                "Large Green Dragon Scale Cloth",
                "Large Midnight Dragon Scale Cloth",
                "Large Blue Dragon Scale Cloth",
                "Large Red Dragon Scale Cloth",
                "Large Jester Argyle Cloth",
                "Large Alchemist Cloth",
                "Large Mosaic Cloth",
                "Large Spooky Cloth",
                "Large Flame Cloth",
                "Large Heavy Chainmail Cloth"
            };
        }

        public static readonly Dictionary<int, Tuple<int, CurrencyType>> prices = new Dictionary<int, Tuple<int, CurrencyType>>
        {
            #region "Region 1 & 2"
            { 1793, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Undead Lair Key
            { 308, new Tuple<int, CurrencyType>(250, CurrencyType.Gold) }, // Halloween Cemetery Key
            { 1797, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Pirate Cave Key
            { 1798, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Spider Den Key
            { 1802, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Abyss of Demons Key
            { 1803, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Snake Pit Key
            { 1808, new Tuple<int, CurrencyType>(200, CurrencyType.Gold) }, // Tomb of the Ancients Key
            { 1823, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Sprite World Key
            { 3089, new Tuple<int, CurrencyType>(200, CurrencyType.Gold) }, // Ocean Trench Key
            { 3097, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Totem Key
            { 29836, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Ice Cave Key
            { 3107, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Manor Key
            { 3118, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Davy's Key
            { 3119, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Lab Key
            { 3170, new Tuple<int, CurrencyType>(200, CurrencyType.Gold) }, // Candy Key
            { 3183, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Cemetery Key
            { 3284, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Draconis Key
            { 3277, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Forest Maze Key
            { 3279, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Woodland Labyrinth Key
            { 3278, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Deadwater Docks Key
            { 3290, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // The Crawling Depths Key
            { 3293, new Tuple<int, CurrencyType>(200, CurrencyType.Gold) }, // Shatters Key
            { 8852, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Shaitan's Key
            { 9042, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Theatre Key
            { 29804, new Tuple<int, CurrencyType>(200, CurrencyType.Gold) }, // Puppet Master's Encore Key
            { 573, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Toxic Sewers Key
            { 283, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // The Hive Key
            { 32695, new Tuple<int, CurrencyType>(250, CurrencyType.Gold) }, // Ice Tomb Key
            { 303, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Mountain Temple Key
            #endregion "Region 1 & 2"
            #region "Region 4"
            { 3273, new Tuple<int, CurrencyType>(20, CurrencyType.Gold) }, // Soft Drink
            { 3275, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Fries
            { 3270, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Great Taco
            { 3269, new Tuple<int, CurrencyType>(150, CurrencyType.Gold) }, // Power Pizza
            { 3268, new Tuple<int, CurrencyType>(240, CurrencyType.Gold) }, // Chocolate Cream Sandwich Cookie
            { 3274, new Tuple<int, CurrencyType>(330, CurrencyType.Gold) }, // Grapes of Wrath
            { 3272, new Tuple<int, CurrencyType>(450, CurrencyType.Gold) }, // Superburger
            { 3271, new Tuple<int, CurrencyType>(700, CurrencyType.Gold) }, // Double Cheeseburger Deluxe
            { 3276, new Tuple<int, CurrencyType>(1000, CurrencyType.Gold) }, // Ambrosia
            { 3280, new Tuple<int, CurrencyType>(40, CurrencyType.Gold) }, // Cranberries
            { 3281, new Tuple<int, CurrencyType>(60, CurrencyType.Gold) }, // Ear of Corn
            { 3282, new Tuple<int, CurrencyType>(90, CurrencyType.Gold) }, // Sliced Yam
            { 3283, new Tuple<int, CurrencyType>(120, CurrencyType.Gold) }, // Pumpkin Pie
            { 3286, new Tuple<int, CurrencyType>(300, CurrencyType.Gold) } // Thanksgiving Turkey
            #endregion "Region 4"
        };

        public static readonly Dictionary<int, Tuple<int, CurrencyType>> accessory = new Dictionary<int, Tuple<int, CurrencyType>>
        {
            { 4352, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Alice Blue Accessory Dye
            { 4353, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Antique White Accessory Dye
            { 4354, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Aqua Accessory Dye
            { 4355, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Aquamarine Accessory Dye
            { 4356, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Azure Accessory Dye
            { 4357, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Beige Accessory Dye
            { 4358, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Bisque Accessory Dye
            { 4359, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Black Accessory Dye
            { 4360, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Blanched Almond Accessory Dye
            { 4361, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Blue Accessory Dye
            { 4362, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Blue Violet Accessory Dye
            { 4363, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Brown Accessory Dye
            { 4364, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Burly Wood Accessory Dye
            { 4365, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Cadet Blue Accessory Dye
            { 4366, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Chartreuse Accessory Dye
            { 4367, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Chocolate Accessory Dye
            { 4368, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Coral Accessory Dye
            { 4369, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Cornflower Blue Accessory Dye
            { 4370, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Cornsilk Accessory Dye
            { 4371, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Crimson Accessory Dye
            { 4372, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Cyan Accessory Dye
            { 4373, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Blue Accessory Dye
            { 4374, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Cyan Accessory Dye
            { 4375, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Golden Rod Accessory Dye
            { 4376, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Gray Accessory Dye
            { 4377, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Green Accessory Dye
            { 4378, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Khaki Accessory Dye
            { 4379, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Magenta Accessory Dye
            { 4380, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Olive Green Accessory Dye
            { 4381, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Orange Accessory Dye
            { 4382, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Orchid Accessory Dye
            { 4383, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Red Accessory Dye
            { 4384, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Salmon Accessory Dye
            { 4385, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Sea Green Accessory Dye
            { 4386, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Slate Blue Accessory Dye
            { 4387, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Slate Gray Accessory Dye
            { 4388, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Turquoise Accessory Dye
            { 4389, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Violet Accessory Dye
            { 4390, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Deep Pink Accessory Dye
            { 4391, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Deep Sky Blue Accessory Dye
            { 4392, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dim Gray Accessory Dye
            { 4393, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dodger Blue Accessory Dye
            { 4394, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Fire Brick Accessory Dye
            { 4395, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Floral White Accessory Dye
            { 4396, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Forest Green Accessory Dye
            { 4397, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Fuchsia Accessory Dye
            { 4398, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Gainsboro Accessory Dye
            { 4399, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Ghost White Accessory Dye
            { 4400, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Gold Accessory Dye
            { 4401, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Golden Rod Accessory Dye
            { 4402, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Gray Accessory Dye
            { 4403, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Green Accessory Dye
            { 4404, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Green Yellow Accessory Dye
            { 4405, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Honey Dew Accessory Dye
            { 4406, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Hot Pink Accessory Dye
            { 4407, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Indian Red Accessory Dye
            { 4408, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Indigo Accessory Dye
            { 4409, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Ivory Accessory Dye
            { 4410, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Khaki Accessory Dye
            { 4411, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Lavender Accessory Dye
            { 4412, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Lavender Blush Accessory Dye
            { 4413, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Lawn Green Accessory Dye
            { 4414, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Lemon Chiffon Accessory Dye
            { 4415, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Blue Accessory Dye
            { 4416, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Coral Accessory Dye
            { 4417, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Cyan Accessory Dye
            { 4418, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Golden Rod Yellow Accessory Dye
            { 4419, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Grey Accessory Dye
            { 4420, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Green Accessory Dye
            { 4421, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Pink Accessory Dye
            { 4422, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Salmon Accessory Dye
            { 4423, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Sea Green Accessory Dye
            { 4424, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Sky Blue Accessory Dye
            { 4425, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Slate Gray Accessory Dye
            { 4426, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Steel Blue Accessory Dye
            { 4427, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Yellow Accessory Dye
            { 4428, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Lime Accessory Dye
            { 4429, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Lime Green Accessory Dye
            { 4430, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Linen Accessory Dye
            { 4431, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Magenta Accessory Dye
            { 4432, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Maroon Accessory Dye
            { 4433, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Medium Aqua Marine Accessory Dye
            { 4434, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Medium Blue Accessory Dye
            { 4435, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Medium Orchid Accessory Dye
            { 4436, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Medium Purple Accessory Dye
            { 4437, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Medium Sea Green Accessory Dye
            { 4438, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Medium Slate Blue Accessory Dye
            { 4439, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Medium Spring Green Accessory Dye
            { 4440, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Medium Turquoise Accessory Dye
            { 4441, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Medium Violet Red Accessory Dye
            { 4442, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Midnight Blue Accessory Dye
            { 4443, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Mint Cream Accessory Dye
            { 4444, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Misty Rose Accessory Dye
            { 4445, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Moccasin Accessory Dye
            { 4446, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Navajo White Accessory Dye
            { 4447, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Navy Accessory Dye
            { 4448, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Old Lace Accessory Dye
            { 4449, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Olive Accessory Dye
            { 4450, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Olive Drab Accessory Dye
            { 4451, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Orange Accessory Dye
            { 4452, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Orange Red Accessory Dye
            { 4453, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Orchid Accessory Dye
            { 4454, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Pale Golden Rod Accessory Dye
            { 4455, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Pale Green Accessory Dye
            { 4456, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Pale Turquoise Accessory Dye
            { 4457, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Pale Violet Red Accessory Dye
            { 4458, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Papaya Whip Accessory Dye
            { 4459, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Peach Puff Accessory Dye
            { 4460, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Peru Accessory Dye
            { 4461, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Pink Accessory Dye
            { 4462, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Plum Accessory Dye
            { 4463, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Powder Blue Accessory Dye
            { 4464, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Purple Accessory Dye
            { 4465, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Red Accessory Dye
            { 4466, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Rosy Brown Accessory Dye
            { 4467, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Royal Blue Accessory Dye
            { 4468, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Saddle Brown Accessory Dye
            { 4469, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Salmon Accessory Dye
            { 4470, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Sandy Brown Accessory Dye
            { 4471, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Sea Green Accessory Dye
            { 4472, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Sea Shell Accessory Dye
            { 4473, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Sienna Accessory Dye
            { 4474, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Silver Accessory Dye
            { 4475, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Sky Blue Accessory Dye
            { 4476, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Slate Blue Accessory Dye
            { 4477, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Slate Gray Accessory Dye
            { 4478, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Snow Accessory Dye
            { 4479, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Spring Green Accessory Dye
            { 4480, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Steel Blue Accessory Dye
            { 4481, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Tan Accessory Dye
            { 4482, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Teal Accessory Dye
            { 4483, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Thistle Accessory Dye
            { 4484, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Tomato Accessory Dye
            { 4485, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Turquoise Accessory Dye
            { 4486, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Violet Accessory Dye
            { 4487, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Wheat Accessory Dye
            { 4488, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // White Accessory Dye
            { 4489, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // White Smoke Accessory Dye
            { 4490, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Yellow Accessory Dye
            { 4491, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Yellow Green Accessory Dye
            { 4492, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) } // St Patrick's Green Accessory Dye
        };

        public static readonly Dictionary<int, Tuple<int, CurrencyType>> clothing = new Dictionary<int, Tuple<int, CurrencyType>>
        {
            { 4096, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Alice Blue Clothing Dye
            { 4097, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Antique White Clothing Dye
            { 4098, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Aqua Clothing Dye
            { 4099, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Aquamarine Clothing Dye
            { 4100, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Azure Clothing Dye
            { 4101, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Beige Clothing Dye
            { 4102, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Bisque Clothing Dye
            { 4103, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Black Clothing Dye
            { 4104, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Blanched Almond Clothing Dye
            { 4105, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Blue Clothing Dye
            { 4106, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Blue Violet Clothing Dye
            { 4107, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Brown Clothing Dye
            { 4108, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Burly Wood Clothing Dye
            { 4109, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Cadet Blue Clothing Dye
            { 4110, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Chartreuse Clothing Dye
            { 4111, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Chocolate Clothing Dye
            { 4112, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Coral Clothing Dye
            { 4113, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Cornflower Blue Clothing Dye
            { 4114, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Cornsilk Clothing Dye
            { 4115, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Crimson Clothing Dye
            { 4116, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Cyan Clothing Dye
            { 4117, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Blue Clothing Dye
            { 4118, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Cyan Clothing Dye
            { 4119, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Golden Rod Clothing Dye
            { 4120, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Gray Clothing Dye
            { 4121, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Green Clothing Dye
            { 4122, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Khaki Clothing Dye
            { 4123, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Magenta Clothing Dye
            { 4124, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Olive Green Clothing Dye
            { 4125, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Orange Clothing Dye
            { 4126, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Orchid Clothing Dye
            { 4127, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Red Clothing Dye
            { 4128, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Salmon Clothing Dye
            { 4129, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Sea Green Clothing Dye
            { 4130, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Slate Blue Clothing Dye
            { 4131, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Slate Gray Clothing Dye
            { 4132, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Turquoise Clothing Dye
            { 4133, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dark Violet Clothing Dye
            { 4134, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Deep Pink Clothing Dye
            { 4135, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Deep Sky Blue Clothing Dye
            { 4136, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dim Gray Clothing Dye
            { 4137, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Dodger Blue Clothing Dye
            { 4138, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Fire Brick Clothing Dye
            { 4139, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Floral White Clothing Dye
            { 4140, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Forest Green Clothing Dye
            { 4141, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Fuchsia Clothing Dye
            { 4142, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Gainsboro Clothing Dye
            { 4143, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Ghost White Clothing Dye
            { 4144, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Gold Clothing Dye
            { 4145, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Golden Rod Clothing Dye
            { 4146, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Gray Clothing Dye
            { 4147, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Green Clothing Dye
            { 4148, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Green Yellow Clothing Dye
            { 4149, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Honey Dew Clothing Dye
            { 4150, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Hot Pink Clothing Dye
            { 4151, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Indian Red Clothing Dye
            { 4152, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Indigo Clothing Dye
            { 4153, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Ivory Clothing Dye
            { 4154, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Khaki Clothing Dye
            { 4155, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Lavender Clothing Dye
            { 4156, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Lavender Blush Clothing Dye
            { 4157, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Lawn Green Clothing Dye
            { 4158, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Lemon Chiffon Clothing Dye
            { 4159, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Blue Clothing Dye
            { 4160, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Coral Clothing Dye
            { 4161, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Cyan Clothing Dye
            { 4162, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Golden Rod Yellow Clothing Dye
            { 4163, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Grey Clothing Dye
            { 4164, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Green Clothing Dye
            { 4165, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Pink Clothing Dye
            { 4166, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Salmon Clothing Dye
            { 4167, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Sea Green Clothing Dye
            { 4168, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Sky Blue Clothing Dye
            { 4169, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Slate Gray Clothing Dye
            { 4170, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Steel Blue Clothing Dye
            { 4171, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Light Yellow Clothing Dye
            { 4172, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Lime Clothing Dye
            { 4173, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Lime Green Clothing Dye
            { 4174, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Linen Clothing Dye
            { 4175, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Magenta Clothing Dye
            { 4176, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Maroon Clothing Dye
            { 4177, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Medium Aqua Marine Clothing Dye
            { 4178, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Medium Blue Clothing Dye
            { 4179, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Medium Orchid Clothing Dye
            { 4180, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Medium Purple Clothing Dye
            { 4181, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Medium Sea Green Clothing Dye
            { 4182, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Medium Slate Blue Clothing Dye
            { 4183, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Medium Spring Green Clothing Dye
            { 4184, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Medium Turquoise Clothing Dye
            { 4185, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Medium Violet Red Clothing Dye
            { 4186, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Midnight Blue Clothing Dye
            { 4187, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Mint Cream Clothing Dye
            { 4188, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Misty Rose Clothing Dye
            { 4189, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Moccasin Clothing Dye
            { 4190, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Navajo White Clothing Dye
            { 4191, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Navy Clothing Dye
            { 4192, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Old Lace Clothing Dye
            { 4193, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Olive Clothing Dye
            { 4194, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Olive Drab Clothing Dye
            { 4195, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Orange Clothing Dye
            { 4196, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Orange Red Clothing Dye
            { 4197, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Orchid Clothing Dye
            { 4198, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Pale Golden Rod Clothing Dye
            { 4199, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Pale Green Clothing Dye
            { 4200, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Pale Turquoise Clothing Dye
            { 4201, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Pale Violet Red Clothing Dye
            { 4202, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Papaya Whip Clothing Dye
            { 4203, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Peach Puff Clothing Dye
            { 4204, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Peru Clothing Dye
            { 4205, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Pink Clothing Dye
            { 4206, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Plum Clothing Dye
            { 4207, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Powder Blue Clothing Dye
            { 4208, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Purple Clothing Dye
            { 4209, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Red Clothing Dye
            { 4210, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Rosy Brown Clothing Dye
            { 4211, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Royal Blue Clothing Dye
            { 4212, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Saddle Brown Clothing Dye
            { 4213, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Salmon Clothing Dye
            { 4214, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Sandy Brown Clothing Dye
            { 4215, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Sea Green Clothing Dye
            { 4216, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Sea Shell Clothing Dye
            { 4217, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Sienna Clothing Dye
            { 4218, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Silver Clothing Dye
            { 4219, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Sky Blue Clothing Dye
            { 4220, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Slate Blue Clothing Dye
            { 4221, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Slate Gray Clothing Dye
            { 4222, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Snow Clothing Dye
            { 4223, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Spring Green Clothing Dye
            { 4224, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Steel Blue Clothing Dye
            { 4225, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Tan Clothing Dye
            { 4226, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Teal Clothing Dye
            { 4227, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Thistle Clothing Dye
            { 4228, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Tomato Clothing Dye
            { 4229, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Turquoise Clothing Dye
            { 4230, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Violet Clothing Dye
            { 4231, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Wheat Clothing Dye
            { 4232, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // White Clothing Dye
            { 4233, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // White Smoke Clothing Dye
            { 4234, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Yellow Clothing Dye
            { 4235, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) }, // Yellow Green Clothing Dye
            { 4236, new Tuple<int, CurrencyType>(50, CurrencyType.Gold) } // St Patrick's Green Clothing Dye
        };

        public static readonly Dictionary<int, Tuple<int, CurrencyType>> small = new Dictionary<int, Tuple<int, CurrencyType>>
        {
            { 4864, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Purple Pinstripe Cloth
            { 4865, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Brown Lined Cloth
            { 4866, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Blue Striped Cloth
            { 4867, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Black Striped Cloth
            { 4868, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Rainbow Cloth
            { 4869, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Starry Cloth
            { 4870, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Brown Stitch Cloth
            { 4871, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Tan Diamond Cloth
            { 4872, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Green Weave Cloth
            { 4873, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Blue Wave Cloth
            { 4874, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Yellow Wire Cloth
            { 4875, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Futuristic Cloth
            { 4876, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Stony Cloth
            { 4877, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Heart Cloth
            { 4878, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Skull Cloth
            { 4879, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Red Diamond Cloth
            { 4880, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Jester Cloth
            { 4881, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Crossbox Cloth
            { 4882, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small White Diamond Cloth
            { 4883, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Grey Scaly Cloth
            { 4884, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Red Spotted Cloth
            { 4885, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Smiley Cloth
            { 4886, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Bold Diamond Cloth
            { 4887, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Blue Lace Cloth
            { 4888, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Loud Spotted Cloth
            { 4889, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Red Weft Cloth
            { 4890, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Pink Sparkly Cloth
            { 4891, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Red Lace Cloth
            { 4892, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Pink Maze Cloth
            { 4893, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Yellow Dot Cloth
            { 4894, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Cloud Cloth
            { 4895, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Glowthread Cloth
            { 4896, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Sweater Cloth
            { 4897, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Bee Stripe Cloth
            { 4898, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Western Stripe Cloth
            { 4899, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Blue Point Cloth
            { 4900, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Robber Cloth
            { 4901, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Chainmail Cloth
            { 4902, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Dark Blue Stripe Cloth
            { 4903, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Vine Cloth
            { 4904, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Party Cloth
            { 4905, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Viva Cloth
            { 4906, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Nautical Cloth
            { 4907, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Cactus Zag Cloth
            { 4908, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Big-Stripe Blue Cloth
            { 4909, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Big-Stripe Red Cloth
            { 4910, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Starry Night Cloth
            { 4911, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Lemon-Lime Cloth
            { 4912, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Floral Cloth
            { 4913, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Pink Dot Cloth
            { 4914, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Dark Eyes Cloth
            { 4915, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Wind Cloth
            { 4916, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Shamrock Cloth
            { 4917, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Bright Stripes Cloth
            { 4918, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small USA Flag Cloth
            { 4919, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Flannel Cloth
            { 4920, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Cow Print Cloth
            { 4921, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Lush Camo Cloth
            { 4922, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Dark Camo Cloth
            { 4923, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Teal Crystal Cloth
            { 4924, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Blue Fireworks Cloth
            { 4925, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Crisscross Cloth
            { 4926, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Diamond Cloth
            { 4927, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Egyptian Cloth
            { 4928, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Purple Bones Cloth
            { 4929, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Plaid Cloth
            { 4930, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Red USA Star Cloth
            { 4931, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Blue USA Star Cloth
            { 4932, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small USA Star Cloth
            { 4933, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Purple Stripes Cloth
            { 4934, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Bright Floral Cloth
            { 4935, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Clanranald Cloth
            { 4936, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small American Flag Cloth
            { 4937, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Relief Cloth
            { 4938, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Intense Clovers Cloth
            { 4939, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Celtic Knot Cloth
            { 4942, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Leopard Print Cloth
            { 4943, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Zebra Print Cloth
            { 4944, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Colored Egg Cloth
            { 4945, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Spring Cloth
            { 4946, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Hibiscus Beach Wrap Cloth
            { 4947, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Small Blue Camo Cloth
            { 4948, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) } // Small Sunburst Cloth
        };

        public static readonly Dictionary<int, Tuple<int, CurrencyType>> large = new Dictionary<int, Tuple<int, CurrencyType>>
        {
            { 4608, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Purple Pinstripe Cloth
            { 4609, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Brown Lined Cloth
            { 4610, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Blue Striped Cloth
            { 4611, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Black Striped Cloth
            { 4612, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Rainbow Cloth
            { 4613, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Starry Cloth
            { 4614, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Brown Stitch Cloth
            { 4615, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Tan Diamond Cloth
            { 4616, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Green Weave Cloth
            { 4617, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Blue Wave Cloth
            { 4618, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Yellow Wire Cloth
            { 4619, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Futuristic Cloth
            { 4620, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Stony Cloth
            { 4621, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Heart Cloth
            { 4622, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Skull Cloth
            { 4623, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Red Diamond Cloth
            { 4624, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Jester Cloth
            { 4625, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Crossbox Cloth
            { 4626, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large White Diamond Cloth
            { 4627, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Grey Scaly Cloth
            { 4628, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Red Spotted Cloth
            { 4629, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Smiley Cloth
            { 4630, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Bold Diamond Cloth
            { 4631, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Blue Lace Cloth
            { 4632, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Loud Spotted Cloth
            { 4633, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Red Weft Cloth
            { 4634, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Pink Sparkly Cloth
            { 4635, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Red Lace Cloth
            { 4636, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Pink Maze Cloth
            { 4637, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Yellow Dot Cloth
            { 4638, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Cloud Cloth
            { 4639, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Glowthread Cloth
            { 4640, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Sweater Cloth
            { 4641, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Bee Stripe Cloth
            { 4642, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Western Stripe Cloth
            { 4643, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Blue Point Cloth
            { 4644, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Robber Cloth
            { 4645, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Chainmail Cloth
            { 4646, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Dark Blue Stripe Cloth
            { 4647, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Vine Cloth
            { 4648, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Party Cloth
            { 4649, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Viva Cloth
            { 4650, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Nautical Cloth
            { 4651, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Cactus Zag Cloth
            { 4652, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Big-Stripe Blue Cloth
            { 4653, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Big-Stripe Red Cloth
            { 4654, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Starry Night Cloth
            { 4655, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Lemon-Lime Cloth
            { 4656, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Floral Cloth
            { 4657, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Pink Dot Cloth
            { 4658, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Dark Eyes Cloth
            { 4659, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Wind Cloth
            { 4660, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Shamrock Cloth
            { 4661, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Bright Stripes Cloth
            { 4662, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large USA Flag Cloth
            { 4663, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Flannel Cloth
            { 4664, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Cow Print Cloth
            { 4665, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Lush Camo Cloth
            { 4666, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Dark Camo Cloth
            { 4667, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Teal Crystal Cloth
            { 4668, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Blue Fireworks Cloth
            { 4669, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Crisscross Cloth
            { 4670, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Diamond Cloth
            { 4671, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Egyptian Cloth
            { 4672, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Purple Bones Cloth
            { 4673, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Plaid Cloth
            { 4674, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Red USA Star Cloth
            { 4675, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Blue USA Star Cloth
            { 4676, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large USA Star Cloth
            { 4677, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Purple Stripes Cloth
            { 4678, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Bright Floral Cloth
            { 4679, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Clanranald Cloth
            { 4680, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large American Flag Cloth
            { 4681, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Relief Cloth
            { 4682, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Intense Clovers Cloth
            { 4683, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Celtic Knot Cloth
            { 4686, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Leopard Print Cloth
            { 4687, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Zebra Print Cloth
            { 4688, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Colored Egg Cloth
            { 4689, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Spring Cloth
            { 4690, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Hibiscus Beach Wrap Cloth
            { 4691, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Large Blue Camo Cloth
            { 4692, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) } // Large Sunburst Cloth
        };

        public static readonly Dictionary<int, Tuple<int, CurrencyType>> region5 = new Dictionary<int, Tuple<int, CurrencyType>>
        {
            { 3206, new Tuple<int, CurrencyType>(300, CurrencyType.Gold) }, // Uncommon Feline Egg
            { 3207, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold) }, // Rare Feline Egg
            { 3208, new Tuple<int, CurrencyType>(3500, CurrencyType.Gold) }, // Legendary Feline Egg
            { 3210, new Tuple<int, CurrencyType>(300, CurrencyType.Gold) }, // Uncommon Canine Egg
            { 3211, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold) }, // Rare Canine Egg
            { 3212, new Tuple<int, CurrencyType>(3500, CurrencyType.Gold) }, // Legendary Canine Egg
            { 3214, new Tuple<int, CurrencyType>(300, CurrencyType.Gold) }, // Uncommon Avian Egg
            { 3215, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold) }, // Rare Avian Egg
            { 3216, new Tuple<int, CurrencyType>(3500, CurrencyType.Gold) }, // Legendary Avian Egg
            { 3218, new Tuple<int, CurrencyType>(300, CurrencyType.Gold) }, // Uncommon Exotic Egg
            { 3219, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold) }, // Rare Exotic Egg
            { 3220, new Tuple<int, CurrencyType>(3500, CurrencyType.Gold) }, // Legendary Exotic Egg
            { 3222, new Tuple<int, CurrencyType>(300, CurrencyType.Gold) }, // Uncommon Farm Egg
            { 3223, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold) }, // Rare Farm Egg
            { 3224, new Tuple<int, CurrencyType>(3500, CurrencyType.Gold) }, // Legendary Farm Egg
            { 3226, new Tuple<int, CurrencyType>(300, CurrencyType.Gold) }, // Uncommon Woodland Egg
            { 3227, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold) }, // Rare Woodland Egg
            { 3228, new Tuple<int, CurrencyType>(3500, CurrencyType.Gold) }, // Legendary Woodland Egg
            { 3230, new Tuple<int, CurrencyType>(300, CurrencyType.Gold) }, // Uncommon Reptile Egg
            { 3231, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold) }, // Rare Reptile Egg
            { 3232, new Tuple<int, CurrencyType>(3500, CurrencyType.Gold) }, // Legendary Reptile Egg
            { 3234, new Tuple<int, CurrencyType>(300, CurrencyType.Gold) }, // Uncommon Insect Egg
            { 3235, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold) }, // Rare Insect Egg
            { 3236, new Tuple<int, CurrencyType>(3500, CurrencyType.Gold) }, // Legendary Insect Egg
            { 3238, new Tuple<int, CurrencyType>(300, CurrencyType.Gold) }, // Uncommon Penguin Egg
            { 3239, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold) }, // Rare Penguin Egg
            { 3240, new Tuple<int, CurrencyType>(3500, CurrencyType.Gold) }, // Legendary Penguin Egg
            { 3242, new Tuple<int, CurrencyType>(300, CurrencyType.Gold) }, // Uncommon Aquatic Egg
            { 3243, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold) }, // Rare Aquatic Egg
            { 3244, new Tuple<int, CurrencyType>(3500, CurrencyType.Gold) }, // Legendary Aquatic Egg
            { 3246, new Tuple<int, CurrencyType>(300, CurrencyType.Gold) }, // Uncommon Spooky Egg
            { 3247, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold) }, // Rare Spooky Egg
            { 3248, new Tuple<int, CurrencyType>(3500, CurrencyType.Gold) }, // Legendary Spooky Egg
            { 3250, new Tuple<int, CurrencyType>(300, CurrencyType.Gold) }, // Uncommon Humanoid Egg
            { 3251, new Tuple<int, CurrencyType>(2000, CurrencyType.Gold) }, // Rare Humanoid Egg
            { 3252, new Tuple<int, CurrencyType>(3500, CurrencyType.Gold) }, // Legendary Humanoid Egg
            { 3254, new Tuple<int, CurrencyType>(300, CurrencyType.Gold) }, // Uncommon ???? Egg
            { 3255, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold) }, // Rare ???? Egg
            { 3256, new Tuple<int, CurrencyType>(3500, CurrencyType.Gold) }, // Legendary ???? Egg
            { 3258, new Tuple<int, CurrencyType>(300, CurrencyType.Gold) }, // Uncommon Automaton Egg
            { 3259, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold) }, // Rare Automaton Egg
            { 3260, new Tuple<int, CurrencyType>(3500, CurrencyType.Gold) }, // Legendary Automaton Egg
            { 3262, new Tuple<int, CurrencyType>(300, CurrencyType.Gold) }, // Uncommon Mystery Egg
            { 3263, new Tuple<int, CurrencyType>(1000, CurrencyType.Gold) }, // Rare Mystery Egg
            { 3264, new Tuple<int, CurrencyType>(3500, CurrencyType.Gold) } // Legendary Mystery Egg
        };

        public static readonly Dictionary<int, Tuple<int, CurrencyType>> region6 = new Dictionary<int, Tuple<int, CurrencyType>>
        {
            { 2572, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Mithril Shield
            { 2850, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Colossus Shield
            { 2608, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Magic Nova Spell
            { 2852, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Elemental Detonation Spell
            { 2651, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Tome of Divine Favor
            { 2853, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Tome of Holy Guidance
            { 2645, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Seal of the Holy Warrior
            { 2854, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Seal of the Blessed Champion
            { 2785, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Cloak of Endless Twilight
            { 2855, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Cloak of Ghostly Concealment
            { 2661, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Golden Quiver
            { 2856, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Quiver of Elvish Mastery
            { 2667, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Golden Helm
            { 2857, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Helm of the Great General
            { 2728, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Nightwing Venom
            { 2858, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Baneserpent Poison
            { 2735, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Lifedrinker Skull
            { 2859, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Bloodsucker Skull
            { 2742, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Dragonstalker Trap
            { 2860, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Giantcatcher Trap
            { 2630, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Banishment Orb
            { 2861, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Planefetter Orb
            { 2848, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Prism of Phantoms
            { 2851, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Prism of Apparitions
            { 2866, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Scepter of Skybolts
            { 2867, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Scepter of Storms
            { 3160, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Ice Star
            { 3161, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) } // Doom Circle
        };

        public static readonly Dictionary<int, Tuple<int, CurrencyType>> region7 = new Dictionary<int, Tuple<int, CurrencyType>>
        {
            { 2690, new Tuple<int, CurrencyType>(51, CurrencyType.Gold) }, // Ravenheart Sword
            { 2691, new Tuple<int, CurrencyType>(150, CurrencyType.Gold) }, // Dragonsoul Sword
            { 2692, new Tuple<int, CurrencyType>(225, CurrencyType.Gold) }, // Archon Sword
            { 2631, new Tuple<int, CurrencyType>(450, CurrencyType.Gold) }, // Skysplitter Sword
            { 2827, new Tuple<int, CurrencyType>(550, CurrencyType.Gold) }, // Sword of Acclaim
            { 2567, new Tuple<int, CurrencyType>(51, CurrencyType.Gold) }, // Wand of Death
            { 2693, new Tuple<int, CurrencyType>(150, CurrencyType.Gold) }, // Wand of Deep Sorcery
            { 2694, new Tuple<int, CurrencyType>(225, CurrencyType.Gold) }, // Wand of Shadow
            { 2695, new Tuple<int, CurrencyType>(450, CurrencyType.Gold) }, // Wand of Ancient Warning
            { 2806, new Tuple<int, CurrencyType>(550, CurrencyType.Gold) }, // Wand of Recompense
            { 2585, new Tuple<int, CurrencyType>(51, CurrencyType.Gold) }, // Fire Dagger
            { 2696, new Tuple<int, CurrencyType>(150, CurrencyType.Gold) }, // Ragetalon Dagger
            { 2697, new Tuple<int, CurrencyType>(225, CurrencyType.Gold) }, // Emeraldshard Dagger
            { 2698, new Tuple<int, CurrencyType>(450, CurrencyType.Gold) }, // Agateclaw Dagger
            { 2815, new Tuple<int, CurrencyType>(650, CurrencyType.Gold) }, // Dagger of Foul Malevolence
            { 2590, new Tuple<int, CurrencyType>(51, CurrencyType.Gold) }, // Golden Bow
            { 2699, new Tuple<int, CurrencyType>(150, CurrencyType.Gold) }, // Verdant Bow
            { 2700, new Tuple<int, CurrencyType>(225, CurrencyType.Gold) }, // Bow of Fey Magic
            { 2701, new Tuple<int, CurrencyType>(450, CurrencyType.Gold) }, // Bow of Innocent Blood
            { 2818, new Tuple<int, CurrencyType>(600, CurrencyType.Gold) }, // Bow of Covert Havens
            { 2719, new Tuple<int, CurrencyType>(51, CurrencyType.Gold) }, // Staff of Horror
            { 2720, new Tuple<int, CurrencyType>(150, CurrencyType.Gold) }, // Staff of Necrotic Arcana
            { 2721, new Tuple<int, CurrencyType>(225, CurrencyType.Gold) }, // Staff of Diabolic Secrets
            { 2722, new Tuple<int, CurrencyType>(450, CurrencyType.Gold) }, // Staff of Astral Knowledge
            { 2824, new Tuple<int, CurrencyType>(900, CurrencyType.Gold) }, // Staff of the Cosmic Whole
            { 3148, new Tuple<int, CurrencyType>(51, CurrencyType.Gold) }, // Demon Edge
            { 3149, new Tuple<int, CurrencyType>(150, CurrencyType.Gold) }, // Jewel Eye Katana
            { 3150, new Tuple<int, CurrencyType>(225, CurrencyType.Gold) }, // Ichimonji
            { 3151, new Tuple<int, CurrencyType>(450, CurrencyType.Gold) }, // Muramasa
            { 3152, new Tuple<int, CurrencyType>(700, CurrencyType.Gold) } // Masamune
        };

        public static readonly Dictionary<int, Tuple<int, CurrencyType>> region8 = new Dictionary<int, Tuple<int, CurrencyType>>
        {
            { 2771, new Tuple<int, CurrencyType>(51, CurrencyType.Gold) }, // Drake Hide Armor
            { 2702, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Roc Leather Armor
            { 2703, new Tuple<int, CurrencyType>(225, CurrencyType.Gold) }, // Hippogriff Hide Armor
            { 2704, new Tuple<int, CurrencyType>(425, CurrencyType.Gold) }, // Griffon Hide Armor
            { 2809, new Tuple<int, CurrencyType>(800, CurrencyType.Gold) }, // Hydra Skin Armor
            { 2579, new Tuple<int, CurrencyType>(51, CurrencyType.Gold) }, // Dragonscale Armor
            { 2705, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Desolation Armor
            { 2706, new Tuple<int, CurrencyType>(225, CurrencyType.Gold) }, // Vengeance Armor
            { 2707, new Tuple<int, CurrencyType>(425, CurrencyType.Gold) }, // Abyssal Armor
            { 2812, new Tuple<int, CurrencyType>(850, CurrencyType.Gold) }, // Acropolis Armor
            { 2751, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Ring of Paramount Attack
            { 2752, new Tuple<int, CurrencyType>(250, CurrencyType.Gold) }, // Ring of Paramount Defense
            { 2753, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Ring of Paramount Speed
            { 2754, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Ring of Paramount Vitality
            { 2755, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Ring of Paramount Wisdom
            { 2756, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Ring of Paramount Dexterity
            { 2757, new Tuple<int, CurrencyType>(250, CurrencyType.Gold) }, // Ring of Paramount Health
            { 2758, new Tuple<int, CurrencyType>(250, CurrencyType.Gold) }, // Ring of Paramount Magic
            { 2759, new Tuple<int, CurrencyType>(200, CurrencyType.Gold) }, // Ring of Exalted Attack
            { 2760, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Ring of Exalted Defense
            { 2761, new Tuple<int, CurrencyType>(200, CurrencyType.Gold) }, // Ring of Exalted Speed
            { 2762, new Tuple<int, CurrencyType>(200, CurrencyType.Gold) }, // Ring of Exalted Vitality
            { 2763, new Tuple<int, CurrencyType>(200, CurrencyType.Gold) }, // Ring of Exalted Wisdom
            { 2764, new Tuple<int, CurrencyType>(200, CurrencyType.Gold) }, // Ring of Exalted Dexterity
            { 2765, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Ring of Exalted Health
            { 2766, new Tuple<int, CurrencyType>(400, CurrencyType.Gold) }, // Ring of Exalted Magic
            { 2656, new Tuple<int, CurrencyType>(51, CurrencyType.Gold) }, // Robe of the Master
            { 2708, new Tuple<int, CurrencyType>(100, CurrencyType.Gold) }, // Robe of the Shadow Magus
            { 2709, new Tuple<int, CurrencyType>(225, CurrencyType.Gold) }, // Robe of the Moon Wizard
            { 2710, new Tuple<int, CurrencyType>(425, CurrencyType.Gold) }, // Robe of the Elder Warlock
            { 2821, new Tuple<int, CurrencyType>(850, CurrencyType.Gold) } // Robe of the Grand Sorcerer
        };
    }
}