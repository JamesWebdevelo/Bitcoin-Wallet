using HBitcoin.KeyManagement;
using NBitcoin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace BusinessLayer.Models
{
    public class Wallet
    {
        /// <summary>
        /// Properties
        /// </summary>
        public Key PrivateKey { set; get; }

        /// <summary>
        /// Method
        /// </summary>
        public string GenerateWallet()
        {
            Assertion.AssertArgumentsLenght(/*args.Length*/ 2, 1, 2);

            string[] args = null;
            var walletFilePath = GetWalletFilePath(args);
            AssertWalletNotExists(walletFilePath);

            string pw;
            string pwConf;
            do
            {
                /// 1. Get password from user
                //WriteLine("Choose a password:");
                //pw = PasswordConsole.ReadPassword();
                pw = "admin";
                /// 2. Get password confirmation from user
                //WriteLine("Confirm password:");
                //pwConf = PasswordConsole.ReadPassword();
                pwConf = "admin";

                if (pw != pwConf)
                {
                    //WriteLine("Passwords do not match. Try again!");
                }
            }
            while (pw != pwConf);

            /// 3. Create wallet
            try
            {
                /// S 
                Safe safe = Safe.Create(out Mnemonic mnemonic, pw, walletFilePath, Config.Network);
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
            if (walletFileName == "") walletFileName = Config.DefaultWalletFileName;

            var walletDirName = "Wallets";
            Directory.CreateDirectory(walletDirName);
            return Path.Combine(walletDirName, walletFileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="walletFilePath"></param>
        public static void AssertWalletNotExists(string walletFilePath)
        {
            if (File.Exists(walletFilePath))
            {
                //Exit($"A wallet, named {walletFilePath} already exists.");
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
