#region

using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using gameserver.logic;
using gameserver.realm.entity;
using gameserver.realm.entity.player;
using gameserver.logic.transitions;
using gameserver.realm.entity.merchant;
using core.config;

#endregion

namespace gameserver.realm
{
    public class Entity : IProjectileOwner, ICollidable<Entity>, IDisposable
    {
        private const int EFFECT_COUNT = 52;
        protected static readonly ILog Log = LogManager.GetLogger(typeof(Entity));
        private readonly ObjectDesc desc;
        private readonly int[] effects;
        private Position[] posHistory;
        private Projectile[] projectiles;
        public bool BagDropped;
        public TagList Tags;
        public bool IsPet;
        private byte posIdx;
        protected byte ProjectileId;
        private bool stateEntry;
        private State stateEntryCommonRoot;
        private Dictionary<object, object> states;
        private bool tickingEffects;
        private Player playerOwner; //For Drakes

        public Entity(RealmManager manager, ushort objType)
            : this(manager, objType, true, false)
        {
        }

        public Entity(RealmManager manager, ushort objType, bool interactive)
            : this(manager, objType, interactive, false)
        {
        }

        protected Entity(RealmManager manager, ushort objType, bool interactive, bool isPet)
        {
            Manager = manager;
            ObjectType = objType;
            Name = "";
            Usable = false;
            BagDropped = false;
            IsPet = isPet;
            Manager.Behaviors.ResolveBehavior(this);
            Manager.GameData.ObjectDescs.TryGetValue(objType, out desc);
            Size = desc != null ? manager.GameData.ObjectDescs[objType].MaxSize : 100;

            if (interactive)
            {
                posHistory = new Position[256];
                projectiles = new Projectile[256];
                effects = new int[EFFECT_COUNT];
            }
            if (objType == 0x072f)
                Usable = true;

            if (ObjectDesc != null)
                Tags = ObjectDesc.Tags;

            if (objType == 0x0d60) ApplyConditionEffect(new ConditionEffect
            {
                Effect = ConditionEffectIndex.Invincible,
                DurationMS = -1
            });
        }

        public RealmManager Manager { get; private set; }


        public ObjectDesc ObjectDesc => desc;

        public World Owner { get; internal set; }

        public World WorldInstance { get; protected set; }

        public int UpdateCount { get; set; }

        public ushort ObjectType { get; private set; }
        public int Id { get; internal set; }

        public bool Usable { get; set; }


        //Stats
        public string Name { get; set; }
        public int Size { get; set; }

        private ConditionEffects _conditionEffects;
        private Int32 _conditionEffects1;
        private Int32 _conditionEffects2;
        public ConditionEffects ConditionEffects
        {
            get { return _conditionEffects; }
            set
            {
                _conditionEffects = value;
                _conditionEffects1 = (int)value;
                _conditionEffects2 = (int)((ulong)value >> 31);
            }
        }

        public IDictionary<object, object> StateStorage => states ?? (states = new Dictionary<object, object>());

        public State CurrentState { get; private set; }

        public float X { get; private set; }
        public float Y { get; private set; }

        public float lastX { get; private set; }
        public float lastY { get; private set; }

        public CollisionNode<Entity> CollisionNode { get; set; }
        public CollisionMap<Entity> Parent { get; set; }

        Entity IProjectileOwner.Self => this;

        Projectile[] IProjectileOwner.Projectiles => projectiles;

        public void SwitchTo(State state)
        {
            var origState = CurrentState;

            CurrentState = state;
            GoDeeeeeeeep();

            stateEntryCommonRoot = State.CommonParent(origState, CurrentState);
            stateEntry = true;
        }

        private void GoDeeeeeeeep()
        {
            //always the first deepest sub-state
            if (CurrentState == null) return;
            while (CurrentState.States.Count > 0)
                CurrentState = CurrentState = CurrentState.States[0];
        }

        public void OnChatTextReceived(string text)
        {
            var state = CurrentState;
            if (state != null)
                foreach (var t in state.Transitions.OfType<ChatTransition>())
                    t.OnChatReceived(text);
        }

