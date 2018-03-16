#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class FILE : OutgoingMessage
    {
        public string Name { get; set; }
        public byte[] Bytes { get; set; }

        public override MessageID ID => MessageID.FILE;

        public override Message CreateInstance() => new FILE();

        protected override void Read(NReader rdr)
        {
            Name = rdr.ReadUTF();
            Bytes = new byte[rdr.ReadInt32()];
            Bytes = rdr.ReadBytes(Bytes.Length);
        }

        protected override void Write(NWriter wtr)
        {
            wtr.WriteUTF(Name);
            wtr.Write(Bytes.Length);
            wtr.Write(Bytes);
        }
    }
}