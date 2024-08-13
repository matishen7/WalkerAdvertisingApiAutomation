using AutoFixture;
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
    public class GetCustomerByIdTests : BaseTest
    {
        [TestMethod]
        public async Task GetConsumerById_ShouldReturnSuccessAndConsumer()
        {
            // Arrange
            var newConsumer = await CreateConsumerAsync();
            var request = new RestRequest($"/api/Consumer/consumer/{newConsumer.Id}", Method.Get);

            // Act
            var response = await _client.ExecuteAsync<ContactInfo>(request);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.IsInstanceOfType(response.Data, typeof(ContactInfo));
            Assert.AreEqual(newConsumer.Id, response.Data.Id);
            Assert.AreEqual(newConsumer.FirstName, response.Data.FirstName);
            Assert.AreEqual(newConsumer.LastName, response.Data.LastName);
            Assert.AreEqual(newConsumer.Email, response.Data.Email);
            Assert.AreEqual(newConsumer.Phone, response.Data.Phone);
            Assert.AreEqual(newConsumer.State, response.Data.State);
            Assert.AreEqual(newConsumer.Reason, response.Data.Reason);
        }

        [TestMethod]
        public async Task GetConsumerById_ShouldReturnNotFound()
        {
            // Arrange
            var request = new RestRequest($"/api/Consumer/consumer/{int.MaxValue}", Method.Get);

            // Act
            var response = await _client.ExecuteAsync<ContactInfo>(request);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public async Task GetConsumerById_ShouldReturnNotFound2(int id)
        {
            // Arrange
            var request = new RestRequest($"/api/Consumer/consumer/{id}", Method.Get);

            // Act
            var response = await _client.ExecuteAsync<ContactInfo>(request);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        private async Task<ContactInfo> CreateConsumerAsync()
        {
            var newConsumer = _fixture.Create<ContactInfo>();
            var request = new RestRequest($"/api/Consumer", Method.Post);
            request.AddJsonBody(newConsumer);
            var response = await _client.ExecuteAsync<ContactInfo>(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            return response.Data;
        }
    }
}
