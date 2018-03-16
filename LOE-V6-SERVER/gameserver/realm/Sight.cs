#region

using System;
using System.Collections.Generic;
using gameserver.realm.entity.player;

#endregion

namespace gameserver.realm
{
    internal static class Sight
    {
        private static readonly Dictionary<int, IntPoint[]> points = new Dictionary<int, IntPoint[]>();

        public static IntPoint[] GetSightCircle(int radius)
        {
            IntPoint[] ret;
            if (!points.TryGetValue(radius, out ret))
            {
                List<IntPoint> pts = new List<IntPoint>();
                for (int y = -radius; y <= radius; y++)
                    for (int x = -radius; x <= radius; x++)
                    {
                        if (x * x + y * y <= radius * radius)
                            pts.Add(new IntPoint(x, y));
                    }
                ret = points[radius] = pts.ToArray();
            }
            return ret;
        }

        public static IntPoint[] RayCast(Player player, int radius = 15)
        {
            List<IntPoint> RayTiles = new List<IntPoint>();
            int angle = 0;
            while (angle < 360)
            {
                int distance = 0;
                while (distance < radius)
                {
                    int x = (int)(distance * Math.Cos(angle));
                    int y = (int)(distance * Math.Sin(angle));
                    if ((x * x + y * y) <= (radius * radius))
                    {
                        RayTiles.Add(new IntPoint(x, y));
                        ObjectDesc desc;
                        player.Manager.GameData.ObjectDescs.TryGetValue(player.Owner.Map[(int)player.X + x, (int)player.Y + y].ObjType, out desc);
                        if (desc != null && desc.BlocksSight)
                            break;
                        RayTiles.Add(new IntPoint(x, y));
                    }
                    distance++;
                }
                angle++;
            }
            return RayTiles.ToArray();
        }
    }
}