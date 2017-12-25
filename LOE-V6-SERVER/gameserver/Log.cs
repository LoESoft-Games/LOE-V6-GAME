using System;

namespace gameserver
{
    public class Log
    {
        public static string[] time => DateTime.Now.ToString().Split(' ');

        public static void Write(string message, ConsoleColor color = ConsoleColor.White)
        {
            string response = $"[{time[1]}] [GameServer] {message}";
            Console.ForegroundColor = color;
            Console.WriteLine(response);
            Console.ResetColor();
        }

        public static void Write(string type, string message, ConsoleColor color = ConsoleColor.Yellow)
        {
            string response = $"[{time[1]}] [GameServer] {type}\t->\t{message}";
            Console.ForegroundColor = color;
            Console.WriteLine(response);
            Console.ResetColor();
        }
    }
}