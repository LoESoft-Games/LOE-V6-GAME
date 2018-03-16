#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class GOTO : OutgoingMessage
    {
        public int ObjectId { get; set; }
        public Position Position { get; set; }

        public override MessageID ID => MessageID.GOTO;

        public override Message CreateInstance() => new GOTO();

        protected override void Read(NReader rdr)
        {
            ObjectId = rdr.ReadInt32();
            Position = Position.Read(rdr);
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(ObjectId);
            Position.Write(wtr);
        }
    }
}