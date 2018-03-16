#region

using core.config;
using System;
using System.Globalization;
using gameserver.networking.incoming;
using gameserver.networking.outgoing;
using gameserver.realm;
using gameserver.realm.entity;
using gameserver.realm.world;
using FAILURE = gameserver.networking.outgoing.FAILURE;
using gameserver.realm.entity.player;

#endregion

namespace gameserver.networking.handlers
{
    internal class UsePortalHandler : MessageHandlers<USEPORTAL>
    {
        public override MessageID ID => MessageID.USEPORTAL;

        protected override void HandlePacket(Client client, USEPORTAL packet)
        {
            client.Manager.Logic.AddPendingAction(t => Handle(client, client.Player, packet), PendingPriority.Networking);
        }

        void Handle(Client client, Player player, USEPORTAL packet)
        {
            if (player?.Owner == null)
                return;

            Portal portal = player.Owner.GetEntity(packet.ObjectId) as Portal;

            if (portal == null)
                return;

            if (!portal.Usable)
            {
                player.SendError("{\"key\":\"server.realm_full\"}");
                return;
            }

            World world = portal.WorldInstance;

            if (world == null)
            {
                bool setWorldInstance = true;

                PortalDesc desc = portal.ObjectDesc;

                if (desc == null)
                {
                    client.SendMessage(new FAILURE
                    {
                        ErrorId = 0,
                        ErrorDescription = "Portal not found!"
                    });
                }
                else
                {
                    switch (portal.ObjectType)
                    {
                        case 0x0720:
                            world = player.Manager.PlayerVault(client);
                            setWorldInstance = false;
                            break;
                        case 0x0704:
                        case 0x0703: //portal of cowardice
                        case 0x0d40:
                        case 0x070d:
                        case 0x070e:
                            {
                                if (player.Manager.LastWorld.ContainsKey(player.AccountId))
                                {
                                    World w = player.Manager.LastWorld[player.AccountId];

                                    if (w != null && player.Manager.Worlds.ContainsKey(w.Id))
                                        world = w;
                                    else
                                        world = player.Manager.GetWorld(World.NEXUS_ID);
                                }
                                else
                                    world = player.Manager.GetWorld(World.NEXUS_ID);
                                setWorldInstance = false;
                            }
                            break;
                        case 0x0750:
                            world = player.Manager.GetWorld(World.MARKET);
                            break;
                        case 0x071d:
                            world = player.Manager.GetWorld(World.NEXUS_ID);
                            break;
                        case 0x0712:
                            world = player.Manager.GetWorld(World.NEXUS_ID);
                            break;
                        case 0x1756:
                            world = player.Manager.GetWorld(World.DAILY_QUEST_ID);
                            break;
                        case 0x072f:
                            if (player.Guild != null)
                            {
                                //client.Player.SendInfo(
                                //    "Sorry, you are unable to enter the GuildHall because of a possible memory leak, check back later");
                                player.SendInfo("Thanks.");
                            }
                            break;
                        default:
                            {
                                Type worldType =
                                    Type.GetType("gameserver.realm.world." + desc.DungeonName.Replace(" ", string.Empty).Replace("'", string.Empty));
                                if (worldType != null)
                                {
                                    try
                                    {
                                        world = client.Manager.AddWorld((World)Activator.CreateInstance(worldType,
                                        System.Reflection.BindingFlags.CreateInstance, null, null,
                                        CultureInfo.InvariantCulture, null));
                                    }
                                    catch
                                    {
                                        player.SendError($"Dungeon instance \"{desc.DungeonName}\" isn't declared yet and under maintenance until further notice.");
                                    }
                                }
                                else
                                    player.SendHelp($"Dungeon instance \"{desc.DungeonName}\" isn't declared yet and under maintenance until further notice.");
                            }
                            break;
                    }
                }
                if (setWorldInstance)
                    portal.WorldInstance = world;
            }

            if (world != null)
            {
                if (world.IsFull)
                {
                    player.SendError("{\"key\":\"server.dungeon_full\"}");
                    return;
                }

                if (player.Manager.LastWorld.ContainsKey(player.AccountId))
                {
                    World dummy;
                    player.Manager.LastWorld.TryRemove(player.AccountId, out dummy);
                }
                if (player.Owner is Nexus || player.Owner is GameWorld)
                    player.Manager.LastWorld.TryAdd(player.AccountId, player.Owner);

                client?.Reconnect(new RECONNECT
                {
                    Host = "",
                    Port = Settings.GAMESERVER.PORT,
                    GameId = world.Id,
                    Name = world.Name,
                    Key = world.PortalKey,
                });
            }
        }
    }
}