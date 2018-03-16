#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class INVRESULT : OutgoingMessage
    {
        public int Result { get; set; }

        public override MessageID ID => MessageID.INVRESULT;

        public override Message CreateInstance() => new INVRESULT();

        protected override void Read(NReader rdr)
        {
            Result = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(Result);
        }
    }
}