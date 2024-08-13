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
    public class DeleteConsumerTests : BaseTest
    {
        [TestMethod]
        public async Task DeleteConsumer_ShouldReturnSuccess()
        {
            // Arrange - Create a consumer first
            var id = await CreateConsumerAsync();
            var request = new RestRequest($"/api/Consumer/{id}", Method.Delete);

            // Act
            var response = await _client.ExecuteAsync(request);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task DeleteConsumer_ShouldReturnSuccess_And_Validate()
        {
            var id = await CreateConsumerAsync();
            var request = new RestRequest($"/api/Consumer/{id}", Method.Delete);

            var response = await _client.ExecuteAsync(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            request = new RestRequest("/api/Consumer", Method.Get);
            var allCustomersResponse = await _client.ExecuteAsync<List<ContactInfo>>(request);
            Assert.AreEqual(HttpStatusCode.OK, allCustomersResponse.StatusCode);
            Assert.IsNotNull(allCustomersResponse.Data);
            Assert.IsInstanceOfType(allCustomersResponse.Data, typeof(List<ContactInfo>));
            Assert.IsTrue(allCustomersResponse.Data.Find(x => x.Id == id) == null, $"There should be no customer found with id {id}");
        }

        private async Task<int> CreateConsumerAsync()
        {
            var newConsumer = _fixture.Create<ContactInfo>();
            var request = new RestRequest($"/api/Consumer", Method.Post);
            request.AddJsonBody(newConsumer);
            var response = await _client.ExecuteAsync<ContactInfo>(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            return response.Data.Id;
        }
    }
}
