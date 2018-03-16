#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class ENEMYSHOOT : OutgoingMessage
    {
        public byte BulletId { get; set; }
        public int OwnerId { get; set; }
        public byte BulletType { get; set; }
        public Position Position { get; set; }
        public float Angle { get; set; }
        public short Damage { get; set; }
        public byte NumShots { get; set; }
        public float AngleInc { get; set; }

        public override MessageID ID => MessageID.ENEMYSHOOT;

        public override Message CreateInstance() => new ENEMYSHOOT();

        protected override void Read(NReader rdr)
        {
            BulletId = rdr.ReadByte();
            OwnerId = rdr.ReadInt32();
            BulletType = rdr.ReadByte();
            Position = Position.Read(rdr);
            Angle = rdr.ReadSingle();
            Damage = rdr.ReadInt16();
            if (rdr.BaseStream.Length - rdr.BaseStream.Position <= 0) return;
            NumShots = rdr.ReadByte();
            AngleInc = rdr.ReadSingle();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(BulletId);
            wtr.Write(OwnerId);
            wtr.Write(BulletType);
            Position.Write(wtr);
            wtr.Write(Angle);
            wtr.Write(Damage);
            if (NumShots == 1 || AngleInc == 0) return;
            wtr.Write(NumShots);
            wtr.Write(AngleInc);
        }
    }
}