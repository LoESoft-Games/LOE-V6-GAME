#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class ARENA_DEATH : OutgoingMessage
    {
        public int RestartPrice { get; set; }

        public override MessageID ID => MessageID.ARENA_DEATH;

        public override Message CreateInstance() => new ARENA_DEATH();

        protected override void Read(NReader rdr)
        {
            RestartPrice = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(RestartPrice);
        }
    }
}