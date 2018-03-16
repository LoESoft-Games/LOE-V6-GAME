#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class QUEST_REDEEM_RESPONSE : IncomingMessage
    {
        public ObjectSlot Object { get; set; }

        public override MessageID ID => MessageID.QUEST_REDEEM_RESPONSE;

        public override Message CreateInstance() => new QUEST_REDEEM_RESPONSE();

        protected override void Read(NReader rdr)
        {
            Object = ObjectSlot.Read(rdr);
        }

        protected override void Write(NWriter wtr)
        {
            Object.Write(wtr);
        }
    }
}
