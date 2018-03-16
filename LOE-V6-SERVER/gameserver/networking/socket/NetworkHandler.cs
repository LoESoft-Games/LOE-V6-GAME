using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using log4net;
using core.config;
using static gameserver.networking.Client;

namespace gameserver.networking
{
    internal partial class NetworkHandler : IDisposable
    {
        public int BUFFER_SIZE = int.MaxValue / Settings.NETWORKING.CPU_HANDLER;

        private static readonly ILog log = LogManager.GetLogger(typeof(NetworkHandler));

        private readonly Client parent;
        private readonly ConcurrentQueue<Message> pendingPackets = new ConcurrentQueue<Message>();
        private readonly object sendLock = new object();
        private readonly Socket skt;

        private SocketAsyncEventArgs _incoming;
        private byte[] _incomingBuff;
        private IncomingStage _incomingState = IncomingStage.Awaiting;

        private SocketAsyncEventArgs _outgoing;
        private byte[] _outgoingBuff;
        private OutgoingState _outgoingState = OutgoingState.Awaiting;

        public NetworkHandler(Client parent, Socket skt)
        {
            this.parent = parent;
            this.skt = skt;
        }

        public void BeginHandling()
        {
            skt.NoDelay = Settings.NETWORKING.DISABLE_NAGLES_ALGORITHM;
            skt.UseOnlyOverlappedIO = true;

            _incoming = new SocketAsyncEventArgs();
            _incoming.Completed += IncomingCompleted;
            _incoming.UserToken = new OutgoingToken(); // target token
            _incoming.SetBuffer(_incomingBuff = new byte[BUFFER_SIZE], 0, BUFFER_SIZE);

            _outgoing = new SocketAsyncEventArgs();
            _outgoing.Completed += OutgoingCompleted;
            _outgoing.UserToken = new IncomingToken(); // target token
            _outgoing.SetBuffer(_outgoingBuff = new byte[BUFFER_SIZE], 0, BUFFER_SIZE);

            _outgoingState = OutgoingState.ReceivingHdr;

            _outgoing.SetBuffer(0, 5);

            if (!skt.ReceiveAsync(_outgoing))
                OutgoingCompleted(this, _outgoing);
        }

        private void OnError(Exception ex)
        {
            log.Error("Socket error detected: ", ex);
            parent.Disconnect(DisconnectReason.SOCKET_ERROR_DETECTED);
        }

        public void Dispose()
        {
            _incoming.Completed -= IncomingCompleted;
            _incoming.Dispose();
            _incomingBuff = null;
            _outgoing.Completed -= OutgoingCompleted;
            _outgoing.Dispose();
            _outgoingBuff = null;
        }
    }
}