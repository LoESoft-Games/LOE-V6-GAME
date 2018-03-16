#region

using core;

#endregion

namespace gameserver.networking.incoming
{
    public class SETCONDITION : IncomingMessage
    {
        public int ConditionEffect { get; set; }
        public float ConditionDuration { get; set; }

        public override MessageID ID => MessageID.SETCONDITION;

        public override Message CreateInstance() => new SETCONDITION();

        protected override void Read(NReader rdr)
        {
            ConditionEffect = rdr.ReadInt32();
            ConditionDuration = rdr.ReadSingle();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(ConditionEffect);
            wtr.Write(ConditionDuration);
        }
    }
}