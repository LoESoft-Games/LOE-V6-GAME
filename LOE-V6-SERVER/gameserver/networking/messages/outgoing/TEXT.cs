#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class TEXT : OutgoingMessage
    {
        public string Name { get; set; }
        public int ObjectId { get; set; }
        public int Stars { get; set; }
        public int Admin { get; set; }
        public byte BubbleTime { get; set; }
        public string Recipient { get; set; }
        public string Text { get; set; }
        public string CleanText { get; set; }
        public int NameColor { get; set; }
        public int TextColor { get; set; }

        public override MessageID ID => MessageID.TEXT;

        public override Message CreateInstance() => new TEXT();

        protected override void Read(NReader rdr)
        {
            Name = rdr.ReadUTF();
            ObjectId = rdr.ReadInt32();
            Stars = rdr.ReadInt32();
            Admin = rdr.ReadInt32();
            BubbleTime = rdr.ReadByte();
            Recipient = rdr.ReadUTF();
            Text = rdr.ReadUTF();
            CleanText = rdr.ReadUTF();
            NameColor = rdr.ReadInt32();
            TextColor = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.WriteUTF(Name);
            wtr.Write(ObjectId);
            wtr.Write(Stars);
            wtr.Write(Admin);
            wtr.Write(BubbleTime);
            wtr.WriteUTF(Recipient);
            wtr.WriteUTF(Text);
            wtr.WriteUTF(CleanText);
            wtr.Write(NameColor);
            wtr.Write(TextColor);
        }
    }
}