#region

using System;
using System.Collections.Generic;
using System.IO;
using core;
using Ionic.Zlib;
using Newtonsoft.Json;

#endregion

namespace gameserver.realm.terrain
{
    public class Json2Wmap
    {
        private struct obj
        {
            public string name;
            public string id;
        }
        private struct loc
        {
            public string ground;
            public obj[] objs;
            public obj[] regions;
        }
        private struct json_dat
        {
            public byte[] data;
            public int width;
            public int height;
            public loc[] dict;
        }

        public static void Convert(EmbeddedData data, string from, string to)
        {
            byte[] x = Convert(data, File.ReadAllText(from));
            File.WriteAllBytes(to, x);
        }
        public static byte[] Convert(EmbeddedData data, string json)
        {
            json_dat obj = JsonConvert.DeserializeObject<json_dat>(json);
            byte[] dat = ZlibStream.UncompressBuffer(obj.data);

            Dictionary<short, TerrainTile> tileDict = new Dictionary<short, TerrainTile>();
            for (int i = 0; i < obj.dict.Length; i++)
            {
                loc o = obj.dict[i];
                tileDict[(short)i] = new TerrainTile
                {
                    TileId = o.ground == null ? (ushort)0xff : data.IdToTileType[o.ground],
                    TileObj = o.objs == null ? null : o.objs[0].id,
                    Name = o.objs == null ? "" : o.objs[0].name ?? "",
                    Terrain = TerrainType.None,
                    Region = o.regions == null ? TileRegion.None : (TileRegion)Enum.Parse(typeof(TileRegion), o.regions[0].id.Replace(' ', '_'))
                };
            }

            TerrainTile[,] tiles = new TerrainTile[obj.width, obj.height];
            using (NReader rdr = new NReader(new MemoryStream(dat)))
                for (int y = 0; y < obj.height; y++)
                    for (int x = 0; x < obj.width; x++)
                    {
                        tiles[x, y] = tileDict[rdr.ReadInt16()];
                    }
            return WorldMapExporter.Export(tiles);
        }
    }
}