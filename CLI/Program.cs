using BusinessLayer;
using System;
using System.Collections.Generic;

namespace CLI
{
    class Program
    {
        #region Commands
        public static HashSet<string> Commands = new HashSet<string>()
        {
            "help",
            //"generate-wallet",
            //"recover-wallet",
            //"show-balances",
            //"show-history",
            //"receive",
            //"send"
        };
        #endregion
        /// <summary>
        /// There are roughly three way to communicate with the Bitcoin network: as a full node, as an SPV node or through an HTTP API.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Load config file
            // It also creates it with default settings if doesn't exist
            Config.Load();

            var command = args[0];

            #region HelpCommand
            if (command == "help")
            {
                Help.AssertArgumentsLenght(args.Length, 1, 1);
                Help.DisplayHelp(Commands);
            }
            #endregion
        }
    }
}
