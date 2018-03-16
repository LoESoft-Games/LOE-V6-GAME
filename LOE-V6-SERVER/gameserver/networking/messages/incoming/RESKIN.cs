#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class RESKIN : IncomingMessage
    {
        public int SkinId { get; set; }

        public override MessageID ID => MessageID.RESKIN;

        public override Message CreateInstance() => new RESKIN();

        protected override void Read(NReader rdr)
        {
            SkinId = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(SkinId);
        }
    }
}