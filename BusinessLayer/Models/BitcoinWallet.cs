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

        #region Method

        /// <summary>
        /// Method
        /// </summary>
        public string GenerateWallet(string password)
        {

            /// Get Path of Wallet file
            var walletFilePath = GetWalletFilePath();
            string response = AssertWalletNotExists(walletFilePath);

            /// Create wallet if not already exists
            if (response is null)
            {
                try
                {
                    /// Use Safe class to properly manage our private keys
                    Safe safe = Safe.Create(out Mnemonic mnemonic, password, walletFilePath, Config.Network);
                    return mnemonic.ToString();
                }
                catch (Exception e)
                {
                    return e.Message.ToString();
                }
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static string GetWalletFilePath()
        {
            /// Keep default name for the time being
            string walletFileName = Config.DefaultWalletFileName;

            /// Create Directory and the "Wallets" Folder
            var walletDirName = "Wallets";
            Directory.CreateDirectory(walletDirName);
            return Path.Combine(walletDirName, walletFileName);
        }

        /// <summary>
        /// Check if wallet file already exists
        /// </summary>
        /// <param name="walletFilePath"></param>
        public static string AssertWalletNotExists(string walletFilePath)
        {
            if (File.Exists(walletFilePath))
            {
                return ($"A wallet, named {walletFilePath} already exists.");
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get the specified wallet name if one was defined. Else set the name to a default value;
        /// </summary>
        /// <param name="args"></param>
        /// <param name="argName"></param>
        /// <param name="required"></param>
        /// <returns></returns>
        //private static string GetArgumentValue(string argName, bool required = true)
        //{
        //    string argValue = "";
        //    //foreach (var arg in args)
        //    //{
        //    //    if (arg.StartsWith($"{argName}=", StringComparison.OrdinalIgnoreCase))
        //    //    {
        //    //        argValue = arg.Substring(arg.IndexOf("=") + 1);
        //    //        break;
        //    //    }
        //    //}
        //    if (required && argValue == "")
        //    {
        //        //Exit($@"'{argName}=' is not specified.");
        //    }
        //    return argValue;
        //}

        /// <summary>
        /// Recover an existing Bitcoin wallet
        /// </summary>
        public string RecoverWallet(string mnemonicString, string password)
        {

            var walletFilePath = GetWalletFilePath();
            AssertWalletNotExists(walletFilePath);

            AssertCorrectMnemonicFormat(mnemonicString);
            var mnemonic = new Mnemonic(mnemonicString);

            Safe safe = Safe.Recover(mnemonic, password, walletFilePath, Config.Network);

            // If no exception thrown the wallet is successfully recovered.
            return ($"Wallet {walletFilePath} is successfully recovered.");
        }

        /// <summary>
        /// tbd
        /// </summary>
        /// <param name="mnemonic"></param>
        public static void AssertCorrectMnemonicFormat(string mnemonic)
        {
            try
            {
                if (new Mnemonic(mnemonic).IsValidChecksum)
                {
                    return;
                }
            }
            catch (FormatException) { }
            catch (NotSupportedException) { }

            //Exit("Incorrect mnemonic format.");
        }

        #endregion
    }
}
