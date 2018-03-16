#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class SWITCH_MUSIC : OutgoingMessage
    {
        public string Music { get; set; }

        public override MessageID ID => MessageID.SWITCH_MUSIC;

        public override Message CreateInstance() => new SWITCH_MUSIC();

        protected override void Read(NReader rdr)
        {
            Music = rdr.ReadUTF();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.WriteUTF(Music);
        }
    }
}