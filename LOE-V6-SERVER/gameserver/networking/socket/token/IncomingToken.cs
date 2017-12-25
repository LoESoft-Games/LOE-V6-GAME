namespace gameserver.networking
{
    internal partial class NetworkHandler
    {
        private class IncomingToken
        {
            public int Length { get; set; }
            public Message Packet { get; set; }
        }
    }
}