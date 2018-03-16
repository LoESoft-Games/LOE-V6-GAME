#region

using System;
using gameserver.networking.incoming;
using gameserver.realm;
using gameserver.realm.terrain;

#endregion

namespace gameserver.networking.handlers
{
    internal class GroundDamageHandler : MessageHandlers<GROUNDDAMAGE>
    {
        public override MessageID ID => MessageID.GROUNDDAMAGE;

        protected override void HandlePacket(Client client, GROUNDDAMAGE packet)
        {
            client.Manager.Logic.AddPendingAction(t =>
            {
                if (client.Player.HasConditionEffect(ConditionEffectIndex.Paused) ||
                    client.Player.HasConditionEffect(ConditionEffectIndex.Invincible) ||
                    client.Player.HasConditionEffect(ConditionEffectIndex.Invulnerable))
                    return;

                try
                {
                    if (client.Player.Owner == null) return;
                    WmapTile tile = client.Player.Owner.Map[(int)packet.Position.X, (int)packet.Position.Y];
                    ObjectDesc objDesc = tile.ObjType == 0 ? null : client.Manager.GameData.ObjectDescs[tile.ObjType];
                    TileDesc tileDesc = client.Manager.GameData.Tiles[tile.TileId];
                    if (tileDesc.Damaging && (objDesc == null || !objDesc.ProtectFromGroundDamage))
                    {
                        int dmg = (int)client.Player.StatsManager.Random.obf6((uint)tileDesc.MinDamage, (uint)tileDesc.MaxDamage);
                        dmg = (int)client.Player.StatsManager.GetDefenseDamage(dmg, true);

                        client.Player.HP -= dmg;
                        client.Player.UpdateCount++;
                        if (client.Player.HP <= 0)
                            client.Player.Death(tileDesc.ObjectId);
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }, PendingPriority.Networking);
        }
    }
}