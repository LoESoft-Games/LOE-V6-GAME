#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class LOAD : IncomingMessage
    {
        public int CharacterId { get; set; }
        public bool IsFromArena { get; set; }

        public override MessageID ID => MessageID.LOAD;

        public override Message CreateInstance() => new LOAD();

        protected override void Read(NReader rdr)
        {
            CharacterId = rdr.ReadInt32();
            IsFromArena = rdr.ReadBoolean();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(CharacterId);
            wtr.Write(IsFromArena);
        }
    }
}