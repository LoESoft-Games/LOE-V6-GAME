#region

using System;
using System.Runtime.InteropServices;
using gameserver.realm.terrain;

#endregion

namespace gameserver.realm.mapsetpiece
{
    internal class EyeOfTheDragon : MapSetPiece
    {
        public override int Size => 51;

        internal string mapName = "eyeofthedragon";

        public unsafe override void RenderSetPiece(World world, IntPoint pos)
        {
            GCHandle h = GCHandle.Alloc(world);
            IntPtr ptr = GCHandle.ToIntPtr(h);

            GCHandle mapHandle = GCHandle.Alloc(new Wmap(world.Manager.GameData));

            LoadJson(ptr.ToPointer(), mapName, &pos, GCHandle.ToIntPtr(mapHandle).ToPointer());

            Wmap map = (mapHandle.Target as Wmap);

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    try
                    {
                        if (map[x, y].TileId != 0 && map[x, y].TileId != 255)
                        {
                            var tile = world.Map[x + pos.X, y + pos.Y].Clone();
                            tile.TileId = map[x, y].TileId;
                            tile.ObjType = map[x, y].ObjType;
                            if (tile.ObjType != 0) tile.ObjId = world.GetNextEntityId();
                            world.Map[x + pos.X, y + pos.Y] = tile;
                        }
                    }
                    catch { }
                }
            }
        }
    }
}