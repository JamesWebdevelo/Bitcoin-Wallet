using System;
using Xunit;
using BusinessLayer.Configuration;
using BusinessLayer;
using NBitcoin;

namespace UnitTestForWallet
{
    public class UnitTestConfig
    {
        [Theory]
        [InlineData("TestNet")]
        [InlineData("Main")]
        public void GetNetwork_ReturnsCorrectNetwork(string expected)
        {
            // Act
            Network actual = Config.GetNetwork(expected);

            // Assert
            Assert.Equal(expected, actual.ToString());
        }

        [Fact]
        public void GetConnectionType_ReturnsResult()
        {
            // Arrange
            // Act
            // Assert
        }

        [Fact]
        public void GetSpendInformation_ReturnsResult()
        {
            // Arrange
            // Act
            // Assert
        }
    }
}
