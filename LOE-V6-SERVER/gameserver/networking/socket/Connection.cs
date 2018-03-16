using gameserver.networking.error;
using gameserver.networking.outgoing;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using FAILURE = gameserver.networking.outgoing.FAILURE;

namespace gameserver.networking
{
    public partial class Client
    {
        public Task task = Task.Delay(250);

        public string[] time => DateTime.Now.ToString().Split(' ');

        public void _(string accId, RECONNECT msg)
        {
            string response = $"[{time[1]}] [{nameof(Client)}] Reconnect\t->\tplayer id {accId} to {msg.Name}";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(response);
            Console.ResetColor();
        }

        public enum DisconnectReason : byte
        {
            FAILED_TO_LOAD_CHARACTER = 1,
            OUTDATED_CLIENT = 2,
            OUTDATED_INTERNAL_CLIENT = 3,
            DISABLE_GUEST_ACCOUNT = 4,
            BAD_LOGIN = 5,
            SERVER_FULL = 6,
            ACCOUNT_BANNED = 7,
            INVALID_DISCONNECT_KEY = 8,
            LOST_CONNECTION = 9,
            ACCOUNT_IN_USE = 10,
            INVALID_WORLD = 11,
            INVALID_PORTAL_KEY = 12,
            PORTAL_KEY_EXPIRED = 13,
            CHARACTER_IS_DEAD = 14,
            HP_POTION_CHEAT_ENGINE = 15,
            MP_POTION_CHEAT_ENGINE = 16,
            STOPING_SERVER = 17,
            SOCKET_IS_NOT_CONNECTED = 18,
            RECEIVING_HDR = 19,
            RECEIVING_BODY = 20,
            ERROR_WHEN_HANDLING_PACKET = 21,
            SOCKET_ERROR_DETECTED = 22,
            PROCESS_POLICY_FILE = 23,
            RESTART = 24,
            PLAYER_KICK = 25,
            PLAYER_BANNED = 26,
            CHARACTER_IS_DEAD_ERROR = 27,
            CHEAT_ENGINE_DETECTED = 28,
            RECONNECT_TO_CASTLE = 29,
            REALM_MANAGER_DISCONNECT = 30,
            STOPPING_REALM_MANAGER = 31,
            DUPER_DISCONNECT = 32,
            ACCESS_DENIED = 33,
            UNKNOW_ERROR_INSTANCE = 255
        }

        public void _(string accId, Socket skt, DisconnectReason type)
        {
            string response = $"[{time[1]}] [{nameof(Client)}] [({(int)type}) {type.ToString()}] Disconnect\t->\tplayer id {accId} to {skt.RemoteEndPoint.ToString().Split(':')[0]}";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(response);
            Console.ResetColor();
        }

        public async void Reconnect(RECONNECT msg)
        {
            if (this == null)
                return;

            if (Account == null)
            {
                string[] labels = new string[] { "{CLIENT_NAME}" };
                string[] arguments = new string[] { Account.Name };

                SendMessage(new FAILURE
                {
                    ErrorId = Type.JSON_DIALOG,
                    ErrorDescription =
                        JSONErrorIDHandler.
                            FormatedJSONError(
                                errorID: ErrorIDs.LOST_CONNECTION,
                                labels: labels,
                                arguments: arguments
                            )
                });

                await task;

                Disconnect(DisconnectReason.LOST_CONNECTION);
                return;
            }

            _(Account.AccountId, msg);

            Save();

            await task;

            task.Dispose();

            SendMessage(msg);
        }

        public void Save()
        {
            try
            {
                Player?.SaveToCharacter();

                if (Character != null)
                    Manager.Database.SaveCharacter(Account, Character, false);
                if (Account != null)
                    Manager.Database.ReleaseLock(Account);
            }
            catch (Exception ex)
            {
                Program.Logger.Error($"[{nameof(Client)}] Save exception:\n{ex}");
            }
        }

        private async void Disconnect(Client client)
        {
            if (client == null)
                return;

            Save();

            await task;

            task.Dispose();

            Manager.Disconnect(this);
        }

        public async void Disconnect(DisconnectReason type)
        {
            try
            {
                Save();

                await task;

                task.Dispose();

                if (State == ProtocolState.Disconnected)
                    return;

                if (Socket == null)
                    return;

                if (Account == null)
                    return;

                _(Account.AccountId, Socket, type);

                State = ProtocolState.Disconnected;

                if (Account != null)
                    Disconnect(this);

                Socket?.Close();
            }
            catch (Exception e)
            {
                Program.Logger.Error($"[{nameof(Client)}] Disconnect exception:\n{e}");
            }
        }
    }
}