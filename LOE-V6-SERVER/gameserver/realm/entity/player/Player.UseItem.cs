#region

using System;
using System.Collections.Generic;
using System.Linq;
using gameserver.networking;
using gameserver.networking.incoming;
using gameserver.networking.outgoing;
using static gameserver.networking.Client;

#endregion

namespace gameserver.realm.entity.player
{
    partial class Player
    {
        public bool Activate(RealmTime time, Item item, USEITEM pkt)
        {
            bool endMethod = false;
            Position target = pkt.ItemUsePos;
            Mp -= item.MpCost;
            IContainer con = Owner?.GetEntity(pkt.SlotObject.ObjectId) as IContainer;
            if (con == null) return true;
            if (CheatEngineDetectSlot(item, pkt, con)) CheatEngineDetect(item, pkt);
            if (item.IsBackpack) Backpack();
            if (item.XpBooster) XpBooster(item);
            if (item.LootDropBooster) LootDropBooster(item);
            if (item.LootTierBooster) LootTierBooster(item);

            foreach (ActivateEffect eff in item.ActivateEffects)
            {
                switch (eff.Effect)
                {
                    case ActivateEffects.BulletNova: BulletNova(time, item, target); break;
                    case ActivateEffects.Shoot: Shoot(time, item, target); break;
                    case ActivateEffects.StatBoostSelf: StatBoostSelf(eff); break;
                    case ActivateEffects.StatBoostAura: StatBoostAura(eff); break;
                    case ActivateEffects.ConditionEffectSelf: ConditionEffectSelf(eff); break;
                    case ActivateEffects.ConditionEffectAura: ConditionEffectAura(eff); break;
                    case ActivateEffects.Heal: Heal(eff); break;
                    case ActivateEffects.HealNova: HealNova(eff); break;
                    case ActivateEffects.Magic: Magic(eff); break;
                    case ActivateEffects.MagicNova: MagicNova(eff); break;
                    case ActivateEffects.Teleport: Teleport(target); break;
                    case ActivateEffects.VampireBlast: VampireBlast(time, eff, target); break;
                    case ActivateEffects.Trap: Trap(time, eff, target); break;
                    case ActivateEffects.StasisBlast: StasisBlast(eff, target); break;
                    case ActivateEffects.Decoy: Decoy(eff); break;
                    case ActivateEffects.Lightning: Lightning(time, eff, target); break;
                    case ActivateEffects.PoisonGrenade: PoisonGrenade(eff, target); break;
                    case ActivateEffects.RemoveNegativeConditions: RemoveNegativeConditions(eff); break;
                    case ActivateEffects.RemoveNegativeConditionsSelf: RemoveNegativeConditionsSelf(); break;
                    case ActivateEffects.IncrementStat: IncrementStat(eff); break;
                    case ActivateEffects.UnlockPortal: UnlockPortal(eff); break;
                    case ActivateEffects.Create: Create(eff); break;
                    case ActivateEffects.Dye: Dye(item); break;
                    case ActivateEffects.ShurikenAbility: ShurikenAbility(time, item, target, pkt); break;
                    case ActivateEffects.UnlockSkin: UnlockSkin(item, endMethod); break;
                    case ActivateEffects.PermaPet: PermaPet(); break;
                    case ActivateEffects.Pet: Pet(eff); break;
                    case ActivateEffects.CreatePet: CreatePet(); break;
                    case ActivateEffects.MysteryPortal: MysteryPortal(); break;
                    case ActivateEffects.GenericActivate: GenericActivate(eff, target); break;
                    case ActivateEffects.PetSkin: PetSkin(); break;
                    case ActivateEffects.Unlock: Unlock(); break;
                    case ActivateEffects.MysteryDyes: MysteryDyes(); break;
                    case ActivateEffects.Exchange: Exchange(); break;
                }
            }
            UpdateCount++;
            return endMethod;
        }

        #region "Activate Code Assit"

        public static void ActivateHealHp(Player player, int amount, List<Message> pkts)
        {
            int maxHp = player.Stats[0] + player.Boost[0];
            int newHp = Math.Min(maxHp, player.HP + amount);
            if (newHp != player.HP)
            {
                pkts.Add(new SHOWEFFECT
                {
                    EffectType = EffectType.Heal,
                    TargetId = player.Id,
                    Color = new ARGB(0xffffffff)
                });
                pkts.Add(new NOTIFICATION
                {
                    Color = new ARGB(0xff00ff00),
                    ObjectId = player.Id,
                    Text = "{\"key\":\"blank\",\"tokens\":{\"data\":\"+" + (newHp - player.HP) + "\"}}"
                    //"+" + (newHp - player.HP)
                });
                player.HP = newHp;
                player.UpdateCount++;
            }
        }