        private void TickState(RealmTime time)
        {
            State s;
            if (stateEntry)
            {
                //State entry
                s = CurrentState;
                while (s != null && s != stateEntryCommonRoot)
                {
                    foreach (var i in s.Behaviors)
                        i.OnStateEntry(this, time);
                    s = s.Parent;
                }
                stateEntryCommonRoot = null;
                stateEntry = false;
            }

            var origState = CurrentState;
            var state = CurrentState;
            var transited = false;
            while (state != null)
            {
                if (!transited)
                    if (state.Transitions.Any(i => i.Tick(this, time)))
                        transited = true;

                foreach (var i in state.Behaviors.TakeWhile(i => Owner != null))
                    i.Tick(this, time);
                if (Owner == null) break;

                state = state.Parent;
            }
            if (!transited) return;
            //State exit
            s = origState;
            while (s != null && s != stateEntryCommonRoot)
            {
                foreach (var i in s.Behaviors)
                    i.OnStateExit(this, time);
                s = s.Parent;
            }
        }

        public virtual void Move(float x, float y)
        {
            if (Owner != null && !(this is Projectile) && (!(this is GameObject) || (this as GameObject).Hittestable))
                ((this is Enemy || this is GameObject && !(this is Decoy)) ? Owner.EnemiesCollision : Owner.PlayersCollision)
                    .Move(this, x, y);
            X = x;
            Y = y;
        }

        class FPoint
        {
            public float X;
            public float Y;
        }

        public void ValidateAndMove(float x, float y)
        {
            if (Owner == null)
                return;

            var pos = new FPoint();
            ResolveNewLocation(x, y, pos);
            Move(pos.X, pos.Y);
        }

        public float GetSpeed(float speed, RealmTime time) => (float)(speed * (HasConditionEffect(ConditionEffects.Slowed) ? 0.5f : 1) * Settings.GAMESERVER.TICKETS_PER_SECOND * (time.ElapsedMsDelta / 1000.0));
        public float GetSpeed2(float speed, RealmTime time) => (speed * (HasConditionEffect(ConditionEffects.Slowed) ? 0.5f : 1) * Settings.GAMESERVER.TICKETS_PER_SECOND);

        private void ResolveNewLocation(float x, float y, FPoint pos)
        {
            if (HasConditionEffect(ConditionEffects.Paralyzed) ||
                HasConditionEffect(ConditionEffects.Petrify))
            {
                pos.X = X;
                pos.Y = Y;
                return;
            }

            var dx = x - X;
            var dy = y - Y;

            const float colSkipBoundary = .4f;
            if (dx < colSkipBoundary &&
                dx > -colSkipBoundary &&
                dy < colSkipBoundary &&
                dy > -colSkipBoundary)
            {
                CalcNewLocation(x, y, pos);
                return;
            }

            var ds = colSkipBoundary / Math.Max(Math.Abs(dx), Math.Abs(dy));
            var tds = 0f;

            pos.X = X;
            pos.Y = Y;

            var done = false;
            while (!done)
            {
                if (tds + ds >= 1)
                {
                    ds = 1 - tds;
                    done = true;
                }

                CalcNewLocation(pos.X + dx * ds, pos.Y + dy * ds, pos);
                tds = tds + ds;
            }
        }

