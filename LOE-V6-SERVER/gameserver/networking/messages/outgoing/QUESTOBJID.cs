#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class QUESTOBJID : OutgoingMessage
    {
        public int ObjectId { get; set; }

        public override MessageID ID => MessageID.QUESTOBJID;

        public override Message CreateInstance() => new QUESTOBJID();

        protected override void Read(NReader rdr)
        {
            ObjectId = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(ObjectId);
        }
    }
}