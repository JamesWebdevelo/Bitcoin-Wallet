using BusinessLayer.Configuration;
using NBitcoin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayer
{
    public enum ConnectionType
    {
        Http,
        FullNode
    }

    /// <summary>
    /// This Config class stores global settings.
    /// </summary>
    public static class Config
    {
        /// Initialized with default attributes
        public static string DefaultWalletFileName = @"BitcoinWallet.json";
        public static Network Network = Network.TestNet;
        public static bool CanSpendUnconfirmed = false;
        public static ConnectionType ConnectionType = ConnectionType.Http;

        #region Constructor
        static Config()
        {
            /// Create the default Config File if it does not exists
            if (!File.Exists(ConfigFile.ConfigFilePath))
            {
                Save();
            }
            /// Otherwise just load it.
            Load();
        }
        #endregion

        #region Methods
        public static void Load()
        {
            /// Get the defined settings
            var rawContent = Deserializer.Deserialize();

            DefaultWalletFileName = rawContent.DefaultWalletFileName;

            /// 1. Decide which Network
            if (rawContent.Network == Network.Main.ToString())
            {
                Network = Network.Main;
            }
            else if (rawContent.Network == Network.TestNet.ToString())
            {
                Network = Network.TestNet;
            }
            else if (rawContent.Network == null)
            {
                throw new Exception($"Network is missing from {ConfigFile.ConfigFilePath}");
            }
            else
            {
                throw new Exception($"Wrong Network is specified in {ConfigFile.ConfigFilePath}");
            }

            /// 2. Decide which Connection Type
            if (rawContent.ConnectionType == ConnectionType.FullNode.ToString())
            {
                ConnectionType = ConnectionType.FullNode;
            }
            else if (rawContent.ConnectionType == ConnectionType.Http.ToString())
            {
                ConnectionType = ConnectionType.Http;
            }
            else if (rawContent.ConnectionType == null)
            {
                throw new Exception($"ConnectionType is missing from {ConfigFile.ConfigFilePath}");
            }
            else
            {
                throw new Exception($"Wrong ConnectionType is specified in {ConfigFile.ConfigFilePath}");
            }

            /// 3. Decide if Unconfirmed can be spent
            if (rawContent.CanSpendUnconfirmed == "True")
            {
                CanSpendUnconfirmed = true;
            }
            else if (rawContent.CanSpendUnconfirmed == "False")
            {
                CanSpendUnconfirmed = false;
            }
            else if (rawContent.CanSpendUnconfirmed == null)
            {
                throw new Exception($"CanSpendUnconfirmed is missing from {ConfigFile.ConfigFilePath}");
            }
            else
            {
                throw new Exception($"Wrong CanSpendUnconfirmed is specified in {ConfigFile.ConfigFilePath}");
            }
        }

        /// <summary>
        /// Creates a new Config.json File
        /// </summary>
        public static void Save()
        {
            Serializer.Serialize(DefaultWalletFileName, Network.ToString(), ConnectionType.ToString(), CanSpendUnconfirmed.ToString());
            Load();
        }
        #endregion
    }
}
