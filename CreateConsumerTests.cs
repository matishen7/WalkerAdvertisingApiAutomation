using Microsoft.Extensions.Configuration;
using RestSharp;

namespace WalkerAdvertisingApiAutomation
{
    [TestClass]
    public class CreateConsumerTests
    {
        private RestClient _client;
        private string _baseUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            // Load configuration from appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Retrieve the base URL from the configuration file
            _baseUrl = config["ApiBaseUrl"];

            // Initialize the RestClient with the base URL
            _client = new RestClient(_baseUrl);
        }
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