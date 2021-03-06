﻿using BusinessLayer.Configuration;
using HBitcoin.KeyManagement;
using NBitcoin;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Communication
{
    public class QBitNinjaQuerrier
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="safe"></param>
        /// <param name="minUnusedKeys"></param>
        /// <param name="hdPathType"></param>
        /// <returns></returns>
        public static Dictionary<BitcoinAddress, List<BalanceOperation>> QueryOperationsPerSafeAddresses(Safe safe, int minUnusedKeys = 7, HdPathType? hdPathType = null)
        {
            if (hdPathType == null)
            {
                Dictionary<BitcoinAddress, List<BalanceOperation>> operationsPerReceiveAddresses = QueryOperationsPerSafeAddresses(safe, 7, HdPathType.Receive);
                Dictionary<BitcoinAddress, List<BalanceOperation>> operationsPerChangeAddresses = QueryOperationsPerSafeAddresses(safe, 7, HdPathType.Change);

                var operationsPerAllAddresses = new Dictionary<BitcoinAddress, List<BalanceOperation>>();
                foreach (var elem in operationsPerReceiveAddresses)
                    operationsPerAllAddresses.Add(elem.Key, elem.Value);
                foreach (var elem in operationsPerChangeAddresses)
                    operationsPerAllAddresses.Add(elem.Key, elem.Value);
                return operationsPerAllAddresses;
            }

            var addresses = safe.GetFirstNAddresses(minUnusedKeys, hdPathType.GetValueOrDefault());
            //var addresses = FakeData.FakeSafe.GetFirstNAddresses(minUnusedKeys);

            var operationsPerAddresses = new Dictionary<BitcoinAddress, List<BalanceOperation>>();
            var unusedKeyCount = 0;
            foreach (var elem in QueryOperationsPerAddresses(addresses))
            {
                operationsPerAddresses.Add(elem.Key, elem.Value);
                if (elem.Value.Count == 0) unusedKeyCount++;
            }
            //WriteLine($"{operationsPerAddresses.Count} {hdPathType} keys are processed.");

            var startIndex = minUnusedKeys;
            while (unusedKeyCount < minUnusedKeys)
            {
                addresses = new List<BitcoinAddress>();
                for (int i = startIndex; i < startIndex + minUnusedKeys; i++)
                {
                    addresses.Add(safe.GetAddress(i, hdPathType.GetValueOrDefault()));
                    //addresses.Add(FakeData.FakeSafe.GetAddress(i));
                }
                /// Endless Loop 
                foreach (var elem in QueryOperationsPerAddresses(addresses))
                {
                    operationsPerAddresses.Add(elem.Key, elem.Value);
                    if (elem.Value.Count == 0) unusedKeyCount++;
                }
                // WriteLine($"{operationsPerAddresses.Count} {hdPathType} keys are processed.");
                startIndex += minUnusedKeys;
            }

            return operationsPerAddresses;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addresses"></param>
        /// <returns></returns>
        public static Dictionary<BitcoinAddress, List<BalanceOperation>> QueryOperationsPerAddresses(IEnumerable<BitcoinAddress> addresses)
        {
            var operationsPerAddresses = new Dictionary<BitcoinAddress, List<BalanceOperation>>();
            var client = new QBitNinjaClient(Config.Network);
            foreach (var addr in addresses)
            {
                var operations = client.GetBalance(addr, unspentOnly: false).Result.Operations;
                operationsPerAddresses.Add(addr, operations);
            }
            return operationsPerAddresses;
        }
    }
}
