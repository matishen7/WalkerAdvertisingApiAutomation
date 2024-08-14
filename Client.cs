using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkerAdvertisingApiAutomation.Requests;

namespace WalkerAdvertisingApiAutomation
{
    public class Client
    {
        private static readonly Client _instance = new Client();
        private RestClient _client;
        private string _baseUrl = "https://automationtestwaapi.azurewebsites.net";
        private Client()
        {
            _client = new RestClient(_baseUrl);
        }

        public static Client Instance { get { return _instance; } }
        public async Task<RestResponse<ContactInfo>> CreateConsumer(ContactInfo newConsumer)
        {
            RestRequest request = new RestRequest("/api/Consumer", Method.Post);
            request.AddJsonBody(newConsumer);
            return await _client.ExecuteAsync<ContactInfo>(request);
        }

        public async Task<RestResponse<List<ContactInfo>>> GetAllConsumers()
        {
            var request = new RestRequest("/api/Consumer", Method.Get);
            return await _client.ExecuteAsync<List<ContactInfo>>(request);
        }
        public async Task<RestResponse<ContactInfo>> GetConsumerById(int id) 
        {
            var request = new RestRequest($"/api/Consumer/consumer/{id}", Method.Get);
            return await _client.ExecuteAsync<ContactInfo>(request);
        }

        public async Task<RestResponse> DeleteConsumer(int id)
        {
            var request = new RestRequest($"/api/Consumer/{id}", Method.Delete);
            return await _client.ExecuteAsync(request);
        }

        public async Task<RestResponse> UpdateConsumer(ContactInfo newConsumer)
        {
            var request = new RestRequest("/api/Consumer", Method.Put);
            request.AddJsonBody(newConsumer);
            return await _client.ExecuteAsync(request);
        }
    }
}
