#region

using System.Linq;
using core;
using System.Text;
using gameserver.networking.incoming;
using gameserver.networking.outgoing;
using gameserver.realm;
using gameserver.realm.world;
using FAILURE = gameserver.networking.outgoing.FAILURE;
using System;
using gameserver.networking.error;
using static gameserver.networking.Client;

#endregion

namespace gameserver.networking.handlers
{
    internal class HelloHandler : MessageHandlers<HELLO>
    {
        public override MessageID ID => MessageID.HELLO;

        protected override void HandlePacket(Client client, HELLO packet)
        {
            if (SERVER_VERSION != packet.BuildVersion)
            {
                client.SendMessage(new FAILURE
                {
                    ErrorId = 8,
                    ErrorDescription =
                        JSONErrorIDHandler.
                            FormatedJSONError(
                                errorID: ErrorIDs.OUTDATED_CLIENT,
                                labels: new[] { "{CLIENT_BUILD_VERSION}", "{SERVER_BUILD_VERSION}" },
                                arguments: new[] { packet.BuildVersion, SERVER_VERSION }
                            )
                });
                client.Disconnect(DisconnectReason.OUTDATED_CLIENT);
                return;
            }
            if (!INTERNAL_SERVER_BUILD.Contains(packet.InternalBuildVersion)) //support for multi internal server build versions
            {
                client.SendMessage(new FAILURE
                {
                    ErrorId = 8,
                    ErrorDescription =
                        JSONErrorIDHandler.
                            FormatedJSONError(
                                errorID: ErrorIDs.OUTDATED_INTERNAL_CLIENT,
                                labels: null,
                                arguments: null
                            )
                });
                client.Disconnect(DisconnectReason.OUTDATED_INTERNAL_CLIENT);
                return;
            }
            DbAccount acc;
            LoginStatus s1 = client.Manager.Database.Verify(packet.GUID, packet.Password, out acc);
            if (s1 == LoginStatus.AccountNotExists)
            {
                RegisterStatus s2 = client.Manager.Database.Register(packet.GUID, packet.Password, true, out acc); //Register guest but do not allow join game.
                client.SendMessage(new FAILURE()
                {
                    ErrorId = 8,
                    ErrorDescription =
                        JSONErrorIDHandler.
                            FormatedJSONError(
                                errorID: ErrorIDs.DISABLE_GUEST_ACCOUNT,
                                labels: null,
                                arguments: null
                            )
                });
                client.Disconnect(DisconnectReason.DISABLE_GUEST_ACCOUNT);
                return;
            }
            else if (s1 == LoginStatus.InvalidCredentials)
            {
                client.SendMessage(new FAILURE
                {
                    ErrorId = 0,
                    ErrorDescription = "Bad login."
                });
                client.Disconnect(DisconnectReason.BAD_LOGIN);
            }
            client.ConnectedBuild = packet.BuildVersion;
            Tuple<bool, ErrorIDs> TryConnect = client.Manager.TryConnect(client);
            if (!TryConnect.Item1)
            {
                client.Account = null;
                ErrorIDs errorID = TryConnect.Item2;
                string[] labels;
                string[] arguments;
                DisconnectReason type;
                switch (TryConnect.Item2)
                {
                    case ErrorIDs.SERVER_FULL:
                        {
                            labels = new[] { "{MAX_USAGE}" };
                            arguments = new[] { $"{client.Manager.MaxClients}" };
                            type = DisconnectReason.SERVER_FULL;
                        }
                        break;
                    case ErrorIDs.ACCOUNT_BANNED:
                        {
                            labels = new[] { "{CLIENT_NAME}" };
                            arguments = new[] { client.Account.Name };
                            type = DisconnectReason.ACCOUNT_BANNED;
                        }
                        break;
                    case ErrorIDs.INVALID_DISCONNECT_KEY:
                        {
                            labels = new[] { "{CLIENT_NAME}" };
                            arguments = new[] { client.Account.Name };
                            type = DisconnectReason.INVALID_DISCONNECT_KEY;
                        }
                        break;
                    case ErrorIDs.LOST_CONNECTION:
                        {
                            labels = new[] { "{CLIENT_NAME}" };
                            arguments = new[] { client.Account.Name };
                            type = DisconnectReason.LOST_CONNECTION;
                        }
                        break;
                    default:
                        {
                            labels = new[] { "{UNKNOW_ERROR_INSTANCE}" };
                            arguments = new[] { "connection aborted by unexpected protocol at line <b>340</b> or line <b>346</b> from 'TryConnect' function in RealmManager for security reasons" };
                            type = DisconnectReason.UNKNOW_ERROR_INSTANCE;
                        }
                        break;
                }
                client.SendMessage(new FAILURE
                {
                    ErrorId = 8,
                    ErrorDescription =
                        JSONErrorIDHandler.
                            FormatedJSONError(
                                errorID: errorID,
                                labels: labels,
                                arguments: arguments
                            )
                });
                client.Disconnect(type);
                return;
            }
            else
            {
                if (packet.GameId == World.NEXUS_LIMBO)
                    packet.GameId = World.NEXUS_ID;
                World world = client.Manager.GetWorld(packet.GameId);
                if (world == null && packet.GameId == World.TUT_ID)
                    world = client.Manager.AddWorld(new Tutorial(false));
                if (!client.Manager.Database.AcquireLock(acc))
                {
                    //SendFailure(client, "Account in Use (" +
                    //    client.Manager.Database.GetLockTime(acc) + " seconds until timeout)");
                    client.Disconnect(DisconnectReason.ACCOUNT_IN_USE);
                    return;
                }
                if (world == null)
                {
                    client.SendMessage(new FAILURE
                    {
                        ErrorId = 1,
                        ErrorDescription = "Invalid world."
                    });
                    client.Disconnect(DisconnectReason.INVALID_WORLD);
                    return;
                }
                if (world.NeedsPortalKey)
                {
                    if (!world.PortalKey.SequenceEqual(packet.Key))
                    {
                        client.SendMessage(new FAILURE
                        {
                            ErrorId = 1,
                            ErrorDescription = "Invalid Portal Key"
                        });
                        client.Disconnect(DisconnectReason.INVALID_PORTAL_KEY);
                        return;
                    }
                    if (world.PortalKeyExpired)
                    {
                        client.SendMessage(new FAILURE
                        {
                            ErrorId = 1,
                            ErrorDescription = "Portal key expired."
                        });
                        client.Disconnect(DisconnectReason.PORTAL_KEY_EXPIRED);
                        return;
                    }
                }
                if (packet.MapInfo.Length > 0 || world.Id == -6) //Test World
                    (world as Test).LoadJson(Encoding.Default.GetString(packet.MapInfo));
                if (world.IsLimbo)
                    world = world.GetInstance(client);
                client.Account = acc;
                client.Random = new wRandom(world.Seed);
                client.TargetWorld = world.Id;
                client.SendMessage(new MAPINFO
                {
                    Width = world.Map.Width,
                    Height = world.Map.Height,
                    Name = world.Name,
                    Seed = world.Seed,
                    ClientWorldName = world.ClientWorldName,
                    Difficulty = world.Difficulty,
                    Background = world.Background,
                    AllowTeleport = world.AllowTeleport,
                    ShowDisplays = world.ShowDisplays,
                    ClientXML = world.ClientXml,
                    ExtraXML = Manager.GameData.AdditionXml,
                    Music = world.Name
                });
                client.State = ProtocolState.Handshaked;
            }
        }
    }
}