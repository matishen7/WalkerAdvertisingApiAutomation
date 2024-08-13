using System.Configuration;
using AutoFixture;
using RestSharp;
using WalkerAdvertisingApiAutomation.Requests;

namespace WalkerAdvertisingApiAutomation
{
    public class BaseTest
    {
        protected RestClient _client;
        protected string _baseUrl = "https://automationtestwaapi.azurewebsites.net";
        protected Fixture _fixture = new Fixture();

        [TestInitialize]
        public void Initialize()
        {
            _client = new RestClient(_baseUrl);
        }

        //[TestCleanup]
        //public async Task Cleanup()
        //{
        //    var request = new RestRequest("/api/Consumer", Method.Get);
        //    var response = await _client.ExecuteAsync<List<ContactInfo>>(request);
        //    foreach (var consumer in response.Data)
        //    {
        //        request = new RestRequest($"/api/Consumer/{consumer.Id}", Method.Delete);
        //        await _client.ExecuteAsync(request);
        //    }
        //}
    }
}