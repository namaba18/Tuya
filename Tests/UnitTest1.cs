using Application.DTOs;
using Application.Services;
using Infrastructure.DataBase;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task CreateCustomer()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("OrdersDb")
            .Options;

            using var context = new AppDbContext(options);
            var repo = new CustomerRepository(context);

            CreateCustomerDto dto = new CreateCustomerDto
            {
                Name = "John Doe",
                Email = "john@correo.com",
                PhoneNumber = "1234567890"
            };

            var customerSercice = new CustomerService(repo);

            await customerSercice.CreateAsync(dto);

            Assert.Equal(1, await context.Customers.CountAsync());
        }
    }
}