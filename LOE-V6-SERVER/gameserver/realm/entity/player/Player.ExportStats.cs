#region

using System.Collections.Generic;

#endregion

namespace gameserver.realm.entity.player
{
    partial class Player
    {
        protected override void ExportStats(IDictionary<StatsType, object> stats)
        {
            base.ExportStats(stats);
            stats[StatsType.AccountId] = AccountId;
            stats[StatsType.Name] = Name;

            stats[StatsType.Experience] = Experience - GetLevelExp(Level);
            stats[StatsType.ExperienceGoal] = ExperienceGoal;
            stats[StatsType.Level] = Level;

            stats[StatsType.CurrentFame] = CurrentFame;
            stats[StatsType.Fame] = Fame;
            stats[StatsType.FameGoal] = FameGoal;
            stats[StatsType.Stars] = Stars;

            stats[StatsType.Guild] = Guild;
            stats[StatsType.GuildRank] = GuildRank;

            stats[StatsType.Credits] = Credits;
            stats[StatsType.Tokens] = Tokens;
            stats[StatsType.NameChosen] = NameChosen ? 1 : 0;
            stats[StatsType.Texture1] = Texture1;
            stats[StatsType.Texture2] = Texture2;

            if (Glowing)
                stats[StatsType.Glowing] = 1;

            stats[StatsType.HP] = HP;
            stats[StatsType.MP] = Mp;

            stats[StatsType.Inventory0] = Inventory[0]?.ObjectType ?? -1;
            stats[StatsType.Inventory1] = Inventory[1]?.ObjectType ?? -1;
            stats[StatsType.Inventory2] = Inventory[2]?.ObjectType ?? -1;
            stats[StatsType.Inventory3] = Inventory[3]?.ObjectType ?? -1;
            stats[StatsType.Inventory4] = Inventory[4]?.ObjectType ?? -1;
            stats[StatsType.Inventory5] = Inventory[5]?.ObjectType ?? -1;
            stats[StatsType.Inventory6] = Inventory[6]?.ObjectType ?? -1;
            stats[StatsType.Inventory7] = Inventory[7]?.ObjectType ?? -1;
            stats[StatsType.Inventory8] = Inventory[8]?.ObjectType ?? -1;
            stats[StatsType.Inventory9] = Inventory[9]?.ObjectType ?? -1;
            stats[StatsType.Inventory10] = Inventory[10]?.ObjectType ?? -1;
            stats[StatsType.Inventory11] = Inventory[11]?.ObjectType ?? -1;

            if (Boost == null) CalcBoost();

            if (Boost != null)
            {
                stats[StatsType.MaximumHP] = Stats[0] + Boost[0];
                stats[StatsType.MaximumMP] = Stats[1] + Boost[1];
                stats[StatsType.Attack] = Stats[2] + Boost[2];
                stats[StatsType.Defense] = Stats[3] + Boost[3];
                stats[StatsType.Speed] = Stats[4] + Boost[4];
                stats[StatsType.Vitality] = Stats[5] + Boost[5];
                stats[StatsType.Wisdom] = Stats[6] + Boost[6];
                stats[StatsType.Dexterity] = Stats[7] + Boost[7];

                stats[StatsType.HPBoost] = Boost[0];
                stats[StatsType.MPBoost] = Boost[1];
                stats[StatsType.AttackBonus] = Boost[2];
                stats[StatsType.DefenseBonus] = Boost[3];
                stats[StatsType.SpeedBonus] = Boost[4];
                stats[StatsType.VitalityBonus] = Boost[5];
                stats[StatsType.WisdomBonus] = Boost[6];
                stats[StatsType.DexterityBonus] = Boost[7];
            }

            stats[StatsType.Size] = Resize16x16Skins.IsSkin16x16Type(PlayerSkin) ? 70 : setTypeSkin?.Size ?? Size;

            stats[StatsType.Has_Backpack] = HasBackpack.GetHashCode();

            stats[StatsType.Backpack0] = HasBackpack ? (Inventory[12]?.ObjectType ?? -1) : -1;
            stats[StatsType.Backpack1] = HasBackpack ? (Inventory[13]?.ObjectType ?? -1) : -1;
            stats[StatsType.Backpack2] = HasBackpack ? (Inventory[14]?.ObjectType ?? -1) : -1;
            stats[StatsType.Backpack3] = HasBackpack ? (Inventory[15]?.ObjectType ?? -1) : -1;
            stats[StatsType.Backpack4] = HasBackpack ? (Inventory[16]?.ObjectType ?? -1) : -1;
            stats[StatsType.Backpack5] = HasBackpack ? (Inventory[17]?.ObjectType ?? -1) : -1;
            stats[StatsType.Backpack6] = HasBackpack ? (Inventory[18]?.ObjectType ?? -1) : -1;
            stats[StatsType.Backpack7] = HasBackpack ? (Inventory[19]?.ObjectType ?? -1) : -1;

            stats[StatsType.Skin] = setTypeSkin?.SkinType ?? PlayerSkin;
            stats[StatsType.HealStackCount] = HealthPotions;
            stats[StatsType.MagicStackCount] = MagicPotions;

            if (Owner?.Name == "Ocean Trench")
                stats[StatsType.OxygenBar] = OxygenBar;

            stats[StatsType.XpBoosterActive] = XpBoosted ? 1 : 0;
            stats[StatsType.XpBoosterTime] = (int)XpBoostTimeLeft;
            stats[StatsType.LootDropBoostTimer] = (int)LootDropBoostTimeLeft;
            stats[StatsType.LootTierBoostTimer] = (int)LootTierBoostTimeLeft;

            stats[StatsType.AccountType] = (int)AccountType;
            stats[StatsType.Admin] = Admin;
        }
    }
}
