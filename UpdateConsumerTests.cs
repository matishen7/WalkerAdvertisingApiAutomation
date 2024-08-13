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
    public class UpdateConsumerTests : BaseTest
    {
        [TestMethod]
        public async Task UpdateConsumer_ShouldReturnSuccess()
        {
            // Arrange
            var newConsumer = await CreateConsumerAsync();
            var request = new RestRequest("/api/Consumer", Method.Put);
            var updatedConsumer = new ContactInfo
            {
                Id = newConsumer.Id,
                FirstName = "Jane",
                LastName = "Smith",
                Phone = "987-654-3210",
                Email = "jane.smith@example.com",
                State = "NY",
                Reason = "Update Information"
            };
            request.AddJsonBody(updatedConsumer);

            // Act
            var response = await _client.ExecuteAsync(request);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

        [TestMethod]
        public async Task UpdateConsumer_ShouldReturnSuccess_Validate_ByGetById()
        {
            // Arrange
            var newConsumer = await CreateConsumerAsync();
            var request = new RestRequest("/api/Consumer", Method.Put);
            var updatedConsumer = new ContactInfo
            {
                Id = newConsumer.Id,
                FirstName = "Jane",
                LastName = "Smith",
                Phone = "987-654-3210",
                Email = "jane.smith@example.com",
                State = "NY",
                Reason = "Update Information"
            };
            request.AddJsonBody(updatedConsumer);

            // Act
            var response = await _client.ExecuteAsync(request);

            // Assert against Get consumer by Id
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            request = new RestRequest($"/api/Consumer/consumer/{newConsumer.Id}", Method.Get);
            var getResponse = await _client.ExecuteAsync<ContactInfo>(request);

            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.IsNotNull(getResponse.Data);
            Assert.IsInstanceOfType(getResponse.Data, typeof(ContactInfo));
            Assert.AreEqual(updatedConsumer.Id, getResponse.Data.Id);
            Assert.AreEqual(updatedConsumer.FirstName, getResponse.Data.FirstName);
            Assert.AreEqual(updatedConsumer.LastName, getResponse.Data.LastName);
            Assert.AreEqual(updatedConsumer.Email, getResponse.Data.Email);
            Assert.AreEqual(updatedConsumer.Phone, getResponse.Data.Phone);
            Assert.AreEqual(updatedConsumer.State, getResponse.Data.State);
            Assert.AreEqual(updatedConsumer.Reason, getResponse.Data.Reason);

        }

        [TestMethod]
        public async Task UpdateConsumer_ShouldReturnNotFound()
        {
            var request = new RestRequest("/api/Consumer", Method.Put);
            var updatedConsumer = new ContactInfo
            {
                Id = int.MaxValue,
                FirstName = "Jane",
                LastName = "Smith",
                Phone = "987-654-3210",
                Email = "jane.smith@example.com",
                State = "NY",
                Reason = "Update Information"
            };
            request.AddJsonBody(updatedConsumer);

            var response = await _client.ExecuteAsync(request);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            // this must be a bug, because it is returning 500 error for non existing consumer
            // expected return should be NotFound
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
