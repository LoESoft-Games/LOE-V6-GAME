#region

using System;
using System.Globalization;
using System.IO;
using System.Threading;
using core;
using log4net;
using log4net.Config;
using gameserver.networking;
using gameserver.realm;
using core.config;
using System.Diagnostics;
using System.Threading.Tasks;
using gameserver.realm.commands.mreyeball;
using static gameserver.networking.Client;

#endregion

namespace gameserver
{
    internal static class Program
    {
        public static DateTime uptime { get; private set; }
        public static readonly ILog Logger = LogManager.GetLogger("Server");

        private static readonly ManualResetEvent Shutdown = new ManualResetEvent(false);

        public static int Usage { get; private set; }
        public static bool autoRestart { get; private set; }

        public static ChatManager chat { get; set; }

        private static RealmManager manager;

        public static DateTime WhiteListTurnOff { get; private set; }

        private static void Main(string[] args)
        {
            Console.Title = "Loading...";

            XmlConfigurator.ConfigureAndWatch(new FileInfo("_gameserver.config"));

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.Name = "Entry";

            using (var db = new Database())
            {
                Usage = -1;

                manager = new RealmManager(db);

                autoRestart = Settings.NETWORKING.RESTART.ENABLE_RESTART;

                manager.Initialize();
                manager.Run();

                Server server = new Server(manager);
                PolicyServer policy = new PolicyServer();

                Console.CancelKeyPress += (sender, e) => e.Cancel = true;

                policy.Start();
                server.Start();

                if (autoRestart)
                {
                    chat = manager.Chat;
                    uptime = DateTime.Now;
                    restart();
                    usage();
                }

                Console.Title = Settings.GAMESERVER.TITLE;

                Logger.Info("Server initialized.");

                Console.CancelKeyPress += delegate
                {
                    Shutdown?.Set();
                };

                while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;

                Logger.Info("Terminating...");
                server?.Stop();
                policy?.Stop();
                manager?.Stop();
                Shutdown?.Dispose();
                Logger.Info("Server terminated.");
                Environment.Exit(0);
            }
        }

        static int ToMiliseconds(int minutes) => minutes * 60 * 1000;

        public static void usage()
        {
            Thread parallel_thread = new Thread(() =>
            {
                do
                {
                    Thread.Sleep(ToMiliseconds(Settings.GAMESERVER.TTL) / 60);
                    Usage = manager.Clients.Keys.Count;
                } while (true);
            });

            parallel_thread.Start();
        }

        public async static void ForceShutdown(Exception ex = null)
        {
            Task task = Task.Delay(1000);

            await task;

            task.Dispose();

            Process.Start(Settings.GAMESERVER.FILE);

            Environment.Exit(0);

            if (ex != null)
                Logger.Error(ex);
        }

        public static void restart()
        {
            Thread parallel_thread = new Thread(() =>
            {
                Thread.Sleep(ToMiliseconds((Settings.NETWORKING.RESTART.RESTART_DELAY_MINUTES <= 5 ? 6 : Settings.NETWORKING.RESTART.RESTART_DELAY_MINUTES) - 5));
                string message = null;
                int i = 5;
                do
                {
                    message = $"Server will be restarted in {i} minute{(i <= 1 ? "" : "s")}.";
                    Logger.Info(message);
                    try
                    {
                        foreach (Client j in manager.Clients.Values)
                            chat.Tell(j?.Player, MrEyeball_Dictionary.BOT_NAME, ("Hey (PLAYER_NAME), prepare to disconnect." + message).Replace("(PLAYER_NAME)", j?.Player.Name));
                    }
                    catch (Exception ex)
                    {
                        ForceShutdown(ex);
                    }
                    Thread.Sleep(ToMiliseconds(1));
                    i--;
                } while (i != 0);
                message = "Server is now offline.";
                Logger.Warn(message);
                try
                {
                    foreach (Client k in manager.Clients.Values)
                        chat.Tell(k?.Player, MrEyeball_Dictionary.BOT_NAME, message);
                }
                catch (Exception ex)
                {
                    ForceShutdown(ex);
                }
                Thread.Sleep(2000);
                try
                {
                    foreach (Client clients in manager.Clients.Values)
                        clients?.Disconnect(DisconnectReason.RESTART);
                }
                catch (Exception ex)
                {
                    ForceShutdown(ex);
                }
                Process.Start(Settings.GAMESERVER.FILE);
                Environment.Exit(0);
            });

            parallel_thread.Start();
        }

        public static void Stop(Task task = null)
        {
            if (task != null)
                Logger.Fatal(task.Exception);

            Shutdown.Set();
        }
    }
}