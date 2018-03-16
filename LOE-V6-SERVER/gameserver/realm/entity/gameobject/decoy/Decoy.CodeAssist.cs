#region

using Mono.Game;
using System;
using gameserver.realm.entity.player;

#endregion

namespace gameserver.realm.entity
{
    partial class Decoy
    {
        public void Damage(int dmg, Entity chr, bool NoDef) { }

        public bool IsVisibleToEnemy() { return true; }

        private Vector2 GetRandDirection()
        {
            double angle = rand.NextDouble() * 2 * Math.PI;
            return new Vector2(
                (float)Math.Cos(angle),
                (float)Math.Sin(angle)
                );
        }

        public static Decoy DecoyRandom(RealmManager manager, Player player, int duration, float tps)
        {
            Decoy d = new Decoy(manager, player, duration, tps);
            d.direction = d.GetRandDirection();
            return d;
        }
    }
}
