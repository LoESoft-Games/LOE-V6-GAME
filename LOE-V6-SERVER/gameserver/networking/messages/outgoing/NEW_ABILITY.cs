#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class NEW_ABILITY : OutgoingMessage
    {
        public Ability Type { get; set; }

        public override MessageID ID => MessageID.NEW_ABILITY;

        public override Message CreateInstance() => new NEW_ABILITY();

        protected override void Read(NReader rdr)
        {
            Type = (Ability)rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write((int)Type);
        }
    }
}
