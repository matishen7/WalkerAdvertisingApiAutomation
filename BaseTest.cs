using System.Configuration;
using RestSharp;

namespace WalkerAdvertisingApiAutomation
{
    public class BaseTest
    {
        protected RestClient _client;
        protected string _baseUrl;

        [TestInitialize]
        public void Initialize()
        {
            string? v = ConfigurationManager.AppSettings["ApiBaseUrl"];
            _baseUrl = v;
        }
    }
}