#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class MOVE : IncomingMessage
    {
        public int TickId { get; set; }
        public int Time { get; set; }
        public Position Position { get; set; }
        public TimedPosition[] Records { get; set; }

        public override MessageID ID => MessageID.MOVE;

        public override Message CreateInstance() => new MOVE();

        protected override void Read(NReader rdr)
        {
            TickId = rdr.ReadInt32();
            Time = rdr.ReadInt32();
            Position = Position.Read(rdr);
            Records = new TimedPosition[rdr.ReadInt16()];
            for (int i = 0; i < Records.Length; i++)
                Records[i] = TimedPosition.Read(rdr);
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(TickId);
            wtr.Write(Time);
            Position.Write(wtr);
            if (Records == null)
            {
                wtr.Write((ushort)0);
                return;
            }
            wtr.Write((ushort)Records.Length);
            foreach (TimedPosition i in Records)
                i.Write(wtr);
        }
    }
}