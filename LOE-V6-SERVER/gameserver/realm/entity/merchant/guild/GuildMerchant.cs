#region

using gameserver.realm.entity.player;

#endregion

namespace gameserver.realm.entity
{
    public class GuildMerchant : SellableObject
    {
        public const int UP1 = 0x0736;
        public const int UP1C = 10000;
        public const int UP2 = 0x0737;
        public const int UP2C = 100000;
        public const int UP3 = 0x0738;
        public const int UP3C = 250000;

        public GuildMerchant(RealmManager manager, ushort objType)
            : base(manager, objType)
        {
            RankReq = 0;
            Currency = CurrencyType.GuildFame;
            switch (objType)
            {
                case UP1:
                    Price = UP1C;
                    break;
                case UP2:
                    Price = UP2C;
                    break;
                case UP3:
                    Price = UP3C;
                    break;
            }
        }

        public override void Buy(Player player)
        {

        }
    }
}