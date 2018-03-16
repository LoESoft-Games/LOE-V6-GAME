#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class IMMINENT_ARENA_WAVE : OutgoingMessage
    {
        public int Type { get; set; } //Not sure for what the type is, but u need it

        public override MessageID ID => MessageID.IMMINENT_ARENA_WAVE;

        public override Message CreateInstance() => new IMMINENT_ARENA_WAVE();

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