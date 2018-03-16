#region

using core;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using appengine.sfx;
using core.config;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Linq;

#endregion

namespace appengine
{
    internal class Program
    {
        private static readonly List<HttpListenerContext> currentRequests = new List<HttpListenerContext>();
        private static HttpListener listener { get; set; }

        public static ILog Logger { get; } = LogManager.GetLogger("AppEngine");
        public static bool autoRestart { get; private set; }

        internal static Database Database { get; set; }
        internal static EmbeddedData GameData { get; set; }
        internal static string InstanceId { get; set; }

        public static string message { get; private set; }

        private static void Main(string[] args)
        {
            message = null;

            message = "Loading...";

            Console.Title = message;

            XmlConfigurator.ConfigureAndWatch(new FileInfo("_appengine.config"));

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.Name = "Entry";

            using (Database = new Database())
            {
                GameData = new EmbeddedData();

                autoRestart = Settings.NETWORKING.RESTART.ENABLE_RESTART;

                InstanceId = Guid.NewGuid().ToString();

                var port = Settings.IS_PRODUCTION ? Settings.APPENGINE.PRODUCTION_PORT : Settings.APPENGINE.TESTING_PORT;

                while (!PortCheck(port))
                    ForceShutdown(port);

                listener = new HttpListener();
                listener.Prefixes.Add(string.Format("http://{1}:{0}/", port, Settings.IS_PRODUCTION ? "*" : "localhost"));
                listener.Start();

                listener.BeginGetContext(ListenerCallback, null);
                Console.CancelKeyPress += (sender, e) => e.Cancel = true;

                if (autoRestart)
                    restart();

                Console.Title = Settings.APPENGINE.TITLE;

                ISManager manager = new ISManager();
                manager.Run();

                while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;

                while (currentRequests.Count > 0) ;

                manager.Dispose();
                listener?.Stop();
                GameData.Dispose();

                Environment.Exit(0);
            }
        }

        private static bool PortCheck(int port) => IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections().All(_ => _.LocalEndPoint.Port != port) && IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners().All(_ => _.Port != port);

        static int ToMiliseconds(int minutes) => minutes * 60 * 1000;

        public static void restart()
        {
            Thread parallel_thread = new Thread(() =>
            {
                Thread.Sleep(ToMiliseconds(Settings.NETWORKING.RESTART.RESTART_DELAY_MINUTES));
                int i = 5;
                do
                {
                    Logger.Info($"AppEngine is restarting in {i} second{(i <= 1 ? "" : "s")}...");
                    Thread.Sleep(1000);
                    i--;
                } while (i != 0);
                Logger.Warn("AppEngine is now offline.");
                Thread.Sleep(2000);
                Process.Start(Settings.APPENGINE.FILE);
                Environment.Exit(0);
            });

            parallel_thread.Start();
        }

        private static void ListenerCallback(IAsyncResult ar)
        {
            try
            {
                if (!listener.IsListening) return;
                var context = listener.EndGetContext(ar);
                listener.BeginGetContext(ListenerCallback, null);
                ProcessRequest(context);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private static void ProcessRequest(HttpListenerContext context)
        {
            try
            {
                string RequestURL = context.Request.Url.LocalPath;
                string dns = context.Request.RemoteEndPoint.Address.ToString();

                if (context.Request.Url.LocalPath.Contains("crossdomain"))
                {
                    new crossdomain().HandleRequest(context);
                    context.Response.Close();
                    return;
                }

                if (RequestURL.Contains("sfx") || RequestURL.Contains("music"))
                {
                    new Sfx().HandleRequest(context);
                    context.Response.Close();
                    return;
                }

                string s;
                if (RequestURL.IndexOf(".") == -1)
                    s = "appengine" + RequestURL.Replace("/", ".");
                else
                    s = "appengine" + RequestURL.Remove(RequestURL.IndexOf(".")).Replace("/", ".");

                var t = Type.GetType(s);
                if (t != null)
                {
                    var handler = Activator.CreateInstance(t, null, null);
                    if (!(handler is RequestHandler))
                    {
                        if (handler == null)
                            using (var wtr = new StreamWriter(context.Response.OutputStream))
                                wtr.Write($"<Error>Class \"{t.FullName}\" not found.</Error>");
                        else
                            using (var wtr = new StreamWriter(context.Response.OutputStream))
                                wtr.Write($"<Error>Class \"{t.FullName}\" is not of the type RequestHandler.</Error>");
                    }
                    else
                        (handler as RequestHandler).HandleRequest(context);
                    Logger.Info($"[{(dns == "::1" ? "localhost" : $"{dns}")}] Request\t->\t{RequestURL}");
                }
                else
                    Logger.Warn($"[{(dns == "::1" ? "localhost" : $"{dns}")}] Request\t->\t{RequestURL}");
            }
            catch (Exception e)
            {
                currentRequests?.Remove(context);
                Logger.Error(e);
            }

            context?.Response.Close();
        }

        public static async void ForceShutdown(int port)
        {
            Task task = Task.Delay(1000);

            Console.ForegroundColor = ConsoleColor.Yellow;

            int i = 5;

            do
            {
                Console.WriteLine($"[{DateTime.Now.ToString().Split(' ')[1]}] [AppEngine] Port {port} is occupied, restarting in {i} second{(i <= 1 ? "" : "s")}...");
                Thread.Sleep(1000);
                i--;
            } while (i != 0);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("AppEngine is restarting.");
            Console.ResetColor();

            await task;

            task.Dispose();

            Process.Start(Settings.APPENGINE.FILE);

            Environment.Exit(0);
        }
    }
}