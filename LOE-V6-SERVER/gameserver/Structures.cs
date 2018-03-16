#region

using System;
using System.Collections.Generic;
using gameserver.realm;
using core;

#endregion

namespace gameserver
{
    public struct BitmapData
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public byte[] Bytes { get; set; }

        public static BitmapData Read(NReader rdr)
        {
            BitmapData ret = new BitmapData();
            ret.Width = rdr.ReadInt32();
            ret.Height = rdr.ReadInt32();
            ret.Bytes = new byte[ret.Width * ret.Height * 4];
            ret.Bytes = rdr.ReadBytes(ret.Bytes.Length);
            return ret;
        }

        public void Write(NWriter wtr)
        {
            wtr.Write(Width);
            wtr.Write(Height);
            wtr.Write(Bytes);
        }
    }

    public struct IntPointComparer : IEqualityComparer<IntPoint>
    {
        public bool Equals(IntPoint x, IntPoint y)
        {
            return x.X == y.X && x.Y == y.Y;
        }

        public int GetHashCode(IntPoint obj)
        {
            return obj.X * 23 << 16 + obj.Y * 17;
        }
    }

    public struct IntPoint
    {
        public int X;
        public int Y;

        public IntPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public struct TradeItem
    {
        public bool Included;
        public int Item;
        public int SlotType;
        public bool Tradeable;

        public static TradeItem Read(NReader rdr)
        {
            TradeItem ret = new TradeItem();
            ret.Item = rdr.ReadInt32();
            ret.SlotType = rdr.ReadInt32();
            ret.Tradeable = rdr.ReadBoolean();
            ret.Included = rdr.ReadBoolean();
            return ret;
        }

        public void Write(NWriter wtr)
        {
            wtr.Write(Item);
            wtr.Write(SlotType);
            wtr.Write(Tradeable);
            wtr.Write(Included);
        }
    }

    public enum EffectType
    {
        Unknown = 0,
        Heal = 1,
        Teleport = 2,
        Stream = 3,
        Throw = 4,
        Nova = 5, //radius=pos1.x
        Poison = 6,
        Line = 7,
        Burst = 8, //radius=dist(pos1,pos2)
        Flow = 9,
        Ring = 10, //radius=pos1.x
        Lightning = 11, //particleSize=pos2.x
        Collapse = 12, //radius=dist(pos1,pos2)
        Coneblast = 13, //origin=pos1, radius = pos2.x
        Jitter = 14,
        Flash = 15, //period=pos1.x, numCycles=pos1.y
        ThrowProjectile = 16,
        Shocker = 17, //If a pet paralyzes a monster
        Shockee = 18, //If a monster got paralyzed from a electric pet
        RisingFury = 19 //If a pet is standing still (this white particles)
    }

    public struct ARGB
    {
        public byte A;
        public byte B;
        public byte G;
        public byte R;

        public ARGB(uint argb)
        {
            A = (byte)((argb & 0xff000000) >> 24);
            R = (byte)((argb & 0x00ff0000) >> 16);
            G = (byte)((argb & 0x0000ff00) >> 8);
            B = (byte)((argb & 0x000000ff) >> 0);
        }

        public static ARGB Read(NReader rdr)
        {
            ARGB ret = new ARGB();
            ret.A = rdr.ReadByte();
            ret.R = rdr.ReadByte();
            ret.G = rdr.ReadByte();
            ret.B = rdr.ReadByte();
            return ret;
        }

        public void Write(NWriter wtr)
        {
            wtr.Write(A);
            wtr.Write(R);
            wtr.Write(G);
            wtr.Write(B);
        }
    }

    public struct ObjectSlot
    {
        public int ObjectId;
        public ushort ObjectType;
        public byte SlotId;

        public static ObjectSlot Read(NReader rdr)
        {
            ObjectSlot ret = new ObjectSlot();
            ret.ObjectId = rdr.ReadInt32();
            ret.SlotId = rdr.ReadByte();
            ret.ObjectType = (ushort)rdr.ReadInt16();
            return ret;
        }

        public void Write(NWriter wtr)
        {
            wtr.Write(ObjectId);
            wtr.Write(SlotId);
            wtr.Write(ObjectType);
        }

        public override string ToString()
        {
            return string.Format("{{ObjectId: {0}, SlotId: {1}, ObjectType: {2}}}", ObjectId, SlotId, ObjectType);
        }
    }

    public struct TimedPosition
    {
        public Position Position;
        public int Time;

        public static TimedPosition Read(NReader rdr)
        {
            TimedPosition ret = new TimedPosition();
            ret.Time = rdr.ReadInt32();
            ret.Position = Position.Read(rdr);
            return ret;
        }

        public void Write(NWriter wtr)
        {
            wtr.Write(Time);
            Position.Write(wtr);
        }

        public override string ToString()
        {
            return string.Format("{{Time: {0}, Position: {1}}}", Time, Position);
        }
    }

    public struct Position
    {
        public float X;
        public float Y;

        public Position(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Position Read(NReader rdr)
        {
            Position ret = new Position();
            ret.X = rdr.ReadSingle();
            ret.Y = rdr.ReadSingle();
            return ret;
        }

        public void Write(NWriter wtr)
        {
            wtr.Write(X);
            wtr.Write(Y);
        }

        public override string ToString()
        {
            return string.Format("{{X: {0}, Y: {1}}}", X, Y);
        }
    }

    public struct ObjectDef
    {
        public ushort ObjectType;
        public ObjectStats Stats;

        public static ObjectDef Read(NReader rdr)
        {
            ObjectDef ret = new ObjectDef();
            ret.ObjectType = (ushort)rdr.ReadInt16();
            ret.Stats = ObjectStats.Read(rdr);
            return ret;
        }

        public void Write(NWriter wtr)
        {
            wtr.Write(ObjectType);
            Stats.Write(wtr);
        }
    }

    public struct ObjectStats
    {
        public int Id;
        public Position Position;
        public KeyValuePair<StatsType, object>[] Stats;

        public static ObjectStats Read(NReader rdr)
        {
            ObjectStats ret = new ObjectStats();
            ret.Id = rdr.ReadInt32();
            ret.Position = Position.Read(rdr);
            ret.Stats = new KeyValuePair<StatsType, object>[rdr.ReadInt16()];
            for (int i = 0; i < ret.Stats.Length; i++)
            {
                StatsType type = rdr.ReadByte();
                if (type == StatsType.Guild || type == StatsType.Name)
                    ret.Stats[i] = new KeyValuePair<StatsType, object>(type, rdr.ReadUTF());
                else
                    ret.Stats[i] = new KeyValuePair<StatsType, object>(type, rdr.ReadInt32());
            }
            return ret;
        }

        public void Write(NWriter wtr)
        {
            try
            {
                wtr.Write(Id);
                Position.Write(wtr);
                wtr.Write((ushort)Stats.Length);
                foreach (KeyValuePair<StatsType, object> i in Stats)
                {
                    wtr.Write(i.Key);
                    if (i.Key.IsUTF() && i.Value != null) wtr.WriteUTF(i.Value.ToString());
                    else wtr.Write((int)i.Value);
                }
            }
            catch (Exception) { }
        }
    }
}