using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayer.Configuration
{
    public class Deserializer : ConfigFile
    {
        /// <summary>
        /// Read the defined settings from the Config.json file
        /// </summary>
        /// <returns></returns>
        internal static ConfigFile Deserialize()
        {
            if (!File.Exists(ConfigFilePath))
            {
                throw new Exception($"Config file does not exist. Create {ConfigFilePath} before reading it.");
            }

            /// Select config file content, parse it and return stringified Properties
            var contentString = File.ReadAllText(ConfigFilePath);
            var configFileSerializer = JsonConvert.DeserializeObject<Deserializer>(contentString);

            return new ConfigFile(configFileSerializer.DefaultWalletFileName, configFileSerializer.Network, configFileSerializer.ConnectionType, configFileSerializer.CanSpendUnconfirmed);
        }
    }
}