        private static void ActivateHealMp(Player player, int amount, List<Message> pkts)
        {
            int maxMp = player.Stats[1] + player.Boost[1];
            int newMp = Math.Min(maxMp, player.Mp + amount);
            if (newMp != player.Mp)
            {
                pkts.Add(new SHOWEFFECT
                {
                    EffectType = EffectType.Heal,
                    TargetId = player.Id,
                    Color = new ARGB(0x6084e0)
                });
                pkts.Add(new NOTIFICATION
                {
                    Color = new ARGB(0x6084e0),
                    ObjectId = player.Id,
                    Text = "{\"key\":\"blank\",\"tokens\":{\"data\":\"+" + (newMp - player.Mp) + "\"}}"
                });
                player.Mp = newMp;
                player.UpdateCount++;
            }
        }

        private static void ActivateBoostStat(Player player, int idxnew, List<Message> pkts)
        {
            int OriginalStat = 0;
            OriginalStat = player.Stats[idxnew] + OriginalStat;
            oldstat = OriginalStat;
        }

        private void Shoot(RealmTime time, Item item, Position target)
        {
            double arcGap = item.ArcGap * Math.PI / 180;
            double startAngle = Math.Atan2(target.Y - Y, target.X - X) - (item.NumProjectiles - 1) / 2 * arcGap;
            ProjectileDesc prjDesc = item.Projectiles[0]; //Assume only one

            for (int i = 0; i < item.NumProjectiles; i++)
            {
                Projectile proj = CreateProjectile(prjDesc, item.ObjectType,
                    (int)StatsManager.GetAttackDamage(prjDesc.MinDamage, prjDesc.MaxDamage),
                    time.TotalElapsedMs, new Position { X = X, Y = Y }, (float)(startAngle + arcGap * i));
                Owner?.EnterWorld(proj);
                FameCounter.Shoot(proj);
            }
        }

