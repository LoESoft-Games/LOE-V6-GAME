#region

using core;

#endregion

namespace gameserver.networking.outgoing
{
    public class NEWTICK : OutgoingMessage
    {
        public int TickId { get; set; }
        public int TickTime { get; set; }
        public ObjectStats[] UpdateStatuses { get; set; }

        public override MessageID ID => MessageID.NEWTICK;

        public override Message CreateInstance() => new NEWTICK();

        protected override void Read(NReader rdr)
        {
            TickId = rdr.ReadInt32();
            TickTime = rdr.ReadInt32();

            UpdateStatuses = new ObjectStats[rdr.ReadInt16()];
            for (int i = 0; i < UpdateStatuses.Length; i++)
                UpdateStatuses[i] = ObjectStats.Read(rdr);
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(TickId);
            wtr.Write(TickTime);

            wtr.Write((ushort)UpdateStatuses.Length);
            foreach (ObjectStats i in UpdateStatuses)
                i.Write(wtr);
        }
    }
}