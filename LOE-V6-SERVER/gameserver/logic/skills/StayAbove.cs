#region

using Mono.Game;
using gameserver.realm;
using gameserver.realm.terrain;

#endregion

namespace gameserver.logic.behaviors
{
    public class StayAbove : CycleBehavior
    {
        //State storage: none

        private readonly int altitude;
        private readonly float speed;

        public StayAbove(double speed, int altitude)
        {
            this.speed = (float)speed;
            this.altitude = altitude;
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
            Status = CycleStatus.NotStarted;

            if (host.HasConditionEffect(ConditionEffectIndex.Paralyzed)) return;

            Wmap map = host.Owner.Map;
            WmapTile tile = map[(int)host.X, (int)host.Y];
            if (tile.Elevation != 0 && tile.Elevation < altitude)
            {
                Vector2 vect;
                vect = new Vector2(map.Width / 2 - host.X, map.Height / 2 - host.Y);
                vect.Normalize();
                float dist = host.GetSpeed(speed - (speed * 2.5f / (time.TickCount / (time.TotalElapsedMs / 1000f))), time);
                host.ValidateAndMove(host.X + vect.X * (dist - (dist * 2.5f / (time.TickCount / (time.TotalElapsedMs / 1000f)))), host.Y + vect.Y * (dist - (dist * 2.5f / (time.TickCount / (time.TotalElapsedMs / 1000f)))));
                host.UpdateCount++;

                Status = CycleStatus.InProgress;
            }
            else
                Status = CycleStatus.Completed;
        }
    }
}