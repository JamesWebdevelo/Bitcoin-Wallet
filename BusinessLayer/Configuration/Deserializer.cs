using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayer.Configuration
{
    public class Deserializer
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
        private Deserializer(string walletFileName, string network, string connectionType, string canSpendUnconfirmed)
        {
            DefaultWalletFileName = walletFileName;
            Network = network;
            ConnectionType = connectionType;
            CanSpendUnconfirmed = canSpendUnconfirmed;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Read the defined settings from the Config.json file
        /// </summary>
        /// <returns></returns>
        internal static Deserializer Deserialize()
        {
            if (!File.Exists(ConfigFilePath))
            {
                throw new Exception($"Config file does not exist. Create {ConfigFilePath} before reading it.");
            }

            var contentString = File.ReadAllText(ConfigFilePath);
            var configFileSerializer = JsonConvert.DeserializeObject<Deserializer>(contentString);

            return new Deserializer(configFileSerializer.DefaultWalletFileName, configFileSerializer.Network, configFileSerializer.ConnectionType, configFileSerializer.CanSpendUnconfirmed);
        }
        #endregion
    }
}
