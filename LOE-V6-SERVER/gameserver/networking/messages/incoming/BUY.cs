#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class BUY : IncomingMessage
    {
        public int ObjectId { get; set; }
        public int Quantity { get; set; }

        public override MessageID ID => MessageID.BUY;

        public override Message CreateInstance() => new BUY();

        protected override void Read(NReader rdr)
        {
            ObjectId = rdr.ReadInt32();
            Quantity = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(ObjectId);
            wtr.Write(Quantity);
        }
    }
}