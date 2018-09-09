using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayer.Configuration
{
    public class Serializer : ConfigFile
    {
        /// <summary>
        /// Write the defined settings into the Config.json file
        /// </summary>
        /// <param name="walletFileName"></param>
        /// <param name="network"></param>
        /// <param name="connectionType"></param>
        /// <param name="canSpendUnconfirmed"></param>
        internal static void Serialize(string walletFileName, string network, string connectionType, string canSpendUnconfirmed)
        {
            var content = JsonConvert.SerializeObject(
                new ConfigFile(walletFileName, network, connectionType, canSpendUnconfirmed), 
                Formatting.Indented);
            File.WriteAllText(ConfigFilePath, content);
        }
    }
}
