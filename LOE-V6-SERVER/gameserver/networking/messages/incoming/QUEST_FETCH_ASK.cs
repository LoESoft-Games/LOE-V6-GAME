#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class QUEST_FETCH_ASK : IncomingMessage
    {
        public override MessageID ID => MessageID.QUEST_FETCH_ASK;

        public override Message CreateInstance() => new QUEST_FETCH_ASK();

        protected override void Read(NReader rdr) { }

        protected override void Write(NWriter wtr) { }
    }
}
