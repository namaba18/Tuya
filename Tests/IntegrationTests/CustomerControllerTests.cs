using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Tests.IntegrationTests
{
    public class CustomerControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public CustomerControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetCustomers_ReturnsSuccessAndCorrectContentType()
        {
            var response = await _client.GetAsync("/Customer");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
