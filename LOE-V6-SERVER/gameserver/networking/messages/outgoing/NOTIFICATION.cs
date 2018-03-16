#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class NOTIFICATION : OutgoingMessage
    {
        public int ObjectId { get; set; }
        public string Text { get; set; }
        public ARGB Color { get; set; }

        public override MessageID ID => MessageID.NOTIFICATION;

        public override Message CreateInstance() => new NOTIFICATION();

        protected override void Read(NReader rdr)
        {
            ObjectId = rdr.ReadInt32();
            Text = rdr.ReadUTF();
            Color = ARGB.Read(rdr);
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(ObjectId);
            wtr.WriteUTF(Text);
            Color.Write(wtr);
        }
    }
}