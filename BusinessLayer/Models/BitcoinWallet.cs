using HBitcoin.KeyManagement;
using NBitcoin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace BusinessLayer.Models
{
    public class BitcoinWallet
    {
        /// <summary>
        /// Properties
        /// </summary>
        public Key PrivateKey { private set; get; }

        /// <summary>
        /// Method
        /// </summary>
        public string GenerateWallet(string password)
        {

            string[] args = null;

            /// Get Path of Wallet file
            var walletFilePath = GetWalletFilePath(args);
            AssertWalletNotExists(walletFilePath);

            /// Create wallet
            try
            {
                Safe safe = Safe.Create(out Mnemonic mnemonic, password, walletFilePath, Config.Network);
                return mnemonic.ToString();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static string GetWalletFilePath(string[] args)
        {
            string walletFileName = GetArgumentValue(args, "wallet-file", required: false);

            if (walletFileName == "")
            {
                walletFileName = Config.DefaultWalletFileName;
            }

            var walletDirName = "Wallets";
            Directory.CreateDirectory(walletDirName);
            return Path.Combine(walletDirName, walletFileName);
        }

        /// <summary>
        /// Method
        /// </summary>
        /// <param name="walletFilePath"></param>
        public static void AssertWalletNotExists(string walletFilePath)
        {
            if (File.Exists(walletFilePath))
            {
                throw new Exception($"A wallet, named {walletFilePath} already exists.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="argName"></param>
        /// <param name="required"></param>
        /// <returns></returns>
        private static string GetArgumentValue(string[] args, string argName, bool required = true)
        {
            string argValue = "";
            foreach (var arg in args)
            {
                if (arg.StartsWith($"{argName}=", StringComparison.OrdinalIgnoreCase))
                {
                    argValue = arg.Substring(arg.IndexOf("=") + 1);
                    break;
                }
            }
            if (required && argValue == "")
            {
                //Exit($@"'{argName}=' is not specified.");
            }
            return argValue;
        }

        /// <summary>
        /// Method
        /// </summary>
        //public void RecoverWallet()
        //{

        //}
    }
}
