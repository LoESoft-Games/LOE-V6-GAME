#region

using System.Linq;
using core;
using gameserver.networking.incoming;
using gameserver.networking.outgoing;
using gameserver.realm.entity.player;
using gameserver.realm;
using FAILURE = gameserver.networking.outgoing.FAILURE;
using static gameserver.networking.Client;

#endregion

namespace gameserver.networking.handlers
{
    internal class CreateHandler : MessageHandlers<CREATE>
    {
        public override MessageID ID => MessageID.CREATE;

        protected override void HandlePacket(Client client, CREATE packet)
        {
            int skin = client.Account.OwnedSkins.Contains(packet.SkinType) ? packet.SkinType : 0;
            DbChar character;
            CreateStatus status = client.Manager.Database.CreateCharacter(client.Manager.GameData, client.Account, (ushort)packet.ClassType, skin, out character);
            if (status == CreateStatus.ReachCharLimit)
            {
                client.SendMessage(new FAILURE
                {
                    ErrorDescription = "Failed to Load character."
                });
                client.Disconnect(DisconnectReason.FAILED_TO_LOAD_CHARACTER);
                return;
            }
            client.Character = character;
            World target = client.Manager.Worlds[client.TargetWorld];
            target.Timers.Add(new WorldTimer(5000, (w, t) =>
            {
                if (status == CreateStatus.OK)
                {
                    client.SendMessage(new CREATE_SUCCESS
                    {
                        CharacterID = client.Character.CharId,
                        ObjectID =
                            client.Manager.Worlds[client.TargetWorld].EnterWorld(
                                client.Player = new Player(client.Manager, client))
                    });
                    client.State = ProtocolState.Ready;
                }
                else
                {
                    client.SendMessage(new FAILURE
                    {
                        ErrorDescription = "Failed to Load character."
                    });
                }
            }));
        }
    }
}