        private void CalcNewLocation(float x, float y, FPoint pos)
        {
            float fx = 0;
            float fy = 0;

            var isFarX = (X % .5f == 0 && x != X) || (int)(X / .5f) != (int)(x / .5f);
            var isFarY = (Y % .5f == 0 && y != Y) || (int)(Y / .5f) != (int)(y / .5f);

            if ((!isFarX && !isFarY) || RegionUnblocked(x, y))
            {
                pos.X = x;
                pos.Y = y;
                return;
            }

            if (isFarX)
            {
                fx = (x > X) ? (int)(x * 2) / 2f : (int)(X * 2) / 2f;
                if ((int)fx > (int)X)
                    fx = fx - 0.01f;
            }

            if (isFarY)
            {
                fy = (y > Y) ? (int)(y * 2) / 2f : (int)(Y * 2) / 2f;
                if ((int)fy > (int)Y)
                    fy = fy - 0.01f;
            }

            if (!isFarX)
            {
                pos.X = x;
                pos.Y = fy;
                return;
            }

            if (!isFarY)
            {
                pos.X = fx;
                pos.Y = y;
                return;
            }

            var ax = (x > X) ? x - fx : fx - x;
            var ay = (y > Y) ? y - fy : fy - y;
            if (ax > ay)
            {
                if (RegionUnblocked(x, fy))
                {
                    pos.X = x;
                    pos.Y = fy;
                    return;
                }

                if (RegionUnblocked(fx, y))
                {
                    pos.X = fx;
                    pos.Y = y;
                    return;
                }
            }
            else
            {
                if (RegionUnblocked(fx, y))
                {
                    pos.X = fx;
                    pos.Y = y;
                    return;
                }

                if (RegionUnblocked(x, fy))
                {
                    pos.X = x;
                    pos.Y = fy;
                    return;
                }
            }

            pos.X = fx;
            pos.Y = fy;
        }

        private bool RegionUnblocked(float x, float y)
        {
            if (TileOccupied(x, y))
                return false;

            var xFrac = x - (int)x;
            var yFrac = y - (int)y;

            if (xFrac < 0.5)
            {
                if (TileFullOccupied(x - 1, y))
                    return false;

                if (yFrac < 0.5)
                {
                    if (TileFullOccupied(x, y - 1) || TileFullOccupied(x - 1, y - 1))
                        return false;
                }
                else
                {
                    if (yFrac > 0.5)
                        if (TileFullOccupied(x, y + 1) || TileFullOccupied(x - 1, y + 1))
                            return false;
                }

                return true;
            }

            if (xFrac > 0.5)
            {
                if (TileFullOccupied(x + 1, y))
                    return false;

                if (yFrac < 0.5)
                {
                    if (TileFullOccupied(x, y - 1) || TileFullOccupied(x + 1, y - 1))
                        return false;
                }
                else
                {
                    if (yFrac > 0.5)
                        if (TileFullOccupied(x, y + 1) || TileFullOccupied(x + 1, y + 1))
                            return false;
                }

                return true;
            }

            if (yFrac < 0.5)
            {
                if (TileFullOccupied(x, y - 1))
                    return false;

                return true;
            }

            if (yFrac > 0.5)
                if (TileFullOccupied(x, y + 1))
                    return false;

            return true;
        }

        public bool TileOccupied(float x, float y)
        {
            var x_ = (int)x;
            var y_ = (int)y;

            var map = Owner.Map;

            if (!map.Contains(x_, y_))
                return true;

            var tile = map[x_, y_];

            var tileDesc = Manager.GameData.Tiles[tile.TileId];
            if (tileDesc?.NoWalk == true)
                return true;

            if (tile.ObjType != 0)
            {
                var objDesc = Manager.GameData.ObjectDescs[tile.ObjType];
                if (objDesc?.EnemyOccupySquare == true)
                    return true;
            }

            return false;
        }

        public bool TileFullOccupied(float x, float y)
        {
            var xx = (int)x;
            var yy = (int)y;

            if (!Owner.Map.Contains(xx, yy))
                return true;

            var tile = Owner.Map[xx, yy];

            if (tile.ObjType != 0)
            {
                var objDesc = Manager.GameData.ObjectDescs[tile.ObjType];
                if (objDesc?.FullOccupy == true)
                    return true;
            }
            return false;
        }

        protected virtual void ExportStats(IDictionary<StatsType, object> stats)
        {
            stats[StatsType.Name] = Name ?? ""; //Name was null for some reason O.o
            stats[StatsType.Size] = Size;
            stats[StatsType.Effects] = _conditionEffects1;
            stats[StatsType.Effects2] = _conditionEffects2;
        }

        public virtual ObjectStats ExportStats()
        {
            var stats = new Dictionary<StatsType, object>();
            ExportStats(stats);
            return new ObjectStats
            {
                Id = Id,
                Position = new Position { X = X, Y = Y },
                Stats = stats.ToArray()
            };
        }

