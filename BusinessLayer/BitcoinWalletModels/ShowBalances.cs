//using HBitcoin.KeyManagement;
//using NBitcoin;
//using QBitNinja.Client.Models;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;

//namespace BusinessLayer.Models
//{
//    public partial class BitcoinWallet
//    {
//        public void ShowBalance()
//        {
//            var walletFilePath = GetWalletFilePath();

//            Safe safe = DecryptWalletByAskingForPassword(walletFilePath);

//            if (Config.ConnectionType == ConnectionType.Http)
//            {
//                // 0. Query all operations, grouped by addresses
//                Dictionary<BitcoinAddress, List<BalanceOperation>> operationsPerAddresses = QueryOperationsPerSafeAddresses(safe, 7);

//                // 1. Get all address history record with a wrapper class
//                var addressHistoryRecords = new List<AddressHistoryRecord>();
//                foreach (var elem in operationsPerAddresses)
//                {
//                    foreach (var op in elem.Value)
//                    {
//                        addressHistoryRecords.Add(new AddressHistoryRecord(elem.Key, op));
//                    }
//                }

//                // 2. Calculate wallet balances
//                Money confirmedWalletBalance;
//                Money unconfirmedWalletBalance;
//                GetBalances(addressHistoryRecords, out confirmedWalletBalance, out unconfirmedWalletBalance);

//                // 3. Group all address history records by addresses
//                var addressHistoryRecordsPerAddresses = new Dictionary<BitcoinAddress, HashSet<AddressHistoryRecord>>();
//                foreach (var address in operationsPerAddresses.Keys)
//                {
//                    var recs = new HashSet<AddressHistoryRecord>();
//                    foreach (var record in addressHistoryRecords)
//                    {
//                        if (record.Address == address)
//                            recs.Add(record);
//                    }
//                    addressHistoryRecordsPerAddresses.Add(address, recs);
//                }

//                // 4. Calculate address balances
//                WriteLine();
//                WriteLine("---------------------------------------------------------------------------");
//                WriteLine("Address\t\t\t\t\tConfirmed\tUnconfirmed");
//                WriteLine("---------------------------------------------------------------------------");
//                foreach (var elem in addressHistoryRecordsPerAddresses)
//                {
//                    Money confirmedBalance;
//                    Money unconfirmedBalance;
//                    GetBalances(elem.Value, out confirmedBalance, out unconfirmedBalance);
//                    if (confirmedBalance != Money.Zero || unconfirmedBalance != Money.Zero)
//                        WriteLine($"{elem.Key.ToString()}\t{confirmedBalance.ToDecimal(MoneyUnit.BTC).ToString("0.#############################")}\t\t{unconfirmedBalance.ToDecimal(MoneyUnit.BTC).ToString("0.#############################")}");
//                }
//                WriteLine("---------------------------------------------------------------------------");
//                WriteLine($"Confirmed Wallet Balance: {confirmedWalletBalance.ToDecimal(MoneyUnit.BTC).ToString("0.#############################")}btc");
//                WriteLine($"Unconfirmed Wallet Balance: {unconfirmedWalletBalance.ToDecimal(MoneyUnit.BTC).ToString("0.#############################")}btc");
//                WriteLine("---------------------------------------------------------------------------");
//            }
//            else if (Config.ConnectionType == ConnectionType.FullNode)
//            {
//                throw new NotImplementedException();
//            }
//            else
//            {
//                Exit("Invalid connection type.");
//            }
//        }

//        private static Safe DecryptWalletByAskingForPassword(string walletFilePath)
//        {
//            Safe safe = null;
//            string pw;
//            bool correctPw = false;
//            //WriteLine("Type your password:");
//            do
//            {
//                pw = PasswordConsole.ReadPassword();
//                try
//                {
//                    safe = Safe.Load(pw, walletFilePath);
//                    AssertCorrectNetwork(safe.Network);
//                    correctPw = true;
//                }
//                catch (System.Security.SecurityException)
//                {
//                    WriteLine("Invalid password, try again, (or press ctrl+c to exit):");
//                    correctPw = false;
//                }
//            } while (!correctPw);

//            if (safe == null)
//                throw new Exception("Wallet could not be decrypted.");
//            WriteLine($"{walletFilePath} wallet is decrypted.");
//            return safe;
//        }
//    }
//}
