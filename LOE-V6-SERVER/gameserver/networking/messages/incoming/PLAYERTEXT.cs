#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class PLAYERTEXT : IncomingMessage
    {
        public string Text { get; set; }

        public override MessageID ID => MessageID.PLAYERTEXT;

        public override Message CreateInstance() => new PLAYERTEXT();

        protected override void Read(NReader rdr)
        {
            Text = rdr.ReadUTF();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.WriteUTF(Text);
        }
    }
}