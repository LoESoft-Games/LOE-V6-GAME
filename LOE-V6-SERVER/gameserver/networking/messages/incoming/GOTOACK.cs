#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class GOTOACK : IncomingMessage
    {
        public int Time { get; set; }

        public override MessageID ID => MessageID.GOTOACK;

        public override Message CreateInstance() => new GOTOACK();

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