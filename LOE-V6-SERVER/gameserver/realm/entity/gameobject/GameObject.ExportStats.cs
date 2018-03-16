#region

using System.Collections.Generic;

#endregion

namespace gameserver.realm.entity
{
    partial class GameObject
    {
        protected override void ExportStats(IDictionary<StatsType, object> stats)
        {
            if (!Vulnerable)
                stats[StatsType.HP] = 0;
            else
                stats[StatsType.HP] = HP;
            base.ExportStats(stats);
        }
    }
}
