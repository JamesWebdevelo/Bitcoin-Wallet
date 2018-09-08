using BusinessLayer;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;

namespace CLI
{
    class Program
    {
        /// <summary>
        /// There are roughly three way to communicate with the Bitcoin network: as a full node, as an SPV node or through an HTTP API.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Load config file
            // It also creates it with default settings if it doesn't exist
            Config.Load();

            #region Create Wallet
            //string password = "admin";
            //string passwordConf = "admin";

            //BitcoinWallet myWallet = new BitcoinWallet();
            
            //do
            //{
            //    string result = myWallet.GenerateWallet(password);
            //    Console.WriteLine(result);
            //}
            //while (password != passwordConf);
            #endregion

            #region Recover Wallet

            #endregion

            Console.ReadKey();
        }
    }
}