        private void PoisonEnemy(Enemy enemy, ActivateEffect eff)
        {
            try
            {
                if (eff.ConditionEffect != null)
                    enemy?.ApplyConditionEffect(new[]
                    {
                        new ConditionEffect
                        {
                            Effect = (ConditionEffectIndex) eff.ConditionEffect,
                            DurationMS = (int) eff.EffectDuration
                        }
                    });
                int remainingDmg = (int)StatsManager.GetDefenseDamage(enemy, eff.TotalDamage, enemy.ObjectDesc.Defense);
                int perDmg = remainingDmg * 1000 / eff.DurationMS;
                WorldTimer tmr = null;
                int x = 0;
                tmr = new WorldTimer(100, (w, t) =>
                {
                    if (enemy.Owner == null) return;
                    w.BroadcastPacket(new SHOWEFFECT
                    {
                        EffectType = EffectType.Poison,
                        TargetId = enemy.Id,
                        Color = new ARGB(0xffddff00)
                    }, null);

                    if (x % 10 == 0)
                    {
                        int thisDmg;
                        if (remainingDmg < perDmg) thisDmg = remainingDmg;
                        else thisDmg = perDmg;

                        enemy.Damage(this, t, thisDmg, true);
                        remainingDmg -= thisDmg;
                        if (remainingDmg <= 0) return;
                    }
                    x++;

                    tmr.Reset();

                    Manager.Logic.AddPendingAction(_ => w.Timers.Add(tmr), PendingPriority.Creation);
                });
                Owner?.Timers.Add(tmr);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private bool CheatEngineDetectSlot(Item item, USEITEM pkt, IContainer con) => pkt?.SlotObject.SlotId != 255 && pkt?.SlotObject.SlotId != 254 && con?.Inventory[pkt.SlotObject.SlotId] != item;

        private bool CheatEngineDetect(Item item, USEITEM pkt)
        {
            log.FatalFormat("Cheat engine detected for player {0},\nItem should be {1}, but its {2}.",
                Name, Inventory[pkt.SlotObject.SlotId].ObjectId, item.ObjectId);
            foreach (Player player in Owner?.Players.Values)
                if (player?.Client.Account.Rank >= 2)
                    player.SendInfo(string.Format("Cheat engine detected for player {0},\nItem should be {1}, but its {2}.",
                Name, Inventory[pkt.SlotObject.SlotId].ObjectId, item.ObjectId));
            Client?.Disconnect(DisconnectReason.CHEAT_ENGINE_DETECTED);
            return true;
        }

        private bool Backpack()
        {
            if (HasBackpack)
                return true;
            Client.Character.Backpack = new[] { -1, -1, -1, -1, -1, -1, -1, -1 };
            HasBackpack = true;
            Client.Character.HasBackpack = true;
            Client?.Save();
            Array.Resize(ref inventory, 20);
            int[] slotTypes =
                Utils.FromCommaSepString32(
                    Manager.GameData.ObjectTypeToElement[ObjectType].Element("SlotTypes").Value);
            Array.Resize(ref slotTypes, 20);
            for (int i = 0; i < slotTypes.Length; i++)
                if (slotTypes[i] == 0) slotTypes[i] = 10;
            SlotTypes = slotTypes;
            return false;
        }

        private bool XpBooster(Item item)
        {
            if (!XpBoosted)
            {
                XpBoostTimeLeft = (float)item.Timer;
                XpBoosted = item.XpBooster;
                xpFreeTimer = (float)item.Timer == -1.0 ? false : true;
                return false;
            }
            else
            {
                SendInfo("You have already an active XP Booster.");
                return true;
            }
        }

        private bool LootDropBooster(Item item)
        {
            if (!LootDropBoost)
            {
                LootDropBoostTimeLeft = (float)item.Timer;
                lootDropBoostFreeTimer = (float)item.Timer == -1.0 ? false : true;
                return false;
            }
            else
            {
                SendInfo("You have already an active Loot Drop Booster.");
                return true;
            }
        }

        private bool LootTierBooster(Item item)
        {
            if (!LootTierBoost)
            {
                LootTierBoostTimeLeft = (float)item.Timer;
                lootTierBoostFreeTimer = (float)item.Timer == -1.0 ? false : true;
                return false;
            }
            else
            {
                SendInfo("You have already an active Loot Tier Booster.");
                return true;
            }
        }

        private void BulletNova(RealmTime time, Item item, Position target)
        {
            Projectile[] prjs = new Projectile[20];
            ProjectileDesc prjDesc = item.Projectiles[0];
            var batch = new Message[21];

            for (var i = 0; i < 20; i++)
            {
                Projectile proj = CreateProjectile(prjDesc, item.ObjectType,
                    Random.Next(prjDesc.MinDamage, prjDesc.MaxDamage),
                    time.TotalElapsedMs, target, (float)(i * (Math.PI * 2) / 20));
                Owner?.EnterWorld(proj);
                FameCounter.Shoot(proj);
                batch[i] = new SERVERPLAYERSHOOT()
                {
                    BulletId = proj.ProjectileId,
                    OwnerId = Id,
                    ContainerType = item.ObjectType,
                    StartingPos = target,
                    Angle = proj.Angle,
                    Damage = (short)proj.Damage
                };
                prjs[i] = proj;
            }

            batch[20] = new SHOWEFFECT()
            {
                EffectType = EffectType.Line,
                PosA = target,
                TargetId = Id,
                Color = new ARGB(0xFFFF00AA)
            };

            foreach (Player plr in Owner?.Players.Values.Where(p => p?.DistSqr(this) < RadiusSqr))
                plr?.Client.SendMessage(batch);
        }

        private void StatBoostSelf(ActivateEffect eff)
        {
            int idx = -1;

            if (eff.Stats == StatsType.MaximumHP) idx = 0;
            else if (eff.Stats == StatsType.MaximumMP) idx = 1;
            else if (eff.Stats == StatsType.Attack) idx = 2;
            else if (eff.Stats == StatsType.Defense) idx = 3;
            else if (eff.Stats == StatsType.Speed) idx = 4;
            else if (eff.Stats == StatsType.Dexterity) idx = 5;
            else if (eff.Stats == StatsType.Vitality) idx = 6;
            else if (eff.Stats == StatsType.Wisdom) idx = 7;

            List<Message> pkts = new List<Message>();

            ActivateBoostStat(this, idx, pkts);
            int OGstat = oldstat;
            int bit = idx + 39;

            int s = eff.Amount;
            Boost[idx] += s;
            ApplyConditionEffect(new ConditionEffect
            {
                DurationMS = eff.DurationMS,
                Effect = (ConditionEffectIndex)bit
            });
            UpdateCount++;
            Owner?.Timers.Add(new WorldTimer(eff.DurationMS, (world, t) =>
            {
                Boost[idx] = OGstat;
                UpdateCount++;
            }));
            Owner?.BroadcastPacket(new SHOWEFFECT
            {
                EffectType = EffectType.Heal,
                TargetId = Id,
                Color = new ARGB(0xffffffff)
            }, null);
        }

        private void StatBoostAura(ActivateEffect eff)
        {
            int idx = -1;

            if (eff.Stats == StatsType.MaximumHP) idx = 0;
            if (eff.Stats == StatsType.MaximumMP) idx = 1;
            if (eff.Stats == StatsType.Attack) idx = 2;
            if (eff.Stats == StatsType.Defense) idx = 3;
            if (eff.Stats == StatsType.Speed) idx = 4;
            if (eff.Stats == StatsType.Dexterity) idx = 5;
            if (eff.Stats == StatsType.Vitality) idx = 6;
            if (eff.Stats == StatsType.Wisdom) idx = 7;

            int bit = idx + 39;

            int amountSBA = eff.Amount;
            int durationSBA = eff.DurationMS;
            float rangeSBA = eff.Range;
            bool noStack = eff.NoStack;
            if (eff.UseWisMod)
            {
                amountSBA = (int)UseWisMod(eff.Amount, 0);
                durationSBA = (int)(UseWisMod(eff.DurationSec) * 1000);
                rangeSBA = UseWisMod(eff.Range);
            }

            this?.Aoe(rangeSBA, true, player =>
            {
                // TODO support for noStack StatBoostAura attribute (paladin total hp increase / insta heal)
                if (!noStack)
                {
                    ApplyConditionEffect(new ConditionEffect
                    {
                        DurationMS = durationSBA,
                        Effect = (ConditionEffectIndex)bit
                    });
                    ActivateBoost[idx].Push(amountSBA);
                    HP += amountSBA;
                    CalcBoost();
                    player.UpdateCount++;
                    Owner?.Timers.Add(new WorldTimer(durationSBA, (world, t) =>
                    {
                        ActivateBoost[idx].Pop(amountSBA);
                        CalcBoost();
                        player.UpdateCount++;
                    }));
                }
                else
                {
                    if (!player.HasConditionEffect(ConditionEffectIndex.HPBoost))
                    {
                        ActivateBoost[idx].Push(amountSBA);
                        HP += amountSBA;
                        CalcBoost();
                        ApplyConditionEffect(new ConditionEffect
                        {
                            DurationMS = durationSBA,
                            Effect = (ConditionEffectIndex)bit
                        });
                        player.UpdateCount++;
                        Owner?.Timers.Add(new WorldTimer(durationSBA, (world, t) =>
                        {
                            ActivateBoost[idx].Pop(amountSBA);
                            CalcBoost();
                            player.UpdateCount++;
                        }));
                    }
                }
            });
            BroadcastSync(new SHOWEFFECT()
            {
                EffectType = EffectType.Nova,
                TargetId = Id,
                Color = new ARGB(0xffffffff),
                PosA = new Position() { X = rangeSBA }
            }, p => this?.Dist(p) < 25);
        }

        private void ConditionEffectSelf(ActivateEffect eff)
        {
            int durationCES = eff.DurationMS;
            if (eff.UseWisMod)
                durationCES = (int)(UseWisMod(eff.DurationSec) * 1000);

            uint color = 0xffffffff;
            switch (eff.ConditionEffect.Value)
            {
                case ConditionEffectIndex.Damaging:
                    color = 0xffff0000;
                    break;
                case ConditionEffectIndex.Berserk:
                    color = 0x808080;
                    break;
            }

            ApplyConditionEffect(new ConditionEffect
            {
                Effect = eff.ConditionEffect.Value,
                DurationMS = durationCES
            });
            Owner?.BroadcastPacket(new SHOWEFFECT
            {
                EffectType = EffectType.Nova,
                TargetId = Id,
                Color = new ARGB(color),
                PosA = new Position { X = 2F }
            }, null);
        }

        private void ConditionEffectAura(ActivateEffect eff)
        {
            int durationCEA = eff.DurationMS;
            float rangeCEA = eff.Range;
            if (eff.UseWisMod)
            {
                durationCEA = (int)(UseWisMod(eff.DurationSec) * 1000);
                rangeCEA = UseWisMod(eff.Range);
            }

            this?.Aoe(rangeCEA, true, player =>
            {
                player.ApplyConditionEffect(new ConditionEffect
                {
                    Effect = eff.ConditionEffect.Value,
                    DurationMS = durationCEA
                });
            });

            uint color = 0xffffffff;
            switch (eff.ConditionEffect.Value)
            {
                case ConditionEffectIndex.Damaging:
                    color = 0xffff0000;
                    break;
                case ConditionEffectIndex.Berserk:
                    color = 0x808080;
                    break;
            }

            BroadcastSync(new SHOWEFFECT
            {
                EffectType = EffectType.Nova,
                TargetId = Id,
                Color = new ARGB(color),
                PosA = new Position { X = rangeCEA }
            }, p => this?.Dist(p) < 25);
        }

        private void Heal(ActivateEffect eff)
        {
            List<Message> pkts = new List<Message>();
            ActivateHealHp(this, eff.Amount, pkts);
            Owner?.BroadcastPackets(pkts, null);
        }

        private void HealNova(ActivateEffect eff)
        {
            var amountHN = eff.Amount;
            var rangeHN = eff.Range;
            if (eff.UseWisMod)
            {
                amountHN = (int)UseWisMod(eff.Amount, 0);
                rangeHN = UseWisMod(eff.Range);
            }

            List<Message> pkts = new List<Message>();
            this?.Aoe(rangeHN, true, player => { ActivateHealHp(player as Player, amountHN, pkts); });
            pkts.Add(new SHOWEFFECT
            {
                EffectType = EffectType.Nova,
                TargetId = Id,
                Color = new ARGB(0xffffffff),
                PosA = new Position { X = rangeHN }
            });
            BroadcastSync(pkts, p => this.Dist(p) < 25);
        }

        private void Magic(ActivateEffect eff)
        {
            List<Message> pkts = new List<Message>();
            ActivateHealMp(this, eff.Amount, pkts);
            Owner?.BroadcastPackets(pkts, null);
        }

        private void MagicNova(ActivateEffect eff)
        {
            List<Message> pkts = new List<Message>();
            this?.Aoe(eff.Range / 2, true, player => { ActivateHealMp(player as Player, eff.Amount, pkts); });
            pkts.Add(new SHOWEFFECT
            {
                EffectType = EffectType.Nova,
                TargetId = Id,
                Color = new ARGB(0xffffffff),
                PosA = new Position { X = eff.Range }
            });
            Owner?.BroadcastPackets(pkts, null);
        }

        private void Teleport(Position target)
        {
            Move(target.X, target.Y);
            UpdateCount++;
            Owner?.BroadcastPackets(new Message[]
            {
                new GOTO
                {
                    ObjectId = Id,
                    Position = new Position
                    {
                        X = X,
                        Y = Y
                    }
                },
                new SHOWEFFECT
                {
                    EffectType = EffectType.Teleport,
                    TargetId = Id,
                    PosA = new Position
                    {
                        X = X,
                        Y = Y
                    },
                    Color = new ARGB(0xFFFFFFFF)
                }
            }, null);
        }

        private void VampireBlast(RealmTime time, ActivateEffect eff, Position target)
        {
            List<Message> pkts = new List<Message>();
            pkts.Add(new SHOWEFFECT
            {
                EffectType = EffectType.Line,
                TargetId = Id,
                PosA = target,
                Color = new ARGB(0xFFFF0000)
            });
            pkts.Add(new SHOWEFFECT
            {
                EffectType = EffectType.Burst,
                Color = new ARGB(0xFFFF0000),
                TargetId = Id,
                PosA = target,
                PosB = new Position { X = target.X + eff.Radius, Y = target.Y }
            });

            int totalDmg = 0;
            List<Enemy> enemies = new List<Enemy>();
            Owner?.Aoe(target, eff.Radius, false, enemy =>
            {
                enemies.Add(enemy as Enemy);
                totalDmg += (enemy as Enemy).Damage(this, time, eff.TotalDamage, false);
            });
            List<Player> players = new List<Player>();
            this.Aoe(eff.Radius, true, player =>
            {
                players?.Add(player as Player);
                ActivateHealHp(player as Player, totalDmg, pkts);
            });

            if (enemies.Count > 0)
            {
                Random rand = new Random();
                for (int i = 0; i < 5; i++)
                {
                    Enemy a = enemies[rand.Next(0, enemies.Count)];
                    Player b = players[rand.Next(0, players.Count)];
                    pkts.Add(new SHOWEFFECT
                    {
                        EffectType = EffectType.Flow,
                        TargetId = b.Id,
                        PosA = new Position { X = a.X, Y = a.Y },
                        Color = new ARGB(0xffffffff)
                    });
                }
            }

            BroadcastSync(pkts, p => this.Dist(p) < 25);
        }

        private void Trap(RealmTime time, ActivateEffect eff, Position target)
        {
            BroadcastSync(new SHOWEFFECT
            {
                EffectType = EffectType.Throw,
                Color = new ARGB(0xff9000ff),
                TargetId = Id,
                PosA = target
            }, p => this.Dist(p) < 25);
            Owner?.Timers.Add(new WorldTimer(1500, (world, t) =>
            {
                Trap trap = new Trap(
                    this,
                    eff.Radius,
                    eff.TotalDamage,
                    eff.ConditionEffect ?? ConditionEffectIndex.Slowed,
                    eff.EffectDuration);
                trap?.Move(target.X, target.Y);
                world?.EnterWorld(trap);
            }));
        }

        private void StasisBlast(ActivateEffect eff, Position target)
        {
            List<Message> pkts = new List<Message>();

            pkts.Add(new SHOWEFFECT
            {
                EffectType = EffectType.Collapse,
                TargetId = Id,
                PosA = target,
                PosB = new Position { X = target.X + 3, Y = target.Y },
                Color = new ARGB(0xFF00D0)
            });
            Owner?.Aoe(target, 3, false, enemy =>
            {
                if (IsSpecial(enemy.ObjectType)) return;

                if (enemy.HasConditionEffect(ConditionEffectIndex.StasisImmune))
                {
                    if (!enemy.HasConditionEffect(ConditionEffectIndex.Invincible))
                    {
                        pkts.Add(new NOTIFICATION
                        {
                            ObjectId = enemy.Id,
                            Color = new ARGB(0xff00ff00),
                            Text = "{\"key\":\"blank\",\"tokens\":{\"data\":\"Immune\"}}"
                        });
                    }
                }
                else if (!enemy.HasConditionEffect(ConditionEffectIndex.Stasis))
                {
                    enemy.ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = ConditionEffectIndex.Stasis,
                        DurationMS = eff.DurationMS
                    });
                    Owner.Timers.Add(new WorldTimer(eff.DurationMS, (world, t) =>
                    {
                        enemy.ApplyConditionEffect(new ConditionEffect
                        {
                            Effect = ConditionEffectIndex.StasisImmune,
                            DurationMS = 3000
                        });
                    }));
                    pkts.Add(new NOTIFICATION
                    {
                        ObjectId = enemy.Id,
                        Color = new ARGB(0xffff0000),
                        Text = "{\"key\":\"blank\",\"tokens\":{\"data\":\"Stasis\"}}"
                    });
                }
            });
            BroadcastSync(pkts, p => this.Dist(p) < 25);
        }

