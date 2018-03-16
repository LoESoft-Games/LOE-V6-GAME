#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class CHANGEGUILDRANK : IncomingMessage
    {
        public string Name { get; set; }
        public int GuildRank { get; set; }

        public override MessageID ID => MessageID.CHANGEGUILDRANK;

        public override Message CreateInstance() => new CHANGEGUILDRANK();

        protected override void Read(NReader rdr)
        {
            Name = rdr.ReadUTF();
            GuildRank = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.WriteUTF(Name);
            wtr.Write(GuildRank);
        }
    }
}