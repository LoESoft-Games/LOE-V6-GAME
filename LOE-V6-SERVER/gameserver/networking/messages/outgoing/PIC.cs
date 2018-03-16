#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class PIC : OutgoingMessage
    {
        public BitmapData BitmapData { get; set; }

        public override MessageID ID => MessageID.PIC;

        public override Message CreateInstance() => new PIC();

        protected override void Read(NReader rdr)
        {
            BitmapData = BitmapData.Read(rdr);
        }

        protected override void Write(NWriter wtr)
        {
            BitmapData.Write(wtr);
        }
    }
}