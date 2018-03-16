#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class JOINGUILD : IncomingMessage
    {
        public string GuildName { get; set; }

        public override MessageID ID => MessageID.JOINGUILD;

        public override Message CreateInstance() => new JOINGUILD();

        protected override void Read(NReader rdr)
        {
            GuildName = rdr.ReadUTF();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.WriteUTF(GuildName);
        }
    }
}