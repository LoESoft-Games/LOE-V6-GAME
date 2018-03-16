#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class BUYRESULT : OutgoingMessage
    {
        public int Result { get; set; }
        public string Message { get; set; }

        public override MessageID ID => MessageID.BUYRESULT;

        public override Message CreateInstance() => new BUYRESULT();

        protected override void Read(NReader rdr)
        {
            Result = rdr.ReadInt32();
            Message = rdr.ReadUTF();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(Result);
            wtr.WriteUTF(Message);
        }
    }
}