using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayer.Models
{
    public class WalletFile
    {
        public string WalletFileName { get; set; }
        public string WalletFilePath { get; set; }

        /// <summary>
        /// Get the Wallet file path
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// Changed from private to internal
        internal static string GetWalletFilePath()
        {
            /// Keep default name for the time being
            string walletFileName = Config.DefaultWalletFileName;

            /// Create Directory and the "Wallets" Folder
            var walletDirName = "Wallets";
            Directory.CreateDirectory(walletDirName);
            return Path.Combine(walletDirName, walletFileName);
        }
    }
}
