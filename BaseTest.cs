using System.Configuration;
using AutoFixture;
using RestSharp;
using WalkerAdvertisingApiAutomation.Requests;

namespace WalkerAdvertisingApiAutomation
{
    public class BaseTest
    {
        protected Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
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