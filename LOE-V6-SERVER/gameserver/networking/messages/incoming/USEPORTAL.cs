#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class USEPORTAL : IncomingMessage
    {
        public int ObjectId { get; set; }

        public override MessageID ID => MessageID.USEPORTAL;

        public override Message CreateInstance() => new USEPORTAL();

        protected override void Read(NReader rdr)
        {
            ObjectId = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(ObjectId);
        }
    }
}