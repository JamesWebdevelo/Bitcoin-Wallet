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

        static Config()
        {
            if (!File.Exists(ConfigFileSerializer.ConfigFilePath))
            {
                Save();
            }
            Load();
        }

        /// <summary>
        /// Method
        /// </summary>
        public static void Load()
        {
            var rawContent = ConfigFileSerializer.Deserialize();

            DefaultWalletFileName = rawContent.DefaultWalletFileName;

            if (rawContent.Network == Network.Main.ToString())
                Network = Network.Main;
            else if (rawContent.Network == Network.TestNet.ToString())
                Network = Network.TestNet;
            else if (rawContent.Network == null)
                throw new Exception($"Network is missing from {ConfigFileSerializer.ConfigFilePath}");
            else
                throw new Exception($"Wrong Network is specified in {ConfigFileSerializer.ConfigFilePath}");

            if (rawContent.ConnectionType == ConnectionType.FullNode.ToString())
                ConnectionType = ConnectionType.FullNode;
            else if (rawContent.ConnectionType == ConnectionType.Http.ToString())
                ConnectionType = ConnectionType.Http;
            else if (rawContent.ConnectionType == null)
                throw new Exception($"ConnectionType is missing from {ConfigFileSerializer.ConfigFilePath}");
            else
                throw new Exception($"Wrong ConnectionType is specified in {ConfigFileSerializer.ConfigFilePath}");

            if (rawContent.CanSpendUnconfirmed == "True")
                CanSpendUnconfirmed = true;
            else if (rawContent.CanSpendUnconfirmed == "False")
                CanSpendUnconfirmed = false;
            else if (rawContent.CanSpendUnconfirmed == null)
                throw new Exception($"CanSpendUnconfirmed is missing from {ConfigFileSerializer.ConfigFilePath}");
            else
                throw new Exception($"Wrong CanSpendUnconfirmed is specified in {ConfigFileSerializer.ConfigFilePath}");
        }

        /// <summary>
        /// Method
        /// </summary>
        public static void Save()
        {
            ConfigFileSerializer.Serialize(DefaultWalletFileName, Network.ToString(), ConnectionType.ToString(), CanSpendUnconfirmed.ToString());
            Load();
        }
    }
}
