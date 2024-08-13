using RestSharp;
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
            var request = new RestRequest("/api/Consumer", Method.Post);
            var newConsumer = new ContactInfo
            {
                FirstName = "John",
                LastName = "Doe",
                Phone = "123-456-7890",
                Email = "john.doe@example.com",
                State = "CA",
                Reason = "New Registration"
            };
            request.AddJsonBody(newConsumer);

            // Act
            var response = await _client.ExecuteAsync<ContactInfo>(request);

            // Assert
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual(newConsumer.FirstName, response.Data.FirstName);
            Assert.AreEqual(newConsumer.Email, response.Data.Email);
        }
    }
}