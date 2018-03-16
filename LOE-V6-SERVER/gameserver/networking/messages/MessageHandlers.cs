using log4net;
using gameserver.networking.incoming;
using gameserver.realm;

namespace gameserver.networking
{
    internal abstract class MessageHandlers<T> : IMessage where T : IncomingMessage
    {
        protected ILog log;

        private Client client;

        public Client Client => client;
        public RealmManager Manager => client.Manager;

        public abstract MessageID ID { get; }

        public MessageHandlers()
        {
            log = LogManager.GetLogger(GetType());
        }

        protected abstract void HandlePacket(Client client, T packet);

        public void Handle(Client client, IncomingMessage packet)
        {
            this.client = client;
            HandlePacket(client, (T)packet);
        }

        protected void SendFailure(string text) => client.SendMessage(new FAILURE { ErrorId = 0, ErrorDescription = text });
    }
}