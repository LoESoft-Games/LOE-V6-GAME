using common.config;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace appengine_monitor
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Loading...";

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.Name = "Entry";
            
            MonitorServer server = new MonitorServer();

            server.Start();

            while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            Log.Write("Terminating...");

            server.Stop();

            Log.Write("Terminated!");

            Environment.Exit(0);
        }
    }
}