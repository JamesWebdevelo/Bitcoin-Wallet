using System;
using Xunit;
using BusinessLayer.Configuration;

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
    }
}
