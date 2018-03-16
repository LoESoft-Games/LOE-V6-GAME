#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class EVOLVE_PET : OutgoingMessage
    {
        public int PetId1 { get; set; }
        public int SkinId1 { get; set; }
        public int SkinId2 { get; set; }

        public override MessageID ID => MessageID.EVOLVE_PET;

        public override Message CreateInstance() => new EVOLVE_PET();

        protected override void Read(NReader rdr)
        {
            PetId1 = rdr.ReadInt32();
            SkinId1 = rdr.ReadInt32();
            SkinId2 = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(PetId1);
            wtr.Write(SkinId1);
            wtr.Write(SkinId2);
        }
    }
}
