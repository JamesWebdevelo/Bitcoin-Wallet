using BusinessLayer.Assertions;
using BusinessLayer.Communication;
using HBitcoin.KeyManagement;
using NBitcoin;
using QBitNinja.Client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayer.Models
{
    public class Receiver
    {
        private List<string> PublicAddresses { get; set; }

        public List<string> ReceiveCoins()
        {
            /// Get current Wallet file
            var walletFilePath = Wallet.GetWalletFilePath(Config.DefaultWalletFileName);

            Safe safe = DecryptWalletByAskingForPassword(walletFilePath);

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

        private static Safe DecryptWalletByAskingForPassword(string walletFilePath)
        {
            Safe safe = null;
            string pw;
            bool correctPw = false;
            //WriteLine("Type your password:");
            do
            {
                pw = "admin"; //PasswordConsole.ReadPassword();
                try
                {
                    safe = Safe.Load(pw, walletFilePath);
                    Assertion.AssertCorrectNetwork(safe.Network);
                    correctPw = true;
                }
                catch (System.Security.SecurityException)
                {
                    //WriteLine("Invalid password, try again, (or press ctrl+c to exit):");
                    correctPw = false;
                }
            } while (!correctPw);

            if (safe == null)
            {
                throw new Exception("Wallet could not be decrypted.");
            }
            //WriteLine($"{walletFilePath} wallet is decrypted.");
            return safe;
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
            Dictionary<BitcoinAddress, List<BalanceOperation>> operationsPerReceiveAddresses = QBitNinjaQuerrier.QueryOperationsPerSafeAddresses(safe, 7, HdPathType.Receive);

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