        private void Decoy(ActivateEffect eff)
        {
            Decoy decoy = new Decoy(Manager, this, eff.DurationMS, StatsManager.GetSpeed());
            decoy?.Move(X, Y);
            Owner?.EnterWorld(decoy);
        }

        private void Lightning(RealmTime time, ActivateEffect eff, Position target)
        {
            Enemy start = null;
            double angle = Math.Atan2(target.Y - Y, target.X - X);
            double diff = Math.PI / 3;
            Owner?.Aoe(target, 6, false, enemy =>
            {
                if (!(enemy is Enemy)) return;
                double x = Math.Atan2(enemy.Y - Y, enemy.X - X);
                if (Math.Abs(angle - x) < diff)
                {
                    start = enemy as Enemy;
                    diff = Math.Abs(angle - x);
                }
            });
            if (start == null)
                return;

            Enemy current = start;
            Enemy[] targets = new Enemy[eff.MaxTargets];
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i] = current;
                Enemy next = current.GetNearestEntity(8, false,
                    enemy =>
                        enemy is Enemy &&
                        Array.IndexOf(targets, enemy) == -1 &&
                        this.Dist(enemy) <= 6) as Enemy;

                if (next == null) break;
                current = next;
            }

            List<Message> pkts = new List<Message>();
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] == null) break;
                if (targets[i].HasConditionEffect(ConditionEffectIndex.Invincible)) continue;
                Entity prev = i == 0 ? (Entity)this : targets[i - 1];
                targets[i]?.Damage(this, time, eff.TotalDamage, false);
                if (eff.ConditionEffect != null)
                    targets[i].ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = eff.ConditionEffect.Value,
                        DurationMS = (int)(eff.EffectDuration * 1000)
                    });
                pkts.Add(new SHOWEFFECT
                {
                    EffectType = EffectType.Lightning,
                    TargetId = prev.Id,
                    Color = new ARGB(0xffff0088),
                    PosA = new Position
                    {
                        X = targets[i].X,
                        Y = targets[i].Y
                    },
                    PosB = new Position { X = 350 }
                });
            }
            BroadcastSync(pkts, p => this?.Dist(p) < 25);
        }

        private void PoisonGrenade(ActivateEffect eff, Position target)
        {
            try
            {
                BroadcastSync(new SHOWEFFECT
                {
                    EffectType = EffectType.Throw,
                    Color = new ARGB(0xffddff00),
                    TargetId = Id,
                    PosA = target
                }, p => this?.Dist(p) < 25);
                Placeholder x = new Placeholder(Manager, 1500);
                x.Move(target.X, target.Y);
                Owner?.EnterWorld(x);
                try
                {
                    Owner.Timers.Add(new WorldTimer(1500, (world, t) =>
                    {
                        world.BroadcastPacket(new SHOWEFFECT
                        {
                            EffectType = EffectType.Nova,
                            Color = new ARGB(0xffddff00),
                            TargetId = x.Id,
                            PosA = new Position { X = eff.Radius }
                        }, null);
                        world.Aoe(target, eff.Radius, false,
                            enemy => PoisonEnemy(enemy as Enemy, eff));
                    }));
                }
                catch (Exception ex)
                {
                    log.ErrorFormat("Poison ShowEffect:\n{0}", ex);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Poisons General:\n{0}", ex);
            }
        }

        private void RemoveNegativeConditions(ActivateEffect eff)
        {
            this?.Aoe(eff.Range / 2, true, player => { ApplyConditionEffect(NegativeEffs); });
            BroadcastSync(new SHOWEFFECT
            {
                EffectType = EffectType.Nova,
                TargetId = Id,
                Color = new ARGB(0xffffffff),
                PosA = new Position { X = eff.Range / 2 }
            }, p => this?.Dist(p) < 25);
        }

        private void RemoveNegativeConditionsSelf()
        {
            ApplyConditionEffect(NegativeEffs);
            Owner?.BroadcastPacket(new SHOWEFFECT
            {
                EffectType = EffectType.Nova,
                TargetId = Id,
                Color = new ARGB(0xffffffff),
                PosA = new Position { X = 1 }
            }, null);
        }

        private void IncrementStat(ActivateEffect eff)
        {
            int idx = -1;

            if (eff.Stats == StatsType.MaximumHP) idx = 0;
            else if (eff.Stats == StatsType.MaximumMP) idx = 1;
            else if (eff.Stats == StatsType.Attack) idx = 2;
            else if (eff.Stats == StatsType.Defense) idx = 3;
            else if (eff.Stats == StatsType.Speed) idx = 4;
            else if (eff.Stats == StatsType.Vitality) idx = 5;
            else if (eff.Stats == StatsType.Wisdom) idx = 6;
            else if (eff.Stats == StatsType.Dexterity) idx = 7;

            Stats[idx] += eff.Amount;
            int limit =
                int.Parse(
                    Manager.GameData.ObjectTypeToElement[ObjectType].Element(
                        StatsManager.StatsIndexToName(idx))
                        .Attribute("max")
                        .Value);
            if (Stats[idx] > limit)
                Stats[idx] = limit;
            UpdateCount++;
        }

        private void UnlockPortal(ActivateEffect eff)
        {
            Portal portal = this.GetNearestEntity(5, Manager.GameData.IdToObjectType[eff.LockedName]) as Portal;

            Message[] packets = new Message[3];
            packets[0] = new SHOWEFFECT
            {
                EffectType = EffectType.Nova,
                Color = new ARGB(0xFFFFFF),
                PosA = new Position { X = 5 },
                TargetId = Id
            };

            if (portal == null)
                return;

            portal.Unlock(eff.DungeonName);

            packets[1] = new NOTIFICATION
            {
                Color = new ARGB(0x00FF00),
                Text =
                    "{\"key\":\"blank\",\"tokens\":{\"data\":\"Unlocked by " +
                    Name + "\"}}",
                ObjectId = Id
            };

            packets[2] = new TEXT
            {
                BubbleTime = 0,
                Stars = -1,
                Name = "",
                Text = eff.DungeonName + " Unlocked by " + Name + ".",
                NameColor = 0x123456,
                TextColor = 0x123456
            };

            BroadcastSync(packets);
        }

        private bool Create(ActivateEffect eff)
        {
            if (Stars >= 10 || Client.Account.Admin)
            {
                ushort objType;
                if (!Manager.GameData.IdToObjectType.TryGetValue(eff.Id, out objType) || !Manager.GameData.Portals.ContainsKey(objType))
                {
                    SendHelp("Dungeon not implemented yet.");
                    return true;
                }
                Entity entity = Resolve(Manager, objType);
                World w = Manager.GetWorld(Owner.Id); //can't use Owner here, as it goes out of scope
                int TimeoutTime = Manager.GameData.Portals[objType].TimeoutTime;
                string DungName = Manager.GameData.Portals[objType].DungeonName;

                ARGB c = new ARGB(0x00FF00);

                entity?.Move(X, Y);
                w?.EnterWorld(entity);

                w.BroadcastPacket(new NOTIFICATION
                {
                    Color = c,
                    Text = "{\"key\":\"blank\",\"tokens\":{\"data\":\"Opened by " + Name + "\"}}",
                    ObjectId = Client.Player.Id
                }, null);

                w.BroadcastPacket(new TEXT
                {
                    BubbleTime = 0,
                    Stars = -1,
                    Name = "",
                    Text = DungName + " opened by " + Name,
                    NameColor = 0x123456,
                    TextColor = 0x123456
                }, null);
                w?.Timers.Add(new WorldTimer(TimeoutTime * 1000,
                    (world, t) => //default portal close time * 1000
                    {
                        try
                        {
                            w?.LeaveWorld(entity);
                        }
                        catch (Exception ex)
                        {
                            log.ErrorFormat("Couldn't despawn portal.\n{0}", ex);
                        }
                    }));
                return false;
            }
            else
            {
                SendInfo("You need at least 10 stars to use Keys! Complete class quests to earn additional stars.");
                return true;
            }
        }

        private void Dye(Item item)
        {
            if (item.Texture1 != 0)
            {
                Texture1 = item.Texture1;
            }
            if (item.Texture2 != 0)
            {
                Texture2 = item.Texture2;
            }
            SaveToCharacter();
        }

        private void ShurikenAbility(RealmTime time, Item item, Position target, USEITEM pkt)
        {
            if (!ninjaShoot)
            {
                ApplyConditionEffect(new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Speedy,
                    DurationMS = -1
                });
                ninjaFreeTimer = true;
                ninjaShoot = true;
            }
            else
            {
                ApplyConditionEffect(new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Speedy,
                    DurationMS = 0
                });
                ushort obj;
                Manager.GameData.IdToObjectType.TryGetValue(item.ObjectId, out obj);
                if (Mp >= item.MpEndCost)
                {
                    Shoot(time, item, pkt.ItemUsePos);
                    Mp -= (int)item.MpEndCost;
                }
                targetlink = target;
                ninjaShoot = false;
            }
        }

        private void UnlockSkin(Item item, bool endMethod)
        {
            if (Client.Player.Owner.Name == "Vault")
            {
                if (!Client.Account.OwnedSkins.Contains(item.ActivateEffects[0].SkinType))
                {
                    Manager.Database.AddSkin(Client.Account, item.ActivateEffects[0].SkinType);
                    SendInfo("New skin unlocked successfully. Change skins in your Vault, or start a new character to use.");
                    Client.SendMessage(new RESKIN_UNLOCK
                    {
                        SkinID = item.ActivateEffects[0].SkinType
                    });
                    endMethod = false;
                    return;
                }
                SendInfo("Error.alreadyOwnsSkin");
                endMethod = true;
            }
            else
            {
                SendInfo("Skin items can only be used from the Vault!");
                endMethod = true;
            }
        }

        //TODO: Should be implemented again c:
        private void PermaPet()
        {

        }

        private void Pet(ActivateEffect eff)
        {
            Entity en = Resolve(Manager, eff.ObjectId);
            en?.Move(X, Y);
            en?.SetPlayerOwner(this);
            Owner?.EnterWorld(en);
            Owner?.Timers.Add(new WorldTimer(30 * 1000, (w, t) => w.LeaveWorld(en)));
        }

        //TODO: maybe?
        private void CreatePet()
        {

        }

        private void MysteryPortal()
        {
            string[] dungeons = new[]
            {
                "Pirate Cave Portal",
                "Forest Maze Portal",
                "Spider Den Portal",
                "Snake Pit Portal",
                "Glowing Portal",
                "Forbidden Jungle Portal",
                "Candyland Portal",
                "Haunted Cemetery Portal",
                "Undead Lair Portal",
                "Davy Jones' Locker Portal",
                "Manor of the Immortals Portal",
                "Abyss of Demons Portal",
                "Lair of Draconis Portal",
                "Mad Lab Portal",
                "Ocean Trench Portal",
                "Tomb of the Ancients Portal",
                "Beachzone Portal",
                "The Shatters",
                "Deadwater Docks",
                "Woodland Labyrinth",
                "The Crawling Depths",
                "Treasure Cave Portal",
                "Battle Nexus Portal",
                "Belladonna's Garden Portal",
                "Lair of Shaitan Portal"
            };

            PortalDesc[] descs = Manager.GameData.Portals.Where(_ => dungeons.Contains<string>(_.Value.ObjectId)).Select(_ => _.Value).ToArray();
            PortalDesc portalDesc = descs[Random.Next(0, descs.Count())];
            Entity por = Entity.Resolve(Manager, portalDesc.ObjectId);
            por?.Move(this.X, this.Y);
            Owner?.EnterWorld(por);

            Client?.SendMessage(new NOTIFICATION
            {
                Color = new ARGB(0x00FF00),
                Text =
                    "{\"key\":\"blank\",\"tokens\":{\"data\":\"Opened by " +
                    Client.Account.Name + "\"}}",
                ObjectId = Client.Player.Id
            });

            Owner?.BroadcastPacket(new TEXT
            {
                BubbleTime = 0,
                Stars = -1,
                Name = "",
                Text = portalDesc.ObjectId + " opened by " + Name,
                NameColor = 0x123456,
                TextColor = 0x123456
            }, null);

            Owner?.Timers.Add(new WorldTimer(portalDesc.TimeoutTime * 1000, (w, t) => //default portal close time * 1000
            {
                try
                {
                    w?.LeaveWorld(por);
                }
                catch (Exception ex)
                {
                    log.ErrorFormat("Couldn't despawn portal.\n{0}", ex);
                }
            }));
        }

        private void GenericActivate(ActivateEffect eff, Position target)
        {
            bool targetPlayer = eff.Target.Equals("player");
            bool centerPlayer = eff.Center.Equals("player");
            int duration = (eff.UseWisMod) ? (int)(UseWisMod(eff.DurationSec) * 1000) : eff.DurationMS;
            float range = (eff.UseWisMod) ? UseWisMod(eff.Range) : eff.Range;

            Owner?.Aoe((eff.Center.Equals("mouse")) ? target : new Position { X = X, Y = Y }, range, targetPlayer, entity =>
            {
                if (IsSpecial(entity.ObjectType)) return;
                if (!entity.HasConditionEffect(ConditionEffectIndex.Stasis) &&
                    !entity.HasConditionEffect(ConditionEffectIndex.Invincible))
                {
                    entity.ApplyConditionEffect(
                    new ConditionEffect()
                    {
                        Effect = eff.ConditionEffect.Value,
                        DurationMS = duration
                    });
                }
            });

            // replaced this last bit with what I had, never noticed any issue with it. Perhaps I'm wrong?
            BroadcastSync(new SHOWEFFECT()
            {
                EffectType = (EffectType)eff.VisualEffect,
                TargetId = Id,
                Color = new ARGB(eff.Color ?? 0xffffffff),
                PosA = centerPlayer ? new Position { X = range } : target,
                PosB = new Position(target.X - range, target.Y) //Its the range of the diffuse effect
            }, p => this?.DistSqr(p) < 25);
        }

        //TODO: needs implementation
        private void PetSkin()
        {

        }

        //TODO: needs implementation
        private void Exchange()
        {

        }

        //TODO: needs implementation
        private void MysteryDyes()
        {

        }

        //TODO: needs implementation
        private void Unlock()
        {

        }

        #endregion "Activate Code Assit"
    }
}