using Application.DTOs;
using Infrastructure.DataBase;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace Tests.IntegrationTests
{
    public class CustomerControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        public CustomerControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
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

        [Fact]
        public async Task PostCustomer_ReturnsSuccessStatusCode()
        {
            var dto = new CreateCustomerDto
            {
                Name = "Test Customer",
                Email = "test@test.com",
                PhoneNumber = "1234567890"
            };

            var content = JsonContent.Create(dto);

            var response = await _client.PostAsync("/Customer", content);

            response.EnsureSuccessStatusCode();
             
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var customer = await db.Customers.FirstOrDefaultAsync(c => c.Email == dto.Email);

            Assert.NotNull(customer);
            Assert.Equal(dto.Name, customer.Name);
        }


    }
}
