#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class AOEACK : IncomingMessage
    {
        public int Time { get; set; }
        public Position Position { get; set; }

        public override MessageID ID => MessageID.AOEACK;

        public override Message CreateInstance() => new AOEACK();

        protected override void Read(NReader rdr)
        {
            Time = rdr.ReadInt32();
            Position = Position.Read(rdr);
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(Time);
            Position.Write(wtr);
        }
    }
}