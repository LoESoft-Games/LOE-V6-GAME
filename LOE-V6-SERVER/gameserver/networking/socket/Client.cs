using core;
using core.config;
using gameserver.realm;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace gameserver.networking
{
    public partial class Client : IDisposable
    {
        private bool disposed;

        public Socket Socket { get; internal set; }
        public RealmManager Manager { get; private set; }
        public RC4 IncomingCipher { get; private set; }
        public RC4 OutgoingCipher { get; private set; }
        private NetworkHandler handler;

        public static string SERVER_VERSION = Settings.NETWORKING.FULL_BUILD;
        public static readonly List<string> INTERNAL_SERVER_BUILD = Settings.NETWORKING.INTERNAL_BUILD;

        public DbChar Character { get; internal set; }
        public DbAccount Account { get; internal set; }

        public wRandom Random { get; internal set; }

        public int Id { get; internal set; }
        public int TargetWorld { get; internal set; }
        public string ConnectedBuild { get; internal set; }

        public byte[] _IncomingCipher => new byte[] { 0x8D, 0x48, 0x0B, 0xE9, 0xC9, 0x29, 0xEB, 0x61 };
        public byte[] _OutgoingCipher => new byte[] { 0x4A, 0xC0, 0x72, 0x09, 0x5F, 0x3B, 0xE3, 0x03 };

        public RC4 ProcessRC4(byte[] cipher) => new RC4(cipher);

        public static class Type
        {
            public const int DEFAULT = 0;
            public const int BAD_KEY = 5;
            public const int INVALID_TELEPORT_TARGET = 6;
            public const int EMAIL_VERIFICATION_NEEDED = 7;
            public const int JSON_DIALOG = 8;
        }

        public Client(RealmManager manager, Socket skt)
        {
            Socket = skt;
            Manager = manager;
            IncomingCipher = ProcessRC4(_IncomingCipher);
            OutgoingCipher = ProcessRC4(_OutgoingCipher);
            BeginProcess();
        }

        public void BeginProcess()
        {
            handler = new NetworkHandler(this, Socket);
            handler.BeginHandling();
        }

        public void Dispose()
        {
            if (disposed)
                return;

            handler?.Dispose();
            handler = null;
            IncomingCipher = null;
            OutgoingCipher = null;
            Manager = null;
            Socket = null;
            Character = null;
            Account = null;
            Player?.Dispose();
            Player = null;
            Random = null;
            ConnectedBuild = null;
            disposed = true;
        }
    }
}