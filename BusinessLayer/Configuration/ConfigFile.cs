using NBitcoin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

/// Allows to test interal methods
[assembly: InternalsVisibleTo("UnitTestForWallet")]

namespace BusinessLayer.Configuration
{
    public class ConfigFile : IConfigFile
    {
        /// Keep Properties public to get serialized
        public static string ConfigFilePath = "Config.json";

        public string DefaultWalletFileName { get; set; }
        public string Network { get; set; }
        public string ConnectionType { get; set; }
        public string CanSpendUnconfirmed { get; set; }

        public ConfigFile() { }

        [JsonConstructor]
        internal ConfigFile(string walletFileName, string network, string connectionType, string canSpendUnconfirmed)
        {
            DefaultWalletFileName = walletFileName;
            Network = network;
            ConnectionType = connectionType;
            CanSpendUnconfirmed = canSpendUnconfirmed;
        }
    }
}
