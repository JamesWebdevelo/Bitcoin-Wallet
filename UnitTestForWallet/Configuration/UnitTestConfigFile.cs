using System;
using Xunit;
using BusinessLayer.Configuration;
using System.IO;
using NBitcoin;
using BusinessLayer;

namespace UnitTestForWallet
{
    public class UnitTestConfigFile
    {
        [Fact]
        public void ConfigFile_FilePathIsCorrect()
        {
            // Arrange
            string expected = "Config.json";

            // Act
            string actual = ConfigFile.ConfigFilePath;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConfigFile_JsonConstructorWorks()
        {
            // Arrange
            string walletFileName = "TestWallet";
            string network = "Test";
            string connectionType = "Http";
            string canSpendUnconfirmed = "false";

            // Act
            ConfigFile testFile = new ConfigFile(walletFileName, network, connectionType, canSpendUnconfirmed);

            // Assert
            Assert.Equal(walletFileName, testFile.DefaultWalletFileName);
            Assert.Equal(network, testFile.Network);
            Assert.Equal(connectionType, testFile.ConnectionType);
            Assert.Equal(canSpendUnconfirmed, testFile.CanSpendUnconfirmed);
        }

        [Theory]
        [InlineData("")]
        [InlineData("NoConfigFileName")]
        public void Deserialize_ExpectionIfFileNotExists(string expected)
        {
            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => ConfigFile.Deserialize(expected));
        }

        [Fact]
        public void Deserialize_ReturnsValuesForAllProperties()
        {
            // Arrange
            Config.Load();
            string value = "Config.json";

            ConfigFile expected = new ConfigFile();

            // Act
            var actual = ConfigFile.Deserialize(value);

            // Assert
            Assert.NotEmpty(actual.DefaultWalletFileName);
            Assert.NotEmpty(actual.Network);
            Assert.NotEmpty(actual.ConnectionType);
            Assert.NotEmpty(actual.CanSpendUnconfirmed);
        }

        [Fact]
        public void Serialize_WasSuccessful()
        {
            // Arrange
            bool expected = true;

            string walletFileName = "BitcoinWallet.json";
            string network = "TestNet";
            string connectionType = "Http";
            string canSpendUnconfirmed = "False";

            // Act
            bool actual = ConfigFile.Serialize(walletFileName, network, connectionType, canSpendUnconfirmed);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "", "", "")]
        [InlineData("BitcoinWallet.json", "TestNet", "Http", "")]
        [InlineData("", "TestNet", "Http", "False")]
        public void Serialize_FlagIfNotSuccessfull(string walletFileName, string network, string connectionType, string canSpendUnconfirmed)
        {
            // Arrange
            bool expected = false;

            // Act
            bool actual = ConfigFile.Serialize(walletFileName, network, connectionType, canSpendUnconfirmed);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
