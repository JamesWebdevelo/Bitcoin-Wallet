using BusinessLayer.Assertions;
using HBitcoin.KeyManagement;
using NBitcoin;
using QBitNinja.Client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using BusinessLayer.Configuration;
using BusinessLayer.Communication;

namespace BusinessLayer.Models
{
    public static class Receiver
    {
        private static List<string> PublicAddresses { get; set; }

        public static List<string> GetPublicAddresses(string password)
        {
            /// Get current Wallet file
            var walletFilePath = Wallet.GetWalletFilePath(Config.DefaultWalletFileName);

            Safe safe = Wallet.DecryptWallet(walletFilePath, password);

            if (Config.ConnectionType == ConnectionType.Http)
            {
                PublicAddresses = CalculateAddressKeys(safe);
            }
            else if (Config.ConnectionType == ConnectionType.FullNode)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotImplementedException("Please check your config file and make sure it contains a connection type.");
            }

            return PublicAddresses;
        }

        /// <summary>
        /// Method returns 7 unused Bitcoin addressed based on the used Safe (Private Key)
        /// </summary>
        /// <param name="safe"></param>
        /// <returns></returns>
        private static List<string> CalculateAddressKeys(Safe safe)
        {
            List<string> output = new List<string>();

            /// Display seven unused addresses to the user
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
