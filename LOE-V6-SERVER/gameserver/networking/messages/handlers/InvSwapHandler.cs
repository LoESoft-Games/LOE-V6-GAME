#region

using System.Collections.Generic;
using gameserver.networking.incoming;
using gameserver.networking.outgoing;
using gameserver.realm;
using gameserver.realm.entity;
using gameserver.realm.entity.player;
using gameserver.realm.world;
using System.Linq;

#endregion

namespace gameserver.networking.handlers
{
    internal class InvSwapHandler : MessageHandlers<INVSWAP>
    {
        public override MessageID ID => MessageID.INVSWAP;

        protected override void HandlePacket(Client client, INVSWAP packet)
        {
            if (client.Player.Owner == null) return;
            client.Manager.Logic.AddPendingAction(t =>
            {
                Entity en1 = client.Player.Owner.GetEntity(packet.SlotObject1.ObjectId);
                Entity en2 = client.Player.Owner.GetEntity(packet.SlotObject2.ObjectId);
                IContainer con1 = en1 as IContainer;
                IContainer con2 = en2 as IContainer;

                if (packet.SlotObject1.SlotId == 254 || packet.SlotObject1.SlotId == 255 ||
                    packet.SlotObject2.SlotId == 254 || packet.SlotObject2.SlotId == 255)
                {
                    if (packet.SlotObject2.SlotId == 254)
                        if (client.Player.HealthPotions < 6)
                        {
                            client.Player.HealthPotions++;
                            con1.Inventory[packet.SlotObject1.SlotId] = null;
                        }
                    if (packet.SlotObject2.SlotId == 255)
                        if (client.Player.MagicPotions < 6)
                        {
                            client.Player.MagicPotions++;
                            con1.Inventory[packet.SlotObject1.SlotId] = null;
                        }
                    if (packet.SlotObject1.SlotId == 254)
                        if (client.Player.HealthPotions > 0)
                        {
                            client.Player.HealthPotions--;
                            con2.Inventory[packet.SlotObject2.SlotId] = null;
                        }
                    if (packet.SlotObject1.SlotId == 255)
                        if (client.Player.MagicPotions > 0)
                        {
                            client.Player.MagicPotions--;
                            con2.Inventory[packet.SlotObject1.SlotId] = null;
                        }
                    if (en1 is Player)
                        (en1 as Player).Client.SendMessage(new INVRESULT { Result = 0 });
                    else if (en2 is Player)
                        (en2 as Player).Client.SendMessage(new INVRESULT { Result = 0 });
                    return;
                }
                //TODO: locker
                Item item1 = con1.Inventory[packet.SlotObject1.SlotId];
                Item item2 = con2.Inventory[packet.SlotObject2.SlotId];
                List<ushort> publicbags = new List<ushort>
                {
                    0x0500,
                    0x0506,
                    0x0501
                };
                if (en1.Dist(en2) > 1)
                {
                    if (en1 is Player)
                        (en1 as Player).Client.SendMessage(new INVRESULT
                        {
                            Result = -1
                        });
                    else if (en2 is Player)
                        (en2 as Player).Client.SendMessage(new INVRESULT
                        {
                            Result = -1
                        });
                    en1.UpdateCount++;
                    en2.UpdateCount++;
                    return;
                }
                //todo
                //if (!IsValid(item1, item2, con1, con2, packet, client))
                //{
                //    client.Disconnect();
                //    return;
                //}

                if (con2 is OneWayContainer)
                {
                    con1.Inventory[packet.SlotObject1.SlotId] = null;
                    con2.Inventory[packet.SlotObject2.SlotId] = null;
                    client.Player.DropBag(item1);
                    en1.UpdateCount++;
                    en2.UpdateCount++;
                    return;
                }
                if (con1 is OneWayContainer)
                {
                    if (con2.Inventory[packet.SlotObject2.SlotId] != null)
                    {
                        (en2 as Player)?.Client.SendMessage(new INVRESULT { Result = -1 });
                        (con1 as OneWayContainer).UpdateCount++;
                        en2.UpdateCount++;
                        return;
                    }

                    List<int> giftsList = client.Account.Gifts.ToList();
                    giftsList.Remove(con1.Inventory[packet.SlotObject1.SlotId].ObjectType);
                    int[] result = giftsList.ToArray();
                    client.Account.Gifts = result;
                    client.Account.Flush();

                    con1.Inventory[packet.SlotObject1.SlotId] = null;
                    con2.Inventory[packet.SlotObject2.SlotId] = item1;
                    (en2 as Player).CalcBoost();
                    client.Player.SaveToCharacter();
                    en1.UpdateCount++;
                    en2.UpdateCount++;
                    (en2 as Player).Client.SendMessage(new INVRESULT { Result = 0 });
                    return;
                }

                if (en1 is Player && en2 is Player & en1.Id != en2.Id)
                {
                    client.Manager.Chat.Announce($"{en1.Name} just tried to steal items from {en2.Name}'s inventory, GTFO YOU GOD DAMN FEGIT!!!!11111oneoneoneeleven");
                    return;
                };
                con1.Inventory[packet.SlotObject1.SlotId] = item2;
                con2.Inventory[packet.SlotObject2.SlotId] = item1;

                if (item2 != null)
                {
                    if (publicbags.Contains(en1.ObjectType) && item2.Soulbound)
                    {
                        client.Player.DropBag(item2);
                        con1.Inventory[packet.SlotObject1.SlotId] = null;
                    }
                }
                if (item1 != null)
                {
                    if (publicbags.Contains(en2.ObjectType) && item1.Soulbound)
                    {
                        client.Player.DropBag(item1);
                        con2.Inventory[packet.SlotObject2.SlotId] = null;
                    }
                }
                en1.UpdateCount++;
                en2.UpdateCount++;

                if (en1 is Player)
                {
                    if (en1.Owner.Name == "Vault")
                        (en1 as Player).Client.Player.SaveToCharacter();
                    (en1 as Player).CalcBoost();
                    (en1 as Player).Client.SendMessage(new INVRESULT { Result = 0 });
                }
                if (en2 is Player)
                {
                    if (en2.Owner.Name == "Vault")
                        (en2 as Player).Client.Player.SaveToCharacter();
                    (en2 as Player).CalcBoost();
                    (en2 as Player).Client.SendMessage(new INVRESULT { Result = 0 });
                }

                if (client.Player.Owner is Vault)
                    if ((client.Player.Owner as Vault).PlayerOwnerName == client.Account.Name)
                        return;
                client.Player.SaveToCharacter();
            }, PendingPriority.Networking);
        }

