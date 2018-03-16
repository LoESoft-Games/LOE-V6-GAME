#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class PONG : IncomingMessage
    {
        public int Serial { get; set; }
        public int Time { get; set; }

        public override MessageID ID => MessageID.PONG;

        public override Message CreateInstance() => new PONG();

        protected override void Read(NReader rdr)
        {
            Serial = rdr.ReadInt32();
            Time = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(Serial);
            wtr.Write(Time);
        }
    }
}