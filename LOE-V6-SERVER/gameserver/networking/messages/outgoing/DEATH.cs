#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class DEATH : OutgoingMessage
    {
        public string AccountId { get; set; }
        public int CharId { get; set; }
        public string Killer { get; set; }
        public int obf0 { get; set; }
        public int obf1 { get; set; }

        public override MessageID ID => MessageID.DEATH;

        public override Message CreateInstance() => new DEATH();

        protected override void Read(NReader rdr)
        {
            AccountId = rdr.ReadUTF();
            CharId = rdr.ReadInt32();
            Killer = rdr.ReadUTF();
            obf0 = rdr.ReadInt32();
            obf1 = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.WriteUTF(AccountId);
            wtr.Write(CharId);
            wtr.WriteUTF(Killer);
            wtr.Write(obf0);
            wtr.Write(obf1);
        }
    }
}