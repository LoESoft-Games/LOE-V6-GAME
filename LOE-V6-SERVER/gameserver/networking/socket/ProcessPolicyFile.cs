using core;
using core.config;
using System.Net.Sockets;
using static gameserver.networking.Client;

namespace gameserver.networking
{
    internal partial class NetworkHandler
    {
        private void ProcessPolicyFile()
        {
            NetworkStream s = new NetworkStream(skt);
            NWriter wtr = new NWriter(s);
            wtr.WriteNullTerminatedString(
                @"<cross-domain-policy>
                    <allow-access-from domain=""*"" to-ports=""*"" />
                </cross-domain-policy>"
            );
            wtr.Write((byte)'\r');
            wtr.Write((byte)'\n');
            parent.Disconnect(DisconnectReason.PROCESS_POLICY_FILE);
        }
    }
}