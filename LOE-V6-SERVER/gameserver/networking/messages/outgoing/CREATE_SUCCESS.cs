#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class CREATE_SUCCESS : OutgoingMessage
    {
        public int ObjectID { get; set; }
        public int CharacterID { get; set; }

        public override MessageID ID => MessageID.CREATE_SUCCESS;

        public override Message CreateInstance() => new CREATE_SUCCESS();

        protected override void Read(NReader rdr)
        {
            ObjectID = rdr.ReadInt32();
            CharacterID = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(ObjectID);
            wtr.Write(CharacterID);
        }
    }
}