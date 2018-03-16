#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class INVITEDTOGUILD : OutgoingMessage
    {
        public string Name { get; set; }
        public string GuildName { get; set; }

        public override MessageID ID => MessageID.INVITEDTOGUILD;

        public override Message CreateInstance() => new INVITEDTOGUILD();

        protected override void Read(NReader rdr)
        {
            Name = rdr.ReadUTF();
            GuildName = rdr.ReadUTF();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.WriteUTF(Name);
            wtr.WriteUTF(GuildName);
        }
    }
}