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
            string mnemonic = "";

            WalletGenerator generator = new WalletGenerator();

            do
            {
                /// Display Seed
                mnemonic = generator.GenerateWallet(password);
                Console.WriteLine(mnemonic);
            }
            while (password != passwordConf);
            Console.ReadKey();
            #endregion

            #region Recover Wallet
            WalletRecoverer recoverer = new WalletRecoverer();

            do
            {
                string result = recoverer.RecoverWallet(mnemonic, password);
                /// Display Seed
                Console.WriteLine(result);
            }
            while (password != passwordConf);
            Console.ReadKey();
            #endregion


        }
    }
}
