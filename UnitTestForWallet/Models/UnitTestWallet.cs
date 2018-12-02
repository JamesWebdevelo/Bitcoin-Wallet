using System;
using Xunit;
using BusinessLayer.Configuration;
using BusinessLayer;
using NBitcoin;
using BusinessLayer.Models;
using System.IO;

namespace UnitTestForWallet
{
    public class UnitTestWallet 
    {
        [Fact]
        public void GetWalletFilePath_ReturnExpectedPath()
        {
            // Arrange
            string fileName = "Walleter.json";
            string expected = $@"Wallets\{fileName}";

            // Act
            var actual = Wallet.GetWalletFilePath(fileName);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GenerateWallet_HandleCaseIfSomethingGoesWrong()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void GenerateWallet_ReturnPassphrase()
        {
            // Arrange
            string password = "admin";
            // Act
            string actual = Wallet.GenerateWallet(password);
            
            // Assert
            Assert.True(actual.Length > 10);
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void RecoverWallet_WalletHasBeenSuccessfullyRecovered()
        {
            // Arrange
            // Delete wallet file before try to recover.
            var path = Wallet.GetWalletFilePath(Config.DefaultWalletFileName);
            Directory.Delete(path);
            
            string password = "admin";
            string phrase = Wallet.GenerateWallet(password);
            
            // Act
            var expected = Wallet.RecoverWallet(phrase, password);

            // Assert
            Assert.Contains(expected, "Wallet is successfully recovered.");
        }
    }
}
