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
    public class CoinReceiver
    {
        public void ReceiveCoins()
        {
            var walletFilePath = WalletGenerator.GetWalletFilePath();

            Safe safe = DecryptWalletByAskingForPassword(walletFilePath);

            if (Config.ConnectionType == ConnectionType.Http)
            {
                Dictionary<BitcoinAddress, List<BalanceOperation>> operationsPerReceiveAddresses = QBitNinjaQuerrier.QueryOperationsPerSafeAddresses(safe, 7, HdPathType.Receive);

                //WriteLine("---------------------------------------------------------------------------");
                //WriteLine("Unused Receive Addresses");
                //WriteLine("---------------------------------------------------------------------------");
                foreach (var elem in operationsPerReceiveAddresses)
                {
                    if (elem.Value.Count == 0)
                    {
                        //WriteLine($"{elem.Key.ToString()}");
                    }
                }

            }
            else if (Config.ConnectionType == ConnectionType.FullNode)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotImplementedException();
            }
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
    }
}
