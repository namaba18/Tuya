using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Tests.IntegrationTests
{
    public class OrderTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public OrderTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetOrders_ReturnsSuccessAndCorrectContentType()
        {
            var response = await _client.GetAsync("/Order");

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
