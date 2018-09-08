using NBitcoin;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Configuration
{
    public class ConfigFile
    {
        /// Keep Properties public to get serialized
        public static string ConfigFilePath = "Config.json";

        public string DefaultWalletFileName { get; set; }
        public string Network { get; set; }
        public string ConnectionType { get; set; }
        public string CanSpendUnconfirmed { get; set; }
    }
}
