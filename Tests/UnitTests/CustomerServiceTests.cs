using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Infrastructure.DataBase;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.UnitTests
{
    public class CustomerServiceTests
    {
        [Fact]
        public async Task CanCreateCustomer()
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

        [Fact]
        public async Task CanGetAllCustomers()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("OrdersDb_GetAll")
            .Options;

            using var context = new AppDbContext(options);

            await context.Customers.AddAsync(new Customer("customer 1", "correo1", ""));
            await context.Customers.AddAsync(new Customer("customer 2", "correo2", ""));
            await context.SaveChangesAsync();

            var repo = new CustomerRepository(context);
            var customerService = new CustomerService(repo);

            List<CustomerDto> customers = await customerService.GetAllAsync();

            Assert.Equal(2, customers.Count);
        }

        [Fact]
        public async Task CanUpdateCustomer()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("OrdersDb_Update")
            .Options;

            using var context = new AppDbContext(options);
            var customer = new Customer("customer 1", "correo1", "");
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();

            var repo = new CustomerRepository(context);
            var customerService = new CustomerService(repo);

            CustomerDto dto = new CustomerDto
            {
                Id = customer.Id,
                Name = "Updated Name",
                Email = "correo2"
            };
            await customerService.UpdateAsync(dto);

            CustomerDto customerDto = await customerService.GetAsync(customer.Id) ?? new CustomerDto();

            Assert.Equal("Updated Name", customerDto.Name);
            Assert.Equal("correo2", customerDto.Email);
        }

        [Fact]
        public async Task CanDeleteCustomer()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("OrdersDb_Delete")
            .Options;

            using var context = new AppDbContext(options);
            var customer = new Customer("customer 1", "correo1", "");
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();

            var repo = new CustomerRepository(context);
            var customerService = new CustomerService(repo);

            await repo.DeleteAsync(customer.Id);
            Assert.Equal(0, await context.Customers.CountAsync());
        }
    }
}