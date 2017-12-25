#region

using System.Collections.Generic;
using System.Linq;
using gameserver.networking.incoming;
using gameserver.networking.outgoing;

#endregion

namespace gameserver.realm.entity.player
{
    partial class Player
    {
        public void RequestTrade(RealmTime time, REQUESTTRADE pkt)
        {
            var target = Owner.GetPlayerByName(pkt.Name);

            if (target == null)
            {
                SendInfo("{\"key\":\"server.player_not_found\",\"tokens\":{\"player\":\"" + pkt.Name + "\"}}");
                return;
            }
            if (!NameChosen || !target.NameChosen)
            {
                SendInfo("{\"key\":\"server.trade_needs_their_name\"}");
                return;
            }
            if (Client.Player == target)
            {
                SendInfo("{\"key\":\"server.self_trade\"}");
                return;
            }

            if (TradeManager.TradingPlayers.Count(_ => _.AccountId == target.AccountId) > 0)
            {
                SendInfo("{\"key\":\"server.they_already_trading\",\"tokens\":{\"player\":\"" + target.Name + "\"}}");
                return;
            }
            if (TradeManager.CurrentRequests.Count(_ => _.Value.AccountId == AccountId && _.Key.AccountId == target.AccountId) > 0)
            {
                var myItems = new TradeItem[12];
                var yourItems = new TradeItem[12];

                for (var i = 0; i < myItems.Length; i++)
                {
                    myItems[i] = new TradeItem
                    {
                        Item = Inventory[i] == null ? -1 : Inventory[i].ObjectType,
                        SlotType = SlotTypes[i],
                        Tradeable = (Inventory[i] != null && i >= 4) && (!Inventory[i].Soulbound),
                        Included = false
                    };
                }

                for (var i = 0; i < yourItems.Length; i++)
                {
                    yourItems[i] = new TradeItem
                    {
                        Item = target.Inventory[i] == null ? -1 : target.Inventory[i].ObjectType,
                        SlotType = SlotTypes[i],
                        Tradeable = (target.Inventory[i] != null && i >= 4) && (!target.Inventory[i].Soulbound),
                        Included = false
                    };
                }

                Client.SendMessage(new TRADESTART
                {
                    MyItems = myItems,
                    YourItems = yourItems,
                    YourName = target.Name
                });

                target.Client.SendMessage(new TRADESTART
                {
                    MyItems = yourItems,
                    YourItems = myItems,
                    YourName = Name
                });

                var t = new TradeManager(this, target);
                target.TradeHandler = t;
                TradeHandler = t;
            }
            else
            {
                SendInfo("{\"key\":\"server.trade_requested\",\"tokens\":{\"player\":\"" + target.Name + "\"}}");
                //todo
                //if (target.Ignored.Contains(Client.Account.AccountId)) return;
                target.Client.SendMessage(new TRADEREQUESTED
                {
                    Name = Name
                });
                var format = new KeyValuePair<Player, Player>(this, target);
                TradeManager.CurrentRequests.Add(format);

                Owner.Timers.Add(new WorldTimer(60 * 1000, (w, t) =>
                {
                    if (!TradeManager.CurrentRequests.Contains(format)) return;
                    TradeManager.CurrentRequests.Remove(format);
                    SendInfo("{\"key\":\"server.trade_timeout\"}");
                }));
            }
        }
    }
}