        public virtual ObjectDef ToDefinition()
        {
            return new ObjectDef
            {
                ObjectType = ObjectType,
                Stats = ExportStats()
            };
        }

        public Player GetPlayerOwner()
        {
            return playerOwner;
        }

        public void SetPlayerOwner(Player target)
        {
            playerOwner = target;
        }

        public virtual void Init(World owner)
        {
            Owner = owner;
            WorldInstance = owner;

            if (ObjectType == 0x0754)
            {
                var en = new GameObject(Manager, 0x1942, null, true, false, true);
                en.Move(X, Y);
                owner.EnterWorld(en);
            }
        }

        public virtual void Tick(RealmTime time)
        {
            if (this is Projectile || Owner == null) return;
            if (playerOwner != null)
            {
                if (this.Dist(playerOwner) > 20) Move(playerOwner.X, playerOwner.Y);
            }
            if (CurrentState != null && Owner != null)
            {
                if (!HasConditionEffect(ConditionEffectIndex.Stasis))
                    TickState(time);
            }
            if (posHistory != null)
                posHistory[posIdx++] = new Position { X = X, Y = Y };
            if (effects == null) return;
            ProcessConditionEffects(time);
        }

        public Position? TryGetHistory(long timeAgo)
        {
            if (posHistory == null) return null;
            var tickPast = timeAgo * Manager.TPS / 1000;
            if (tickPast > 255) return null;
            return posHistory[(byte)(posIdx - 2)];
        }

        public static Entity Resolve(RealmManager manager, string name)
        {
            ushort id;
            if (!manager.GameData.IdToObjectType.TryGetValue(name, out id))
                return null;

            return Resolve(manager, id);
        }

        public static Entity Resolve(RealmManager manager, ushort id)
        {
            var node = manager.GameData.ObjectTypeToElement[id];
            var cls = node.Element("Class");
            if (cls == null) throw new ArgumentException("Invalid XML Element, field class is missing");
            var type = cls.Value;

            switch (type)
            {
                case "Projectile":
                    throw new Exception("Projectile should not instantiated using Entity.Resolve");
                case "Sign":
                    return new Sign(manager, id);
                case "Wall":
                case "DoubleWall":
                    return new Wall(manager, id, node);
                case "ConnectedWall":
                case "CaveWall":
                    return new ConnectedObject(manager, id);
                case "GameObject":
                case "CharacterChanger":
                case "MoneyChanger":
                case "NameChanger":
                    return new GameObject(manager, id, GameObject.GetHP(node), GameObject.GetStatic(node), false, true);
                case "GuildRegister":
                case "GuildChronicle":
                case "GuildBoard":
                    return new GameObject(manager, id, null, false, false, false);
                case "Container":
                    return new Container(manager, node);
                case "Player":
                    throw new Exception("Player should not instantiated using Entity.Resolve");
                case "Character": //Other characters means enemy
                    return new Enemy(manager, id);
                case "Portal":
                case "GuildHallPortal":
                    return new Portal(manager, id, null);
                case "ClosedVaultChest":
                case "ClosedVaultChestGold":
                case "ClosedGiftChest":
                case "VaultChest":
                case "Merchant":
                    return new Merchant(manager, id);
                case "GuildMerchant":
                    return new GuildMerchant(manager, id);
                case "ArenaGuard":
                case "ArenaPortal":
                case "MysteryBoxGround":
                case "ReskinVendor":
                case "PetUpgrader":
                case "FortuneTeller":
                case "YardUpgrader":
                case "FortuneGround":
                case "QuestRewards":
                    return new GameObject(manager, id, null, true, false, false);
                case "Pet":
                    throw new Exception("Pets should not instantiated using Entity.Resolve");
                default:
                    Log.Warn("Not supported type: " + type);
                    return new Entity(manager, id);
            }
        }

