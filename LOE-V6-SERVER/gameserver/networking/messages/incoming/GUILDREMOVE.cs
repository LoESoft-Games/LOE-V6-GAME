#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class GUILDREMOVE : IncomingMessage
    {
        public string Name { get; set; }

        public override MessageID ID => MessageID.GUILDREMOVE;

        public override Message CreateInstance() => new GUILDREMOVE();

        protected override void Read(NReader rdr)
        {
            Name = rdr.ReadUTF();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.WriteUTF(Name);
        }
    }
}