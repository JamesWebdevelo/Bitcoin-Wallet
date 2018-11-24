using HBitcoin.KeyManagement;
using NBitcoin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayer.Models
{
    public static class WalletRecoverer
    {
        /// <summary>
        /// Recover an existing Bitcoin wallet
        /// </summary>
        public static string RecoverWallet(string mnemonicString, string password)
        {
            var walletFilePath = WalletFile.GetWalletFilePath();
            Assertion.AssertWalletNotExists(walletFilePath);

            Assertion.AssertCorrectMnemonicFormat(mnemonicString);
            var mnemonic = new Mnemonic(mnemonicString);

            Safe safe = Safe.Recover(mnemonic, password, walletFilePath, Config.Network);

            // If no exception thrown the wallet is successfully recovered.
            return ($"Wallet {walletFilePath} is successfully recovered.");
        }
    }
}
