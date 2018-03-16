#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class DELETE_PET : OutgoingMessage
    {
        public int PetId { get; set; }

        public override MessageID ID => MessageID.DELETE_PET;

        public override Message CreateInstance() => new DELETE_PET();

        protected override void Read(NReader rdr)
        {
            PetId = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(PetId);
        }
    }
}
