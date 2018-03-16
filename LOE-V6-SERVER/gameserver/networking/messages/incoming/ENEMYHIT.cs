#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class ENEMYHIT : IncomingMessage
    {
        public int Time { get; set; }
        public byte BulletId { get; set; }
        public int TargetId { get; set; }
        public bool Killed { get; set; }

        public override MessageID ID => MessageID.ENEMYHIT;

        public override Message CreateInstance() => new ENEMYHIT();

        protected override void Read(NReader rdr)
        {
            Time = rdr.ReadInt32();
            BulletId = rdr.ReadByte();
            TargetId = rdr.ReadInt32();
            Killed = rdr.ReadBoolean();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(Time);
            wtr.Write(BulletId);
            wtr.Write(TargetId);
            wtr.Write(Killed);
        }
    }
}