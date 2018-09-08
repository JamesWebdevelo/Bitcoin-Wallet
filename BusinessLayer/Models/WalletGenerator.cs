using HBitcoin.KeyManagement;
using NBitcoin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace BusinessLayer.Models
{
    public class WalletGenerator : WalletFile
    {
        /// <summary>
        /// Method
        /// </summary>
        public string GenerateWallet(string password)
        {
            /// Get Path of Wallet file
            var walletFilePath = GetWalletFilePath();
            string response = Assertion.AssertWalletNotExists(walletFilePath);

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
    }
}
