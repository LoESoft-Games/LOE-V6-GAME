#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class PETYARDUPDATE : OutgoingMessage
    {
        public int Type { get; set; }

        public override MessageID ID => MessageID.PETYARDUPDATE;

        public override Message CreateInstance() => new PETYARDUPDATE();

        protected override void Read(NReader rdr)
        {
            Type = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(Type);
        }
    }
}
