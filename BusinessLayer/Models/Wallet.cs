using BusinessLayer.Assertions;
using BusinessLayer.Configuration;
using HBitcoin.KeyManagement;
using NBitcoin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

/// Allows to test interal methods
[assembly: InternalsVisibleTo("UnitTestForWallet")]

namespace BusinessLayer.Models
{
    public static class Wallet
    {
        private static string _walletFileName = Config.DefaultWalletFileName;
        private static string _walletFilePath = "";

        private static string _walletDirName = @"Wallets";

        /// <summary>
        /// Generate a new Wallet
        /// </summary>
        public static string GenerateWallet(string password)
        {
            /// Get Path of Wallet file
            _walletFilePath = GetWalletFilePath(_walletFileName);
            string response = Assertion.AssertWalletNotExists(_walletFilePath);

            /// Create wallet if not already exists
            if (response is null)
            {
                try
                {
                    /// Use Safe class to properly manage our private keys
                    Safe safe = Safe.Create(out Mnemonic mnemonic, password, _walletFilePath, Config.Network);
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
        /// Recover an existing Bitcoin wallet
        /// </summary>
        public static string RecoverWallet(string mnemonicString, string password)
        {
            string walletFilePath = GetWalletFilePath(_walletFileName);

            /// Make sure wallet exists and passphrase is valid
            Assertion.AssertWalletNotExists(_walletFilePath);
            Assertion.AssertCorrectMnemonicFormat(mnemonicString);

            /// Initiate recovering
            var mnemonic = new Mnemonic(mnemonicString);
            Safe safe = Safe.Recover(mnemonic, password, _walletFilePath, Config.Network);

            /// If no exception thrown the wallet is successfully recovered.
            return ($"Wallet {_walletFileName} is successfully recovered.");
        }

        /// <summary>
        /// Get the Wallet file path
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// Changed from private to internal
        internal static string GetWalletFilePath(string walletFileName)
        {
            /// Create Directory if not exists yet
            Directory.CreateDirectory(_walletDirName);

            /// Return Wallet folder Path
            return Path.Combine(_walletDirName, walletFileName);
        }

        /// <summary>
        ///  Decrypt the wallet
        /// </summary>
        /// <param name="walletFilePath"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        internal static Safe DecryptWallet(string walletFilePath, string password)
        {
            Safe output = null;

            try
            {
                /// Load safe with password
                output = Safe.Load(password, walletFilePath);
                Assertion.AssertCorrectNetwork(output.Network);
            }
            catch
            {
                throw new SecurityException("Invalid password, try again.");
            }

            if (output == null)
            {
                throw new Exception("Wallet could not be decrypted.");
            }

            return output;
        }
    }
}
