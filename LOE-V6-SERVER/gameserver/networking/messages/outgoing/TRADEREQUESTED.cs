#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class TRADEREQUESTED : OutgoingMessage
    {
        public string Name { get; set; }

        public override MessageID ID => MessageID.TRADEREQUESTED;

        public override Message CreateInstance() => new TRADEREQUESTED();

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