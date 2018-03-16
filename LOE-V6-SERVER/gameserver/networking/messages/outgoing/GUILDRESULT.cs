#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class GUILDRESULT : OutgoingMessage
    {
        public bool Success { get; set; }
        public string ErrorText { get; set; }

        public override MessageID ID => MessageID.GUILDRESULT;

        public override Message CreateInstance() => new GUILDRESULT();

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