#region

using gameserver.networking.incoming;
using gameserver.networking.outgoing;
using gameserver.realm.entity.player;
using FAILURE = gameserver.networking.outgoing.FAILURE;
using gameserver.realm;
using static gameserver.networking.Client;

#endregion

namespace gameserver.networking.handlers
{
    internal class LoadHandler : MessageHandlers<LOAD>
    {
        public override MessageID ID => MessageID.LOAD;

        protected override void HandlePacket(Client client, LOAD packet)
        {
            client.Character = client.Manager.Database.LoadCharacter(client.Account, packet.CharacterId);
            if (client.Character != null)
            {
                if (client.Character.Dead)
                {
                    client.SendMessage(new FAILURE
                    {
                        ErrorId = 0,
                        ErrorDescription = "Character is dead."
                    });
                    client.Disconnect(DisconnectReason.CHARACTER_IS_DEAD);
                }
                else
                {
                    World target = client.Manager.Worlds[client.TargetWorld];
                    client.SendMessage(new CREATE_SUCCESS
                    {
                        CharacterID = client.Character.CharId,
                        ObjectID =
                                client.Manager.Worlds[client.TargetWorld].EnterWorld(
                                    client.Player = new Player(client.Manager, client))
                    });
                    client.State = ProtocolState.Ready;
                }
            }
            else
            {
                client.SendMessage(new FAILURE
                {
                    ErrorId = 0,
                    ErrorDescription = "Failed to Load character."
                });
                client.Disconnect(DisconnectReason.FAILED_TO_LOAD_CHARACTER);
            }
        }
    }
}