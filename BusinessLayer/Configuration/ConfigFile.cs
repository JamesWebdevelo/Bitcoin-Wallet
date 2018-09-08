//using NBitcoin;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace BusinessLayer.Configuration
//{
//    public class ConfigFile
//    {
//        /// Static Properties
//        public static string FilePath { get; set; } = "Config.json";

//        /// Properties
//        public string DefaultWalletFileName { get; set; }
//        public Network Network { get; set; }
//        public bool CanSpendUnconfirmed { get; set; }
//        public ConnectionType ConnectionType { get; set; }

//        /// <summary>
//        /// Create new config File
//        /// </summary>
//        public ConfigFile()
//        {
//            DefaultWalletFileName = @"BitcoinWallet.json";
//            Network = Network.TestNet;
//            CanSpendUnconfirmed = false;
//            ConnectionType = ConnectionType.Http;
//        }

//        /// <summary>
//        /// Load existing Config file
//        /// </summary>
//        /// <param name="configFile"></param>
//        public ConfigFile(ConfigFile cF)
//        {
//            DefaultWalletFileName = cF.DefaultWalletFileName;
//            Network = cF.Network;
//            CanSpendUnconfirmed = cF.CanSpendUnconfirmed;
//            ConnectionType = cF.ConnectionType;
//        }
//    }

//    public enum ConnectionType
//    {
//        Http,
//        FullNode
//    }
//}
