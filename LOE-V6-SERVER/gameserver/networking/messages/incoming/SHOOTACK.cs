#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class SHOOTACK : IncomingMessage
    {
        public int Time { get; set; }

        public override MessageID ID => MessageID.SHOOTACK;

        public override Message CreateInstance() => new SHOOTACK();

        protected override void Read(NReader rdr)
        {
            Time = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(Time);
        }
    }
}