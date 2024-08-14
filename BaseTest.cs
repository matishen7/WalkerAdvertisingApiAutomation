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

        [TestCleanup]
        public async Task Cleanup()
        {
            var response = await Client.Instance.GetAllConsumers();
            foreach (var consumer in response.Data)
            {
                await Client.Instance.DeleteConsumer(consumer.Id);
            }
        }
    }
}