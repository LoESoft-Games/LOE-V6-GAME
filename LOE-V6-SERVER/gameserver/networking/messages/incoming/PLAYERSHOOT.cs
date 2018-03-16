#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class PLAYERSHOOT : IncomingMessage
    {
        public int Time { get; set; }
        public byte BulletId { get; set; }
        public short ContainerType { get; set; }
        public Position Position { get; set; }
        public float Angle { get; set; }

        public override MessageID ID => MessageID.PLAYERSHOOT;

        public override Message CreateInstance() => new PLAYERSHOOT();

        protected override void Read(NReader rdr)
        {
            Time = rdr.ReadInt32();
            BulletId = rdr.ReadByte();
            ContainerType = rdr.ReadInt16();
            Position = Position.Read(rdr);
            Angle = rdr.ReadSingle();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(Time);
            wtr.Write(BulletId);
            wtr.Write(ContainerType);
            Position.Write(wtr);
            wtr.Write(Angle);
        }
    }
}