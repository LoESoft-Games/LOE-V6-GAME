#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class CLIENTSTAT : OutgoingMessage
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public override MessageID ID => MessageID.CLIENTSTAT;

        public override Message CreateInstance() => new CLIENTSTAT();

        protected override void Read(NReader rdr)
        {
            Name = rdr.ReadUTF();
            Value = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.WriteUTF(Name);
            wtr.Write(Value);
        }
    }
}