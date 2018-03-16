#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class CHOOSENAME : IncomingMessage
    {
        public string Name { get; set; }

        public override MessageID ID => MessageID.CHOOSENAME;

        public override Message CreateInstance() => new CHOOSENAME();

        protected override void Read(NReader rdr)
        {
            Name = rdr.ReadUTF();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.WriteUTF(Name);
        }
    }
}