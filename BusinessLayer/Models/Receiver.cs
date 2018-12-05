using BusinessLayer.Assertions;
using BusinessLayer.Communication;
using HBitcoin.KeyManagement;
using NBitcoin;
using QBitNinja.Client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;

namespace BusinessLayer.Models
{
    public class Receiver
    {
        private List<string> PublicAddresses { get; set; }

        public List<string> ReceiveCoins(string password)
        {
            /// Get current Wallet file
            var walletFilePath = Wallet.GetWalletFilePath(Config.DefaultWalletFileName);

            Safe safe = DecryptWallet(walletFilePath, password);

            if (Config.ConnectionType == ConnectionType.Http)
            {
                PublicAddresses = GetPublicAddresses(safe);
            }
            else if (Config.ConnectionType == ConnectionType.FullNode)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotImplementedException();
            }

            return PublicAddresses;
        }

        private static Safe DecryptWallet(string walletFilePath, string password)
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

        /// <summary>
        /// Method returns 7 unused Bitcoin addressed based on the used Safe (Private Key)
        /// </summary>
        /// <param name="safe"></param>
        /// <returns></returns>
        private static List<string> GetPublicAddresses(Safe safe)
        {
            List<string> output = null;

            /// Just want to show the user 7 unused addresses
            /// Dictionary<BitcoinAddress, List<BalanceOperation>> 
            var operationsPerReceiveAddresses = QBitNinjaQuerrier.QueryOperationsPerSafeAddresses(safe, 7, HdPathType.Receive);

            foreach (var elem in operationsPerReceiveAddresses)
            {
                if (elem.Value.Count == 0)
                {
                    output.Add(elem.Key.ToString());
                }
            }
            return output;
        }
    }
}
