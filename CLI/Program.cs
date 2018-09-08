using BusinessLayer;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;

namespace CLI
{
    class Program
    {
        /// <summary>
        /// There are roughly three ways to communicate with the Bitcoin network: as a full node, as an SPV node or through an HTTP API.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Load config file
            // It also creates it with default settings if it doesn't exist
            Config.Load();

            #region Generate a Wallet
            string password = "admin";
            string passwordConf = "admin";

            WalletGenerator myWallet = new WalletGenerator();

            do
            {
                string result = myWallet.GenerateWallet(password);
                Console.WriteLine(result);
            }
            while (password != passwordConf);
            #endregion

            #region Recover Wallet

            #endregion

            Console.ReadKey();
        }
    }
}
