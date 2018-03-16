#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class INVDROP : IncomingMessage
    {
        public ObjectSlot SlotObject { get; set; }

        public override MessageID ID => MessageID.INVDROP;

        public override Message CreateInstance() => new INVDROP();

        protected override void Read(NReader rdr)
        {
            SlotObject = ObjectSlot.Read(rdr);
        }

        protected override void Write(NWriter wtr)
        {
            SlotObject.Write(wtr);
        }
    }
}