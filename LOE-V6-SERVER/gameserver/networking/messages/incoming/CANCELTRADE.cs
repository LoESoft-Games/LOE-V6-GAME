#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class CANCELTRADE : IncomingMessage
    {
        public override MessageID ID => MessageID.CANCELTRADE;

        public override Message CreateInstance() => new CANCELTRADE();

        protected override void Read(NReader rdr) { }

        protected override void Write(NWriter wtr) { }
    }
}