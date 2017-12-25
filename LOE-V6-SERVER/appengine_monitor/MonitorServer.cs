using common.config;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace appengine_monitor
{
    internal class MonitorServer
    {
        public Socket skt { get; private set; }
        public static Task task { get; private set; }
        public Random rnd { get; private set; }

        public MonitorServer() {
            skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            skt.NoDelay = true;
            skt.UseOnlyOverlappedIO = true;
        }

        public async void Start() {
            task = Task.Delay(2000);

            rnd = new Random();

            Log.Write("Starting server...");

            await task;

            task.Dispose();

            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(rnd.Next(5, 25));
                Console.SetCursorPosition(0, 0);
                Log.Write($"Starting server... {(i != 100 ? $"{i}%" : "OK!")}\r");
            }

            try
            {
                skt.Bind(new IPEndPoint(IPAddress.Any, Settings.APPENGINE_MONITOR.PORT));
                skt.Listen(0xFF);
                skt.BeginAccept(Listen, null);
            } catch (ObjectDisposedException e) {
                Log.Write($"Bad status: {e.Message}", ConsoleColor.Red);
                return;
            }

            Init();
        }

        private static void Init() {
            Log.Write($"AppEngine Monitor version {Settings.APPENGINE_MONITOR.BUILD_VERSION} is now online!");

            Console.Title = Settings.APPENGINE_MONITOR.TITLE;

            Log.Write($"Awaiting for AppEngine response...\n", ConsoleColor.Green);

            StartAppEngine();
        }

        private async static void StartAppEngine() {
            task = Task.Delay(1000);

            Log.Write("Initializing AppEngine instance...");

            try {
                Process.Start(Settings.APPENGINE.FILE);

                await task;

                task.Dispose();

                Log.Write("AppEngine has been initialized!");
            } catch (ObjectDisposedException e) {
                Log.Write($"Bad status: {e.Message}", ConsoleColor.Red);
                return;
            }
        }

        private void Listen(IAsyncResult ar) {
            Socket _skt = null;

            try {
                _skt = skt.EndAccept(ar);
            } catch (ObjectDisposedException e) {
                Log.Write($"Bad status: {e.Message}", ConsoleColor.Red);
                return;
            }

            try {
                skt.BeginAccept(Listen, null);
            } catch (ObjectDisposedException e) {
                Log.Write($"Bad status: {e.Message}", ConsoleColor.Red);
                return;
            }

            if (_skt != null)
                HandleMessage(_skt);
        }

        private void HandleMessage(Socket socket) {
            string[] message = null;

            byte[] response = new byte[socket.ReceiveBufferSize];

            Array.Resize(ref response, socket.Receive(response));

            message = Encoding.UTF8.GetString(response).Split('|');

            if (message == null || message[0] == string.Empty || message[0] != Settings.APPENGINE_MONITOR.TOKEN)
                return;
                        
            if (message.Length > 2) {
                if (message[1] == string.Empty || message[2] == string.Empty)
                    return;

                Log.Write(message[1], message[2]);
            } else {
                if (message[1] == string.Empty)
                    return;

                Log.Write(message[1]);
            }

            socket?.Close();
        }

        public void Stop() {
            Log.Write("Stoping server...");
            skt.Close();
        }
    }
}
