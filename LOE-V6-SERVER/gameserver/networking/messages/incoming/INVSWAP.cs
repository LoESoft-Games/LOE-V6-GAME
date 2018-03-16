#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class INVSWAP : IncomingMessage
    {
        public int Time { get; set; }
        public Position Position { get; set; }
        public ObjectSlot SlotObject1 { get; set; }
        public ObjectSlot SlotObject2 { get; set; }

        public override MessageID ID => MessageID.INVSWAP;

        public override Message CreateInstance() => new INVSWAP();

        protected override void Read(NReader rdr)
        {
            Time = rdr.ReadInt32();
            Position = Position.Read(rdr);
            SlotObject1 = ObjectSlot.Read(rdr);
            SlotObject2 = ObjectSlot.Read(rdr);
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(Time);
            Position.Write(wtr);
            SlotObject1.Write(wtr);
            SlotObject2.Write(wtr);
        }
    }
}