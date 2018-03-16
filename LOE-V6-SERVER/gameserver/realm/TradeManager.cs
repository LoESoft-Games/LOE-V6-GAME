#region

using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using gameserver.networking.incoming;
using gameserver.networking.outgoing;
using gameserver.realm.entity.player;

#endregion

namespace gameserver.realm
{
    public class TradeManager
    {
        private readonly static ILog log = LogManager.GetLogger(typeof(TradeManager));
        public static List<KeyValuePair<Player, Player>> CurrentRequests { get; }
        public static List<Player> TradingPlayers { get; }

        private readonly Player player1, player2;

        private bool player1Accept;
        private bool player2Accept;
        private bool finished;

        private bool[] player1Trades;
        private bool[] player2Trades;

        static TradeManager()
        {
            CurrentRequests = new List<KeyValuePair<Player, Player>>();
            TradingPlayers = new List<Player>();
        }

        public TradeManager(Player player1, Player player2)
        {
            this.player1Trades = new bool[12];
            this.player2Trades = new bool[12];
            this.player1 = player1;
            this.player2 = player2;
            TradingPlayers.Add(player1);
            TradingPlayers.Add(player2);
            if (CurrentRequests.Contains(new KeyValuePair<Player, Player>(player1, player2)))
                CurrentRequests.Remove(new KeyValuePair<Player, Player>(player1, player2));
            if (CurrentRequests.Contains(new KeyValuePair<Player, Player>(player2, player1)))
                CurrentRequests.Remove(new KeyValuePair<Player, Player>(player2, player1));
        }

        public void TradeChanged(Player sender, bool[] changes)
        {
            if (sender == player1)
            {
                if (changes != player1Trades)
                {
                    ResetAccept();

                    for (int i = 0; i < changes.Length; i++)
                    {
                        if (sender.Inventory[i] != null)
                        {
                            if (sender.Inventory[i].Soulbound || i < 4)
                                player1Trades[i] = false;
                            else
                                player1Trades[i] = changes[i];
                        }
                        else
                            player1Trades[i] = false;
                    }
                    player2.Client.SendMessage(new TRADECHANGED { Offers = player1Trades });
                }
            }
            else
            {
                if (changes != player2Trades)
                {
                    ResetAccept();

                    for (int i = 0; i < changes.Length; i++)
                    {
                        if (sender.Inventory[i] != null)
                        {
                            if (sender.Inventory[i].Soulbound || i < 4)
                                player2Trades[i] = false;
                            else
                                player2Trades[i] = changes[i];
                        }
                        else
                            player2Trades[i] = false;
                    }
                    player1.Client.SendMessage(new TRADECHANGED { Offers = player2Trades });
                }
            }
        }

        public void CancelTrade(Player sender)
        {
            if (sender == player1)
            {
                player2.Client.SendMessage(new TRADEDONE
                {
                    Result = 1,
                    Message = "{\"key\":\"server.cancelled_trade\",\"tokens\":{\"player\":\"" + sender.Name + "\"}}"
                });
            }
            else
            {
                player1.Client.SendMessage(new TRADEDONE
                {
                    Result = 1,
                    Message = "{\"key\":\"server.cancelled_trade\",\"tokens\":{\"player\":\"" + sender.Name + "\"}}"
                });
            }

            TradingPlayers.Remove(player1);
            TradingPlayers.Remove(player2);

            Destroy();
        }

        public void AcceptTrade(Player sender, ACCEPTTRADE pkt)
        {
            if (sender == player1)
            {
                if (pkt.MyOffers.SequenceEqual(player1Trades) && pkt.YourOffers.SequenceEqual(player2Trades))
                {
                    player2.Client.SendMessage(new TRADEACCEPTED
                    {
                        MyOffers = player2Trades,
                        YourOffers = player1Trades
                    });
                    player1Accept = true;
                }
            }
            else
            {
                if (pkt.MyOffers.SequenceEqual(player2Trades) && pkt.YourOffers.SequenceEqual(player1Trades))
                {
                    player1.Client.SendMessage(new TRADEACCEPTED
                    {
                        MyOffers = player1Trades,
                        YourOffers = player2Trades
                    });
                    player2Accept = true;
                }
            }

            if (player1Accept && player2Accept)
                Trade();
        }

