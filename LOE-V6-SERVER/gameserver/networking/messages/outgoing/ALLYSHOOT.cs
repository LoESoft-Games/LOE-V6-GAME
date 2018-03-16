#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class ALLYSHOOT : OutgoingMessage
    {
        public byte BulletId { get; set; }
        public int OwnerId { get; set; }
        public short ContainerType { get; set; }
        public float Angle { get; set; }

        public override MessageID ID => MessageID.ALLYSHOOT;

        public override Message CreateInstance() => new ALLYSHOOT();

        protected override void Read(NReader rdr)
        {
            BulletId = rdr.ReadByte();
            OwnerId = rdr.ReadInt32();
            ContainerType = rdr.ReadInt16();
            Angle = rdr.ReadSingle();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(BulletId);
            wtr.Write(OwnerId);
            wtr.Write(ContainerType);
            wtr.Write(Angle);
        }
    }
}