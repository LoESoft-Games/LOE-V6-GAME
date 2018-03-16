#region

using System;
using System.IO;
using System.Runtime.InteropServices;
using gameserver.realm.terrain;
using System.Collections.Generic;

#endregion

namespace gameserver.realm.mapsetpiece
{
    public abstract class MapSetPiece
    {
        public abstract int Size { get; }
        public abstract void RenderSetPiece(World world, IntPoint pos);

        public unsafe void LoadJson(void* world, string embeddedResource, IntPoint* pos, void* wmap)
        {
            if (embeddedResource == null) return;
            string resource = embeddedResource.Replace(".jm", "");
            Stream stream = typeof(RealmManager).Assembly.GetManifestResourceStream("gameserver.realm.mapsetpiece.maps." + resource + ".jm");
            if (stream == null) throw new ArgumentException("JSON map resource " + nameof(resource) + " not found!");
            FromWorldMap(new MemoryStream(Json2Wmap.Convert((GCHandle.FromIntPtr(new IntPtr(world)).Target as World).Manager.GameData, new StreamReader(stream).ReadToEnd())), world, pos, wmap);
        }

        private unsafe void FromWorldMap(Stream dat, void* world, IntPoint* pos, void* wmap)
        {
            Wmap map = (GCHandle.FromIntPtr(new IntPtr(wmap)).Target as Wmap);
            map.Load(dat, 0);
            int w = map.Width, h = map.Height;

            pos->X = ((GCHandle.FromIntPtr(new IntPtr(world)).Target as World).Map.Width / 2) - (w / 2);
            pos->Y = ((GCHandle.FromIntPtr(new IntPtr(world)).Target as World).Map.Width / 2) - (w / 2);

            IEnumerable<Entity> ens = map.InstantiateEntities((GCHandle.FromIntPtr(new IntPtr(world)).Target as World).Manager);

            foreach (Entity i in ens)
            {
                i.Move(i.X + pos->X, i.Y + pos->Y);
                (GCHandle.FromIntPtr(new IntPtr(world)).Target as World).EnterWorld(i);
            }
        }
    }
}