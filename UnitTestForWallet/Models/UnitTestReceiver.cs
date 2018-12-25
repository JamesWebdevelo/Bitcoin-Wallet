using HBitcoin.KeyManagement;
using BusinessLayer.Models;
using BusinessLayer.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTestForWallet.Models
{
    public class UnitTestReceiver
    {
        [Fact]
        public void GetPublicAddresses_ReturnAddressesIfHttpCommunicationWasChoosen()
        {
            // Arrange
            string password = "admin";
            Config.ConnectionType = ConnectionType.Http;
            //Config.Save();

            // Act
            var actual = Receiver.GetPublicAddresses(password);

            // Assert
            Assert.True(actual.Count == 7);
        }

        [Fact]
        public void GetPublicAddresses_HandleExpectionWhenFullNodeCommunicationWasChoosen()
        {
            // Arrange
            string password = "admin";
            Config.ConnectionType = ConnectionType.FullNode;

            // Act / Assert
            Assert.Throws<NotImplementedException>(() => Receiver.GetPublicAddresses(password));
        }
    }
}
