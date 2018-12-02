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

        [Theory]
        [InlineData("")]
        [InlineData("-1")]
        [InlineData("FailNet")]
        public void GetNetwork_ExceptionIfWrongOrEmptyNetwork(string expected)
        {
            // Act & Assert
            Assert.Throws<Exception>(() => Config.GetNetwork(expected));
        }


        [Theory]
        [InlineData("FullNode")]
        [InlineData("Http")]
        public void GetConnectionType_ReturnsCorrectConnectionType(string expected)
        {
            // Act
            var actual = Config.GetConnectionType(expected);
            // Assert
            Assert.Equal(expected, actual.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData("&+?=9387&-.;,sHDb2vV+")]
        [InlineData("Https")]
        public void GetConnectionType_ExceptionIfWrongOrEmptyConnectionType(string expected)
        {
            // Act & Assert
            Assert.Throws<Exception>(() => Config.GetConnectionType(expected));
        }

        [Theory]
        [InlineData("False")]
        [InlineData("True")]
        public void GetSpendInformation_ReturnsCorrectSpendInformation(string expected)
        {
            // Act
            bool actual = Config.GetSpendInformation(expected);
            
            // Assert
            Assert.Equal(expected, actual.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData("-1")]
        [InlineData("0")]
        public void GetSpendInformation_ExceptionIfWrongOrEmptySpend(string expected)
        {
            // Act & Assert
            Assert.Throws<Exception>(() => Config.GetSpendInformation(expected));
        }
    }
}
