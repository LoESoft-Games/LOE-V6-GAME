#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class HATCH_PET : OutgoingMessage
    {
        public string PetName { get; set; }
        public int PetSkinId { get; set; }

        public override MessageID ID => MessageID.HATCH_PET;

        public override Message CreateInstance() => new HATCH_PET();

        protected override void Read(NReader rdr)
        {
            PetName = rdr.ReadUTF();
            PetSkinId = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.WriteUTF(PetName);
            wtr.Write(PetSkinId);
        }
    }
}
