#region

using System.Collections.Generic;

#endregion

namespace gameserver.realm.entity
{
    partial class Decoy
    {
        protected override void ExportStats(IDictionary<StatsType, object> stats)
        {
            stats[StatsType.Texture1] = player.Texture1;
            stats[StatsType.Texture2] = player.Texture2;
            base.ExportStats(stats);
        }
    }
}
