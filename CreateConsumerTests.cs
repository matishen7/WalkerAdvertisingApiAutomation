using AutoFixture;
using RestSharp;
using System.Net;
using WalkerAdvertisingApiAutomation.Requests;

namespace WalkerAdvertisingApiAutomation
{
    [TestClass]
    public class CreateConsumerTests : BaseTest
    {

        [TestMethod]
        public async Task CreateConsumer_ShouldReturnSuccessAndCreatedConsumer()
        {
            // Arrange
            var newConsumer = new ContactInfo
            {
                FirstName = "John",
                LastName = "Doe",
                Phone = "123-456-7890",
                Email = "john.doe@example.com",
                State = "CA",
                Reason = "New Registration"
            };

            // Act
            var response = await Client.Instance.CreateConsumer(newConsumer);

            // Assert
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual(newConsumer.FirstName, response.Data.FirstName);
            Assert.AreEqual(newConsumer.LastName, response.Data.LastName);
            Assert.AreEqual(newConsumer.Email, response.Data.Email);
            Assert.AreEqual(newConsumer.Phone, response.Data.Phone); 
            Assert.AreEqual(newConsumer.State, response.Data.State); 
            Assert.AreEqual(newConsumer.Reason, response.Data.Reason);
        }

        [TestMethod]
        public async Task CreateConsumer_ShouldReturnSuccessAndCreatedConsumer_Dynamic()
        {
            var newConsumer = _fixture.Create<ContactInfo>();

            var response = await Client.Instance.CreateConsumer(newConsumer);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual(newConsumer.FirstName, response.Data.FirstName);
            Assert.AreEqual(newConsumer.LastName, response.Data.LastName);
            Assert.AreEqual(newConsumer.Email, response.Data.Email);
            Assert.AreEqual(newConsumer.Phone, response.Data.Phone);
            Assert.AreEqual(newConsumer.State, response.Data.State);
            Assert.AreEqual(newConsumer.Reason, response.Data.Reason);
        }
    }
}