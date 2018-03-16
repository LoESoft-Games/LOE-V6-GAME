#region

using System;
using System.Xml.Linq;

#endregion

namespace gameserver.realm.entity
{
    partial class GameObject
    {
        public static bool GetStatic(XElement elem)
        {
            return elem.Element("Static") != null;
        }

        public static int? GetHP(XElement elem)
        {
            XElement n = elem.Element("MaxHitPoints");
            if (n != null)
                return Utils.FromString(n.Value);
            return null;
        }

        private static bool IsInteractive(RealmManager manager, ushort objType)
        {
            ObjectDesc desc;
            if (manager.GameData.ObjectDescs.TryGetValue(objType, out desc))
            {
                if (desc.Class != null)
                    if (desc.Class == "Container" || desc.Class.ContainsIgnoreCase("wall") ||
                        desc.Class == "Merchant" || desc.Class == "Portal") return false;
                return !(desc.Static && !desc.Enemy && !desc.EnemyOccupySquare);
            }
            return false;
        }

        protected bool CheckHP()
        {
            try
            {
                if (Vulnerable && HP < 0)
                {
                    if (ObjectDesc != null && (ObjectDesc.EnemyOccupySquare || ObjectDesc.OccupySquare))
                        if (Owner != null)
                            Owner.Obstacles[(int)(X - 0.5), (int)(Y - 0.5)] = 0;


                    if (ObjectDesc != null && Owner.Map[(int)(X - 0.5), (int)(Y - 0.5)].ObjType == ObjectType)
                    {
                        var tile = Owner.Map[(int)(X - 0.5), (int)(Y - 0.5)].Clone();
                        tile.ObjType = 0;
                        Owner.Map[(int)(X - 0.5), (int)(Y - 0.5)] = tile;
                    }

                    Owner?.LeaveWorld(this);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Crash halted - HP check error:\n{0}", ex);
            }
            return true;
        }
    }
}
