using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class Help
    {

        public HashSet<string> Commands = new HashSet<string>()
        {
            "help",
            "generate-wallet",
            "recover-wallet",
            "show-balances",
            "show-history",
            "receive",
            "send"
        };

        public void DisplayHelp()
        {
            Console.WriteLine("Possible commands are:");
            foreach (var cmd in Commands)
            {
                Console.WriteLine($"\t{cmd}");
            }
            Console.WriteLine();
        }

        public void Exit(string reason = "", ConsoleColor color = ConsoleColor.Red)
        {
            //ForegroundColor = color;
            Console.WriteLine();
            if (reason != "")
            {
                Console.WriteLine(reason);
            }

            Environment.Exit(0);
        }
    }
}
