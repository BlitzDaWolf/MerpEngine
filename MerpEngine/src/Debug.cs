﻿using System;
using System.Diagnostics;

namespace MerpEngine
{
    public static class Debug
    {
        public static bool DisplayLogs = true;
        public static bool DisplayInfo = true;

        private static void DisplayMessage(object msg, string type = "Log")
        {
            StackTrace trace = new StackTrace();

            if (!DisplayInfo)
                return;
            ConsoleColor color = Console.ForegroundColor;
#if DEBUG
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[*]");
            Console.ForegroundColor = color;
#endif

            string test = $"{trace.GetFrame(2).GetMethod().DeclaringType.Name}.{trace.GetFrame(2).GetMethod().Name}";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{{{type}}}");
            Console.ForegroundColor = color;
            Console.WriteLine($"{{{test}}}[{DateTime.Now}] {msg}");
        }

        internal static void Info(object v)
        {
            if (DisplayInfo)
            {
                ConsoleColor old = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                DisplayMessage(v, "info");
                Console.ForegroundColor = old;
            }
        }

        public static void Log(object msg, ConsoleColor color = ConsoleColor.Gray)
        {
            if (DisplayLogs)
            {
                ConsoleColor old = Console.ForegroundColor;
                Console.ForegroundColor = color;
                DisplayMessage(msg, "Log");
                Console.ForegroundColor = old;
            }
        }

        public static void Warning(object msg)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            DisplayMessage(msg, "warning");
            Console.ForegroundColor = color;
        }

        public static void Error(object msg)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            DisplayMessage(msg, "Error");
            Console.ForegroundColor = color;
        }
    }
}
