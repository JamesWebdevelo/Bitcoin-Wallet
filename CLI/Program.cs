using BusinessLayer;
using BusinessLayer.Configuration;
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
            //string passwordConf = "admin";

            //string mnemonic = "";

            //do
            //{
            //    /// Display Seed
            //    mnemonic = Wallet.GenerateWallet(password);
            //    Console.WriteLine(mnemonic);
            //}
            //while (password != passwordConf);
            //Console.ReadKey();
            #endregion

            #region Recover Wallet
            //do
            //{
            //    string result = Wallet.RecoverWallet(mnemonic, password);
            //    /// Display Seed
            //    Console.WriteLine(result);
            //}
            //while (password != passwordConf);
            //Console.ReadKey();
            #endregion

            #region Receive
            Receiver.GetPublicAddresses(password);
            Console.ReadKey();
            #endregion
        }
    }
}
