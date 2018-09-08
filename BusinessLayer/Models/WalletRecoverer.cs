using HBitcoin.KeyManagement;
using NBitcoin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayer.Models
{
    public class WalletRecoverer
    {
        /// <summary>
        /// Recover an existing Bitcoin wallet
        /// </summary>
        public string RecoverWallet(string mnemonicString, string password)
        {

            var walletFilePath = WalletGenerator.GetWalletFilePath();
            Assertion.AssertWalletNotExists(walletFilePath);

            Assertion.AssertCorrectMnemonicFormat(mnemonicString);
            var mnemonic = new Mnemonic(mnemonicString);

            Safe safe = Safe.Recover(mnemonic, password, walletFilePath, Config.Network);

            // If no exception thrown the wallet is successfully recovered.
            return ($"Wallet {walletFilePath} is successfully recovered.");
        }
    }
}
