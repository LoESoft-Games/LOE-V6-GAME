#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class EDITACCOUNTLIST : IncomingMessage
    {
        public int AccountListId { get; set; }
        public bool Add { get; set; }
        public int ObjectId { get; set; }

        public override MessageID ID => MessageID.EDITACCOUNTLIST;

        public override Message CreateInstance() => new EDITACCOUNTLIST();

        protected override void Read(NReader rdr)
        {
            AccountListId = rdr.ReadInt32();
            Add = rdr.ReadBoolean();
            ObjectId = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(AccountListId);
            wtr.Write(Add);
            wtr.Write(ObjectId);
        }
    }
}