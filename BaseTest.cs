using System.Configuration;
using AutoFixture;
using RestSharp;

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
    }
}