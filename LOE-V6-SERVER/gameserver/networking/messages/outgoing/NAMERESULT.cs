#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class NAMERESULT : OutgoingMessage
    {
        public bool Success { get; set; }
        public string ErrorText { get; set; }

        public override MessageID ID => MessageID.NAMERESULT;

        public override Message CreateInstance() => new NAMERESULT();

        protected override void Read(NReader rdr)
        {
            Success = rdr.ReadBoolean();
            ErrorText = rdr.ReadUTF();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(Success);
            wtr.WriteUTF(ErrorText);
        }
    }
}