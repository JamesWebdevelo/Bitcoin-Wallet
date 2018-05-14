using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public static class Help
    {
        public static void AssertArgumentsLenght(int length, int min, int max)
        {
            if (length < min)
            {
                Exit($"Not enough arguments are specified, minimum: {min}");
            }
            if (length > max)
            {
                Exit($"Too many arguments are specified, maximum: {max}");
            }
        }

        public static void DisplayHelp(HashSet<string> Commands)
        {
            Console.WriteLine("Possible commands are:");
            foreach (var cmd in Commands)
            {
                Console.WriteLine($"\t{cmd}");
            }
            Console.WriteLine();
        }

        public static void Exit(string reason = "", ConsoleColor color = ConsoleColor.Red)
        {
            //ForegroundColor = color;
            Console.WriteLine();
            if (reason != "")
            {
                Console.WriteLine(reason);
            }
            Console.WriteLine("Press any key to exit...");
            //ResetColor();
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
