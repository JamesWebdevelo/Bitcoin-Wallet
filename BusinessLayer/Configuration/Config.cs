using BusinessLayer.Configuration;
using NBitcoin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayer.Configuration
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
        public static string DefaultWalletFileName = "Wallet.json";
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
            /// Get the defined settings from json file
            var rawContent = ConfigFile.Deserialize(ConfigFile.ConfigFilePath);
            DefaultWalletFileName = rawContent.DefaultWalletFileName;

            /// 1. Decide which Network (based on Law Of Demeter)
            Network = GetNetwork(rawContent.Network);
            
            /// 2. Decide which Connection Type (based on Law Of Demeter)
            ConnectionType = GetConnectionType(rawContent.ConnectionType);
            
            /// 3. Decide if Unconfirmed can be spent (based on Law Of Demeter)
            CanSpendUnconfirmed = GetSpendInformation(rawContent.CanSpendUnconfirmed);
        }

        /// <summary>
        /// Returns the Network
        /// </summary>
        /// <param name="rawContent"></param>
        /// <returns></returns>
        public static Network GetNetwork(string value)
        {
            Network output;

            if (value == Network.Main.ToString())
            {
                output = Network.Main;
            }
            else if (value == Network.TestNet.ToString())
            {
                output = Network.TestNet;
            }
            else if (value == null)
            {
                throw new Exception($"Network is missing from {ConfigFile.ConfigFilePath}");
            }
            else
            {
                throw new Exception($"Wrong Network is specified in {ConfigFile.ConfigFilePath}");
            }
            return output;
        }

        /// <summary>
        /// Returns the Connectiontype
        /// </summary>
        /// <param name="rawContent"></param>
        /// <returns></returns>
        public static ConnectionType GetConnectionType(string value)
        {
            ConnectionType output;

            if (value == ConnectionType.FullNode.ToString())
            {
                output = ConnectionType.FullNode;
            }
            else if (value == ConnectionType.Http.ToString())
            {
                output = ConnectionType.Http;
            }
            else if (value == null)
            {
                throw new Exception($"ConnectionType is missing from {ConfigFile.ConfigFilePath}");
            }
            else
            {
                throw new Exception($"Wrong ConnectionType is specified in {ConfigFile.ConfigFilePath}");
            }

            return output;
        }

        /// <summary>
        /// Return whether the output is spendable 
        /// </summary>
        /// <param name="rawContent"></param>
        /// <returns></returns>
        public static bool GetSpendInformation(string value)
        {
            bool output;

            if (value == "True")
            {
                output = true;
            }
            else if (value == "False")
            {
                output = false;
            }
            else if (value == null)
            {
                throw new Exception($"CanSpendUnconfirmed is missing from {ConfigFile.ConfigFilePath}");
            }
            else
            {
                throw new Exception($"Wrong CanSpendUnconfirmed is specified in {ConfigFile.ConfigFilePath}");
            }

            return output;
        }

        /// <summary>
        /// Creates a new Config.json File
        /// </summary>
        public static void Save()
        {
            bool fetchWasSuccessfull = ConfigFile.Serialize(DefaultWalletFileName, Network.ToString(), ConnectionType.ToString(), CanSpendUnconfirmed.ToString());
            if(fetchWasSuccessfull == true)
            {
                Load();
            }
        }
        #endregion
    }
}
