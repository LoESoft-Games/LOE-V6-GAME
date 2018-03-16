#region

using System;
using gameserver.networking.incoming;
using gameserver.networking.outgoing;
using gameserver.realm;
using gameserver.realm.entity;
using gameserver.realm.entity.player;
using gameserver.realm.world;

#endregion

namespace gameserver.networking.handlers
{
    internal class InvDropHandler : MessageHandlers<INVDROP>
    {
        private readonly Random invRand = new Random();

        public override MessageID ID => MessageID.INVDROP;

        protected override void HandlePacket(Client client, INVDROP packet)
        {
            if (client.Player.Owner == null) return;
            if (packet.SlotObject.ObjectId != client.Player.Id) return;

            client.Manager.Logic.AddPendingAction(t =>
            {
                //TODO: locker again
                const ushort NORM_BAG = 0x0500;
                const ushort SOUL_BAG = 0x0507;

                Entity entity = client.Player.Owner.GetEntity(packet.SlotObject.ObjectId);
                IContainer con = entity as IContainer;
                Item item = null;
                if (packet.SlotObject.SlotId == 254)
                {
                    client.Player.HealthPotions--;
                    item = client.Player.Manager.GameData.Items[0xa22];
                }
                else if (packet.SlotObject.SlotId == 255)
                {
                    client.Player.MagicPotions--;
                    item = client.Player.Manager.GameData.Items[0xa23];
                }
                else
                {
                    if (con.Inventory[packet.SlotObject.SlotId] == null) return;

                    item = con.Inventory[packet.SlotObject.SlotId];
                    con.Inventory[packet.SlotObject.SlotId] = null;
                }
                entity.UpdateCount++;

                if (item != null)
                {
                    Container container;
                    if (item.Soulbound)
                    {
                        container = new Container(client.Player.Manager, SOUL_BAG, 1000 * 30, true)
                        {
                            BagOwners = new string[1] { client.Player.AccountId }
                        };
                    }
                    else
                    {
                        container = new Container(client.Player.Manager, NORM_BAG, 1000 * 30, true);
                    }
                    float bagx = entity.X + (float)((invRand.NextDouble() * 2 - 1) * 0.5);
                    float bagy = entity.Y + (float)((invRand.NextDouble() * 2 - 1) * 0.5);
                    try
                    {
                        container.Inventory[0] = item;
                        container.Move(bagx, bagy);
                        container.Size = 75;
                        client.Player.Owner.EnterWorld(container);

                        if (entity is Player)
                        {
                            (entity as Player).CalcBoost();
                            (entity as Player).Client.SendMessage(new INVRESULT
                            {
                                Result = 0
                            });
                            (entity as Player).Client.Player.SaveToCharacter();
                        }
                        if (client.Player.Owner is Vault)
                            if ((client.Player.Owner as Vault).PlayerOwnerName == client.Account.Name)
                                return;
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                        log.InfoFormat(client.Player.Name + " just attempted to dupe.");
                    }
                }
            }, PendingPriority.Networking);
        }
    }
}