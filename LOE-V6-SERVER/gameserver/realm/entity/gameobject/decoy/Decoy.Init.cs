#region

using Mono.Game;
using gameserver.networking.outgoing;
using gameserver.realm.entity.player;

#endregion

namespace gameserver.realm.entity
{
    partial class Decoy : GameObject, IPlayer
    {
        public Decoy(RealmManager manager, Player player, int duration, float tps)
            : base(manager, 0x0715, duration, true, true, true)
        {
            this.player = player;
            this.duration = duration;
            speed = tps;

            Position? history = player.TryGetHistory(1);
            if (history == null)
                direction = GetRandDirection();
            else
            {
                direction = new Vector2(player.X - history.Value.X, player.Y - history.Value.Y);
                if (direction.LengthSquared() == 0)
                    direction = GetRandDirection();
                else
                    direction.Normalize();
            }
        }

        public override void Tick(RealmTime time)
        {
            if (HP > duration / 2)
            {
                this.ValidateAndMove(
                    X + direction.X * speed * time.ElapsedMsDelta / 1000,
                    Y + direction.Y * speed * time.ElapsedMsDelta / 1000
                );
            }
            if (HP < 250 && !exploded)
            {
                exploded = true;
                Owner.BroadcastPacket(new SHOWEFFECT()
                {
                    EffectType = EffectType.Nova,
                    Color = new ARGB(0xffff0000),
                    TargetId = Id,
                    PosA = new Position() { X = 1 }
                }, null);
            }
            base.Tick(time);
        }
    }
}