        private bool IsValid(Item item1, Item item2, IContainer con1, IContainer con2, INVSWAP packet, Client client)
        {
            if (con2 is Container || con2 is OneWayContainer)
                return true;

            bool ret = false;

            if (con1 is OneWayContainer || con1 is Container)
            {
                ret = con2.AuditItem(item1, packet.SlotObject2.SlotId);

                if (!ret)
                {
                    log.FatalFormat("Cheat engine detected for player {0},\nInvalid InvSwap. {1} instead of {2}",
                            client.Player.Name, client.Manager.GameData.Items[packet.SlotObject1.ObjectType].ObjectId, item1.ObjectId);
                    foreach (Player player in client.Player.Owner.Players.Values)
                        if (player.Client.Account.Rank >= 2)
                            player.SendInfo(string.Format("Cheat engine detected for player {0},\nInvalid InvSwap. {1} instead of {2}",
                                client.Player.Name, client.Manager.GameData.Items[packet.SlotObject1.ObjectType].ObjectId, item1.ObjectId));
                }
            }
            if (con1 is Player && con2 is Player)
            {
                ret = con1.AuditItem(item1, packet.SlotObject2.SlotId) && con2.AuditItem(item2, packet.SlotObject1.SlotId);

                if (!ret)
                {
                    log.FatalFormat("Cheat engine detected for player {0},\nInvalid InvSwap. {1} instead of {2}",
                            client.Player.Name, item1.ObjectId, client.Manager.GameData.Items[packet.SlotObject2.ObjectType].ObjectId);
                    foreach (Player player in client.Player.Owner.Players.Values)
                        if (player.Client.Account.Rank >= 2)
                            player.SendInfo(string.Format("Cheat engine detected for player {0},\nInvalid InvSwap. {1} instead of {2}",
                                client.Player.Name, item1.ObjectId, client.Manager.GameData.Items[packet.SlotObject2.ObjectType].ObjectId));
                }
            }

            return ret;
        }
    }
}