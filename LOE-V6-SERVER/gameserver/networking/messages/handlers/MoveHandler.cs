#region

using gameserver.networking.incoming;
using gameserver.realm;
using gameserver.realm.entity.player;
using gameserver.realm.terrain;

#endregion

namespace gameserver.networking.handlers
{
    internal class MoveHandler : MessageHandlers<MOVE>
    {
        public override MessageID ID => MessageID.MOVE;

        protected override void HandlePacket(Client client, MOVE packet) => client.Manager.Logic.AddPendingAction(t => Handle(client.Player, t, packet), PendingPriority.Networking);

        private void Handle(Player player, RealmTime time, MOVE packet)
        {
            if (player?.Owner == null || player.HasConditionEffect(ConditionEffectIndex.Paralyzed) || packet.Position.X == -1 || packet.Position.Y == -1)
                return;

            float newX = packet.Position.X;
            float newY = packet.Position.Y;

            if (newX != player.X || newY != player.Y)
            {
                player.Move(newX, newY);
                player.ClientTick(time, packet);
                CheckLabConditions(player, packet);
                player.UpdateCount++;
            }
        }

        private static void CheckLabConditions(Entity player, MOVE packet)
        {
            WmapTile tile = player.Owner.Map[(int)packet.Position.X, (int)packet.Position.Y];
            switch (tile.TileId)
            {
                //Green water
                case 0xa9:
                case 0x82:
                    if (tile.ObjId != 0) return;
                    if (!player.HasConditionEffect(ConditionEffectIndex.Hexed) ||
                        !player.HasConditionEffect(ConditionEffectIndex.Stunned) ||
                        !player.HasConditionEffect(ConditionEffectIndex.Speedy))
                    {
                        player.ApplyConditionEffect(ConditionEffectIndex.Hexed);
                        player.ApplyConditionEffect(ConditionEffectIndex.Stunned);
                        player.ApplyConditionEffect(ConditionEffectIndex.Speedy);
                    }
                    break;
                //Blue water
                case 0xa7:
                case 0x83:
                    if (tile.ObjId != 0) return;
                    if (player.HasConditionEffect(ConditionEffectIndex.Hexed) ||
                        player.HasConditionEffect(ConditionEffectIndex.Stunned) ||
                        player.HasConditionEffect(ConditionEffectIndex.Speedy))
                    {
                        player.ApplyConditionEffect(ConditionEffectIndex.Hexed, 0);
                        player.ApplyConditionEffect(ConditionEffectIndex.Stunned, 0);
                        player.ApplyConditionEffect(ConditionEffectIndex.Speedy, 0);
                    }
                    break;
            }
        }
    }
}