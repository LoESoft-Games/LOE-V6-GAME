#region

using core;
using gameserver.realm.terrain;
using System.Diagnostics;

#endregion

namespace gameserver.realm.mapsetpiece.special
{
    internal class AbyssIdol_LavaBomb : MapSetPiece
    {
        public override int Size => 5;

        static readonly byte[,] SetPiece =
        {
            { 0, 4, 3, 4, 0 },
            { 4, 3, 2, 3, 4 },
            { 3, 2, 1, 2, 3 },
            { 4, 3, 2, 3, 4 },
            { 0, 4, 3, 4, 0 }
        };

        public override void RenderSetPiece(World world, IntPoint pos)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            long time = sw.ElapsedMilliseconds;

            // Declare cooldown for setpiece transitions

            int delay = 2000; // 2 seconds

            // Declare stages

            bool stage1 = true;
            bool stage2 = false;
            bool stage3 = false;
            bool stage4 = false;
            bool stage5 = false;
            bool stage6 = false;
            bool stage7 = false;
            bool stage8 = false;

            bool done = false; // when setpiece is done after all stages

            EmbeddedData dat = world.Manager.GameData;

            IntPoint p = new IntPoint
            {
                X = pos.X - (Size / 2),
                Y = pos.Y - (Size / 2)
            };
            do
            {
                for (int x = 0; x < Size; x++)
                    for (int y = 0; y < Size; y++)
                    {
                        // stage 1
                        if (time >= delay && time < delay * 2 && SetPiece[y, x] == 1 && stage1)
                        {
                            WmapTile tile = world.Map[x + p.X, y + p.Y].Clone();
                            tile.TileId = dat.IdToTileType["Lava"];
                            tile.ObjType = 0;
                            world.Map[x + p.X, y + p.Y] = tile;
                            stage1 = false;
                            stage2 = true;
                        }
                        // stage 2
                        if (time >= delay * 2 && time < delay * 3 && SetPiece[y, x] == 2 && stage2)
                        {
                            WmapTile tile = world.Map[x + p.X, y + p.Y].Clone();
                            tile.TileId = dat.IdToTileType["Lava"];
                            tile.ObjType = 0;
                            world.Map[x + p.X, y + p.Y] = tile;
                            stage2 = false;
                            stage3 = true;
                        }
                        // stage 3
                        if (time >= delay * 3 && time < delay * 4 && SetPiece[y, x] == 3 && stage3)
                        {
                            WmapTile tile = world.Map[x + p.X, y + p.Y].Clone();
                            tile.TileId = dat.IdToTileType["Lava"];
                            tile.ObjType = 0;
                            world.Map[x + p.X, y + p.Y] = tile;
                            stage3 = false;
                            stage4 = true;
                        }
                        // stage 4
                        if (time >= delay * 4 && time < delay * 5 && SetPiece[y, x] == 4 && stage4)
                        {
                            WmapTile tile = world.Map[x + p.X, y + p.Y].Clone();
                            tile.TileId = dat.IdToTileType["Lava"];
                            tile.ObjType = 0;
                            world.Map[x + p.X, y + p.Y] = tile;
                            stage4 = false;
                            stage5 = true;
                        }
                        // stage 5
                        if (time >= delay * 5 && time < delay * 6 && SetPiece[y, x] == 4 && stage5)
                        {
                            WmapTile tile = world.Map[x + p.X, y + p.Y].Clone();
                            tile.TileId = dat.IdToTileType["Red Quad"];
                            tile.ObjType = 0;
                            world.Map[x + p.X, y + p.Y] = tile;
                            stage5 = false;
                            stage6 = true;
                        }
                        // stage 6
                        if (time >= delay * 6 && time < delay * 7 && SetPiece[y, x] == 3 && stage6)
                        {
                            WmapTile tile = world.Map[x + p.X, y + p.Y].Clone();
                            tile.TileId = dat.IdToTileType["Red Quad"];
                            tile.ObjType = 0;
                            world.Map[x + p.X, y + p.Y] = tile;
                            stage6 = false;
                            stage7 = true;
                        }
                        // stage 7
                        if (time >= delay * 7 && time < delay * 8 && SetPiece[y, x] == 2 && stage7)
                        {
                            WmapTile tile = world.Map[x + p.X, y + p.Y].Clone();
                            tile.TileId = dat.IdToTileType["Red Quad"];
                            tile.ObjType = 0;
                            world.Map[x + p.X, y + p.Y] = tile;
                            stage7 = false;
                            stage8 = true;
                        }
                        // stage 8 (final)
                        if (time >= delay * 8 && SetPiece[y, x] == 1 && stage8)
                        {
                            WmapTile tile = world.Map[x + p.X, y + p.Y].Clone();
                            tile.TileId = dat.IdToTileType["Red Quad"];
                            tile.ObjType = 0;
                            world.Map[x + p.X, y + p.Y] = tile;
                            stage8 = false;
                            done = true;
                        }
                    }
            } while (!done);
        }
    }
}