﻿using AutoFixture;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WalkerAdvertisingApiAutomation.Requests;

namespace WalkerAdvertisingApiAutomation
{
    [TestClass]
    public class GetAllConsumersTests : BaseTest
    {
        [TestMethod]
        public async Task GetAllConsumers_ShouldReturnSuccessAndConsumers()
        {
            // Act
            var response = await Client.Instance.GetAllConsumers();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.IsInstanceOfType(response.Data, typeof(List<ContactInfo>));
        }
    }
}