        public void Tick(RealmTime time)
        {
            try
            {
                if (!finished)
                {
                    if (player1.Owner == null)
                    {
                        player2.Client.SendMessage(new TRADEDONE
                        {
                            Result = 1,
                            Message = "{\"key\":\"server.trade_other_left\"}"
                        });
                        finished = true;
                        TradingPlayers.Remove(player1);
                        TradingPlayers.Remove(player2);
                    }
                    else if (player2.Owner == null)
                    {
                        player1.Client.SendMessage(new TRADEDONE
                        {
                            Result = 1,
                            Message = "{\"key\":\"server.trade_other_left\"}"
                        });
                        finished = true;
                        TradingPlayers.Remove(player1);
                        TradingPlayers.Remove(player2);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void Trade()
        {
            if (!InventoryFull())
            {
                List<Item> toTakeFromPlayer1 = new List<Item>();
                List<Item> toTakeFromPlayer2 = new List<Item>();

                for (int i = 0; i < player1Trades.Length; i++)
                {
                    if (player1Trades[i])
                    {
                        toTakeFromPlayer1.Add(player1.Inventory[i]);
                        player1.Inventory[i] = null;
                    }
                }

                for (int i = 0; i < player2Trades.Length; i++)
                {
                    if (player2Trades[i])
                    {
                        toTakeFromPlayer2.Add(player2.Inventory[i]);
                        player2.Inventory[i] = null;
                    }
                }

                for (int i = 0; i < 12; i++)
                {
                    if (player1.Inventory[i] == null)
                    {
                        foreach (var item in toTakeFromPlayer2)
                        {
                            if (player1.SlotTypes[i] != 10 && player1.SlotTypes[i] != item.SlotType) continue;
                            else
                            {
                                player1.Inventory[i] = item;
                                toTakeFromPlayer2.Remove(item);
                                break;
                            }
                        }
                    }
                }

                for (int i = 0; i < 12; i++)
                {
                    if (player2.Inventory[i] == null)
                    {
                        foreach (var item in toTakeFromPlayer1)
                        {
                            if (player2.SlotTypes[i] != 10 && player2.SlotTypes[i] != item.SlotType) continue;
                            else
                            {
                                player2.Inventory[i] = item;
                                toTakeFromPlayer1.Remove(item);
                                break;
                            }
                        }
                    }
                }

                TradeDone();
            }
            else
                TradeError();

            player1.SaveToCharacter();
            player2.SaveToCharacter();
        }

        private void TradeError()
        {
            TRADEDONE packet = new TRADEDONE
            {
                Result = 1,
                Message = "{\"key\":\"server.trade_error\"}"
            };

            finished = true;
            TradingPlayers.Remove(player1);
            TradingPlayers.Remove(player2);

            player1.Client.SendMessage(packet);
            player2.Client.SendMessage(packet);
        }

        private void TradeDone()
        {
            TRADEDONE packet = new TRADEDONE
            {
                Result = 1,
                Message = "{\"key\":\"server.trade_successful\"}"
            };

            player1.Client.SendMessage(packet);
            player2.Client.SendMessage(packet);

            player1.UpdateCount++;
            player2.UpdateCount++;

            TradingPlayers.Remove(player1);
            TradingPlayers.Remove(player2);
            finished = true;
        }

        private bool InventoryFull() => (player1.Inventory.Count(_ => _ == null) > player2Trades.Length) && (player2.Inventory.Count(_ => _ == null) > player1Trades.Length);

        private void ResetAccept()
        {
            player1Accept = false;
            player2Accept = false;
        }

        public void Destroy()
        {
            player1.TradeCanceled();
            player2.TradeCanceled();
        }
    }
}
