using BusinessLayer.Configuration;
using HBitcoin.KeyManagement;
using NBitcoin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace BusinessLayer.Assertions
{
    public class Assertion
    {
        /// <summary>
        /// Checks if wallet file already exists
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
        /// Checks the inserted Mnemonic if it has the correct format
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
        }

        /// <summary>
        /// Makes sure the selected Wallet is associated with the proper network
        /// </summary>
        /// <param name="network"></param>
        public static string AssertCorrectNetwork(Network network)
        {
            if (network != Config.Network)
            {
                return($"The wallet you want to load is on the {network} Bitcoin network. But your config file specifies {Config.Network} Bitcoin network.");
            }
            else
            {
                return "test";
            }
        }
    }
}