        public Projectile CreateProjectile(ProjectileDesc desc, ushort container, int dmg, long time, Position pos,
            float angle)
        {
            var ret = new Projectile(Manager, desc) //Assume only one
            {
                ProjectileOwner = this,
                ProjectileId = ProjectileId++,
                Container = (short)container,
                Damage = dmg,
                BeginTime = time,
                BeginPos = pos,
                Angle = angle,
                X = pos.X,
                Y = pos.Y
            };
            if (projectiles[ret.ProjectileId] != null)
                projectiles[ret.ProjectileId].Destroy();
            projectiles[ret.ProjectileId] = ret;
            return ret;
        }

        public virtual bool HitByProjectile(Projectile projectile, RealmTime time)
        {
            if (ObjectDesc == null)
                return true;
            return ObjectDesc.Enemy || ObjectDesc.Player;
        }

        public virtual void ProjectileHit(Projectile projectile, Entity target) { }

        public bool IsOneHit(int dmg, int hpBeforeHit)
        {
            return ObjectDesc.MaxHP == hpBeforeHit && ObjectDesc.MaxHP <= dmg;
        }

        void ProcessConditionEffects(RealmTime time)
        {
            if (effects == null || !tickingEffects) return;

            ConditionEffects newEffects = 0;
            tickingEffects = false;
            for (int i = 0; i < effects.Length; i++)
                if (effects[i] > 0)
                {
                    effects[i] -= time.ElapsedMsDelta;
                    if (effects[i] > 0)
                        newEffects |= (ConditionEffects)((ulong)1 << i);
                    else
                        effects[i] = 0;
                    tickingEffects = true;
                }
                else if (effects[i] != 0)
                    newEffects |= (ConditionEffects)((ulong)1 << i);
            if (newEffects != ConditionEffects)
            {
                ConditionEffects = newEffects;
                UpdateCount++;
            }
        }

        public bool HasConditionEffect(ConditionEffects eff)
        {
            return (ConditionEffects & eff) != 0;
        }

        public bool HasConditionEffect(ConditionEffectIndex eff)
        {
            return (ConditionEffects & (ConditionEffects)((ulong)1 << (int)eff)) != 0;
        }

        public void ApplyConditionEffect(ConditionEffectIndex effect, int durationMs = -1)
        {
            if (!ApplyCondition(effect))
                return;

            var eff = (int)effect;

            effects[eff] = durationMs;
            if (durationMs != 0)
                ConditionEffects |= (ConditionEffects)((ulong)1 << eff);

            tickingEffects = true;
            UpdateCount++;
        }

        public void ApplyConditionEffect(params ConditionEffect[] effs)
        {
            foreach (var eff in effs)
                ApplyConditionEffect(eff.Effect, eff.DurationMS);
        }

        private bool ApplyCondition(ConditionEffectIndex effect)
        {
            if (effect == ConditionEffectIndex.Stunned &&
                HasConditionEffect(ConditionEffects.StunImmume))
                return false;

            if (effect == ConditionEffectIndex.Stasis &&
                HasConditionEffect(ConditionEffects.StasisImmune))
                return false;

            if (effect == ConditionEffectIndex.Paralyzed &&
                HasConditionEffect(ConditionEffects.ParalyzeImmune))
                return false;

            if (effect == ConditionEffectIndex.ArmorBroken &&
                HasConditionEffect(ConditionEffects.ArmorBreakImmune))
                return false;

            if (effect == ConditionEffectIndex.Curse &&
                HasConditionEffect(ConditionEffects.CurseImmune))
                return false;

            if (effect == ConditionEffectIndex.Petrify &&
                HasConditionEffect(ConditionEffects.PetrifyImmune))
                return false;

            if (effect == ConditionEffectIndex.Dazed &&
                HasConditionEffect(ConditionEffects.DazedImmune))
                return false;

            return effect != ConditionEffectIndex.Slowed || !HasConditionEffect(ConditionEffects.SlowedImmune);
        }

        public virtual void Dispose()
        {
            Manager = null;
            Owner = null;
            WorldInstance = null;
            Name = null;
            states = null;
            CurrentState = null;
            CollisionNode = null;
            Parent = null;
            projectiles = null;
            posHistory = null;
        }
    }
}