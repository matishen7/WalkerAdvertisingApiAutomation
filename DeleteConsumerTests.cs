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
            var id = await CreateConsumer();

            // Act
            var response = await Client.Instance.DeleteConsumer(id);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task DeleteConsumer_ShouldReturnSuccess_And_Validate()
        {
            var id = await CreateConsumer();

            var response = await Client.Instance.DeleteConsumer(id);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var allCustomersResponse = await Client.Instance.GetAllConsumers();
            Assert.AreEqual(HttpStatusCode.OK, allCustomersResponse.StatusCode);
            Assert.IsNotNull(allCustomersResponse.Data);
            Assert.IsInstanceOfType(allCustomersResponse.Data, typeof(List<ContactInfo>));
            Assert.IsTrue(allCustomersResponse.Data.Find(x => x.Id == id) == null, $"There should be no customer found with id {id}");
        }

        [TestMethod]
        public async Task DeleteConsumer_ShouldReturnSuccess_And_GetById()
        {
            var id = await CreateConsumer();

            var response = await Client.Instance.DeleteConsumer(id);

            //Validate if consumer is deleted
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);            
            var getResponse = await Client.Instance.GetConsumerById(id);
            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
        }

        private async Task<int> CreateConsumer()
        {
            var newConsumer = _fixture.Create<ContactInfo>();
            var response = await Client.Instance.CreateConsumer(newConsumer);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            return response.Data.Id;
        }
    }
}
