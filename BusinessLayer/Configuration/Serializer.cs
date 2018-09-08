using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayer.Configuration
{
    public class Serializer
    {
        #region Properties
        public static string ConfigFilePath = "Config.json";

        /// Keep Properties public to get serialized
        public string DefaultWalletFileName { get; set; }
        public string Network { get; set; }
        public string ConnectionType { get; set; }
        public string CanSpendUnconfirmed { get; set; }
        #endregion

        #region Constructor
        [JsonConstructor]
        private Serializer(string walletFileName, string network, string connectionType, string canSpendUnconfirmed)
        {
            DefaultWalletFileName = walletFileName;
            Network = network;
            ConnectionType = connectionType;
            CanSpendUnconfirmed = canSpendUnconfirmed;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Write the defined settings into the Config.json file
        /// </summary>
        /// <param name="walletFileName"></param>
        /// <param name="network"></param>
        /// <param name="connectionType"></param>
        /// <param name="canSpendUnconfirmed"></param>
        internal static void Serialize(string walletFileName, string network, string connectionType, string canSpendUnconfirmed)
        {
            var content = JsonConvert.SerializeObject(new Serializer(walletFileName, network, connectionType, canSpendUnconfirmed), Formatting.Indented);
            File.WriteAllText(ConfigFilePath, content);
        }

        /// <summary>
        /// Read the defined settings from the Config.json file
        /// </summary>
        /// <returns></returns>
        internal static Serializer Deserialize()
        {
            if (!File.Exists(ConfigFilePath))
            {
                throw new Exception($"Config file does not exist. Create {ConfigFilePath} before reading it.");
            }

            var contentString = File.ReadAllText(ConfigFilePath);
            var configFileSerializer = JsonConvert.DeserializeObject<Serializer>(contentString);

            return new Serializer(configFileSerializer.DefaultWalletFileName, configFileSerializer.Network, configFileSerializer.ConnectionType, configFileSerializer.CanSpendUnconfirmed);
        }
        #endregion
    }
}
