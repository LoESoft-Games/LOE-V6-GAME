using gameserver.networking.incoming;

namespace gameserver.networking
{
    internal interface IMessage
    {
        MessageID ID { get; }
        void Handle(Client client, IncomingMessage packet);
    }
}