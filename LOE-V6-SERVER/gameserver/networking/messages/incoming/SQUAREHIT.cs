#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class SQUAREHIT : IncomingMessage
    {
        public int Time { get; set; }
        public byte BulletId { get; set; }
        public int ObjectId { get; set; }

        public override MessageID ID => MessageID.SQUAREHIT;

        public override Message CreateInstance() => new SQUAREHIT();

        protected override void Read(NReader rdr)
        {
            Time = rdr.ReadInt32();
            BulletId = rdr.ReadByte();
            ObjectId = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(Time);
            wtr.Write(BulletId);
            wtr.Write(ObjectId);
        }
    }
}