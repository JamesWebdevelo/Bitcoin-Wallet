using NBitcoin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

/// Allows to test interal methods
[assembly: InternalsVisibleTo("UnitTestForWallet")]

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

        public ConfigFile() { }

        [JsonConstructor]
        internal ConfigFile(string walletFileName, string network, string connectionType, string canSpendUnconfirmed)
        {
            DefaultWalletFileName = walletFileName;
            Network = network;
            ConnectionType = connectionType;
            CanSpendUnconfirmed = canSpendUnconfirmed;
        }

        /// <summary>
        /// Write the defined settings into the Config.json file
        /// </summary>
        /// <param name="walletFileName"></param>
        /// <param name="network"></param>
        /// <param name="connectionType"></param>
        /// <param name="canSpendUnconfirmed"></param>
        internal static bool Serialize(string walletFileName, string network, string connectionType, string canSpendUnconfirmed)
        {
            if(walletFileName == "" || network == "" || connectionType == "" || canSpendUnconfirmed == "")
            {
                return false;
            }
            else
            {
                try
                {
                    var content = JsonConvert.SerializeObject(
                    new ConfigFile(walletFileName, network, connectionType, canSpendUnconfirmed),
                    Formatting.Indented);

                    /// Write into file
                    File.WriteAllText(ConfigFilePath, content);
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        /// <summary>
        /// Read the defined settings from the Config.json file
        /// </summary>
        /// <returns></returns>
        internal static ConfigFile Deserialize(string configFilePath)
        {
            if (File.Exists(configFilePath))
            {
                /// Select config file content, parse it and return stringified Properties
                var contentString = File.ReadAllText(configFilePath);
                var configFileSerializer = JsonConvert.DeserializeObject<ConfigFile>(contentString);

                return new ConfigFile(configFileSerializer.DefaultWalletFileName, configFileSerializer.Network, configFileSerializer.ConnectionType, configFileSerializer.CanSpendUnconfirmed);
            }
            else
            {
                throw new FileNotFoundException($"Config file does not exist. Create {configFilePath} first.");
            }
        }
    }
}
