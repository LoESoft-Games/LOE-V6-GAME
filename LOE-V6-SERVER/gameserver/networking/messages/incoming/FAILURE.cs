#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class FAILURE : IncomingMessage
    {
        public int ErrorId { get; set; }
        public string ErrorDescription { get; set; }

        public override MessageID ID => MessageID.FAILURE;

        public override Message CreateInstance() => new FAILURE();

        protected override void Read(NReader rdr)
        {
            ErrorId = rdr.ReadInt32();
            ErrorDescription = rdr.ReadUTF();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(ErrorId);
            wtr.WriteUTF(ErrorDescription);
        }
    }
}