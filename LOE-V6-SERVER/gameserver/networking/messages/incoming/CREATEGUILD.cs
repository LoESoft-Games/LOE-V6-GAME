#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class CREATEGUILD : IncomingMessage
    {
        public string Name { get; set; }

        public override MessageID ID => MessageID.CREATEGUILD;

        public override Message CreateInstance() => new CREATEGUILD();

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