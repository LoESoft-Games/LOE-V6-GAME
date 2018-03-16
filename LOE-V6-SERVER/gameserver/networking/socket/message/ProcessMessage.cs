using gameserver.networking.incoming;
using gameserver.realm.entity.player;
using log4net.Core;
using System;
using System.Collections.Generic;

namespace gameserver.networking
{
    public partial class Client
    {
        public ProtocolState State { get; internal set; }
        public Player Player { get; internal set; }

        internal void ProcessMessage(Message msg)
        {
            try
            {
                //Log.Write("Message", $"From {(Player == null ? "null" : Player.Name)} (ID: {(msg.ID)})");
                Program.Logger.Logger.Log(typeof(Client), Level.Verbose, $"Handling packet '{msg}'...", null);
                if (msg.ID == (MessageID)255) return;
                IMessage handler;
                if (!MessageHandler.Handlers.TryGetValue(msg.ID, out handler))
                    Program.Logger.Warn($"Unhandled packet '{msg.ID}'.");
                else
                    handler.Handle(this, (IncomingMessage)msg);
            }
            catch (Exception e)
            {
                Program.Logger.Error($"Error when handling packet '{msg}'...", e);
                Disconnect(DisconnectReason.ERROR_WHEN_HANDLING_PACKET);
            }
        }

        public bool IsReady()
        {
            if (State == ProtocolState.Disconnected)
                return false;
            return State != ProtocolState.Ready || (Player != null && (Player == null || Player.Owner != null));
        }

        public void SendMessage(Message msg)
        {
            handler?.IncomingMessage(msg);
        }

        public void SendMessage(IEnumerable<Message> msgs)
        {
            handler?.IncomingMessage(msgs);
        }
    }
}