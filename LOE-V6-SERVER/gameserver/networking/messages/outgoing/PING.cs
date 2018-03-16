#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class PING : OutgoingMessage
    {
        public int Serial { get; set; }

        public override MessageID ID => MessageID.PING;

        public override Message CreateInstance() => new PING();

        protected override void Read(NReader rdr)
        {
            Serial = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(Serial);
        }
    }
}