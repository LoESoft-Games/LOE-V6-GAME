using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace gameserver.networking
{
    internal partial class NetworkHandler
    {
        private bool IncomingMessageReceived(Message pkt)
        {
            if (parent.IsReady())
            {
                parent.Manager.Network.AddPendingPacket(parent, pkt);
                return true;
            }
            return false;
        }

        private void IncomingCompleted(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                if (!skt.Connected) return;

                int len;
                switch (_incomingState)
                {
                    case IncomingStage.Ready:
                        len = (e.UserToken as OutgoingToken).Packet.Write(parent, _incomingBuff, 0);

                        _incomingState = IncomingStage.Sending;
                        e.SetBuffer(0, len);

                        if (!skt.Connected) return;
                        skt.SendAsync(e);
                        break;
                    case IncomingStage.Sending:
                        (e.UserToken as OutgoingToken).Packet = null;

                        if (IncomingMessage(e, true))
                        {
                            len = (e.UserToken as OutgoingToken).Packet.Write(parent, _incomingBuff, 0);

                            _incomingState = IncomingStage.Sending;
                            e.SetBuffer(0, len);

                            if (!skt.Connected) return;
                            skt.SendAsync(e);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }

        private bool IncomingMessage(SocketAsyncEventArgs e, bool ignoreSending)
        {
            lock (sendLock)
            {
                if (_incomingState == IncomingStage.Ready ||
                    (!ignoreSending && _incomingState == IncomingStage.Sending))
                    return false;
                Message packet;
                if (pendingPackets.TryDequeue(out packet))
                {
                    (e.UserToken as OutgoingToken).Packet = packet;
                    _incomingState = IncomingStage.Ready;
                    return true;
                }
                _incomingState = IncomingStage.Awaiting;
                return false;
            }
        }

        public void IncomingMessage(Message msg)
        {
            if (!skt.Connected) return;
            pendingPackets.Enqueue(msg);
            if (IncomingMessage(_incoming, false))
            {
                int len = (_incoming.UserToken as OutgoingToken).Packet.Write(parent, _incomingBuff, 0);

                _incomingState = IncomingStage.Sending;
                _incoming.SetBuffer(_incomingBuff, 0, len);
                if (!skt.SendAsync(_incoming))
                    IncomingCompleted(this, _incoming);
            }
        }

        public void IncomingMessage(IEnumerable<Message> msgs)
        {
            if (!skt.Connected) return;
            foreach (Message i in msgs)
                pendingPackets.Enqueue(i);
            if (IncomingMessage(_incoming, false))
            {
                int len = (_incoming.UserToken as OutgoingToken).Packet.Write(parent, _incomingBuff, 0);

                _incomingState = IncomingStage.Sending;
                _incoming.SetBuffer(_incomingBuff, 0, len);
                if (!skt.SendAsync(_incoming))
                    IncomingCompleted(this, _incoming);
            }
        }
    }
}
