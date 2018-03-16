#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class RESKIN_UNLOCK : OutgoingMessage
    {
        public int SkinID { get; set; }

        public override MessageID ID => MessageID.RESKIN_UNLOCK;

        public override Message CreateInstance() => new RESKIN_UNLOCK();

        protected override void Read(NReader rdr)
        {
            SkinID = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(SkinID);
        }
    }
}
