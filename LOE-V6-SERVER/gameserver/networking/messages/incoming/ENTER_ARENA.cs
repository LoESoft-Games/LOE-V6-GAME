#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class ENTER_ARENA : IncomingMessage
    {
        public int Currency { get; set; }

        public override MessageID ID => MessageID.ENTER_ARENA;

        public override Message CreateInstance() => new ENTER_ARENA();

        protected override void Read(NReader rdr)
        {
            Currency = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(Currency);
        }
    }
}
