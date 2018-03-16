#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class ACCEPT_ARENA_DEATH : IncomingMessage
    {
        public int _li { get; set; }

        public override MessageID ID => MessageID.ACCEPT_ARENA_DEATH;

        public override Message CreateInstance() => new ACCEPT_ARENA_DEATH();

        protected override void Read(NReader rdr)
        {
            _li = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(_li);
        }
    }
}
