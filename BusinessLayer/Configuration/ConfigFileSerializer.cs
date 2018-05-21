using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayer.Configuration
{
    public class ConfigFileSerializer
    {
        /// <summary>
        /// Properties
        /// </summary>
        public static string ConfigFilePath = "Config.json";

        /// Keep Properties public to get serialized
        public string DefaultWalletFileName { get; set; }
        public string Network { get; set; }
        public string ConnectionType { get; set; }
        public string CanSpendUnconfirmed { get; set; }

        /// <summary>
        /// Method
        /// </summary>
        /// <param name="walletFileName"></param>
        /// <param name="network"></param>
        /// <param name="connectionType"></param>
        /// <param name="canSpendUnconfirmed"></param>
        [JsonConstructor]
        private ConfigFileSerializer(string walletFileName, string network, string connectionType, string canSpendUnconfirmed)
        {
            DefaultWalletFileName = walletFileName;
            Network = network;
            ConnectionType = connectionType;
            CanSpendUnconfirmed = canSpendUnconfirmed;
        }

        /// <summary>
        /// Method
        /// </summary>
        /// <param name="walletFileName"></param>
        /// <param name="network"></param>
        /// <param name="connectionType"></param>
        /// <param name="canSpendUnconfirmed"></param>
        internal static void Serialize(string walletFileName, string network, string connectionType, string canSpendUnconfirmed)
        {
            var content =
                JsonConvert.SerializeObject(new ConfigFileSerializer(walletFileName, network, connectionType, canSpendUnconfirmed), Formatting.Indented);

            File.WriteAllText(ConfigFilePath, content);
        }

        /// <summary>
        /// Method
        /// </summary>
        /// <returns></returns>
        internal static ConfigFileSerializer Deserialize()
        {
            if (!File.Exists(ConfigFilePath))
                throw new Exception($"Config file does not exist. Create {ConfigFilePath} before reading it.");

            var contentString = File.ReadAllText(ConfigFilePath);
            var configFileSerializer = JsonConvert.DeserializeObject<ConfigFileSerializer>(contentString);

            return new ConfigFileSerializer(configFileSerializer.DefaultWalletFileName, configFileSerializer.Network, configFileSerializer.ConnectionType, configFileSerializer.CanSpendUnconfirmed);
        }
    }
}
