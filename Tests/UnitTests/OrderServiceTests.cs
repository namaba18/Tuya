using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Infrastructure.DataBase;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.UnitTests
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task CanCreateOrder()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("OrdersDb_CreateOrder")
            .Options;
            using var context = new AppDbContext(options);

            await context.Customers.AddAsync(new Customer("customer 2", "correo2", ""));
            await context.SaveChangesAsync();

            var customer = await context.Customers.FirstAsync();

            var repo = new OrderRepository(context);
            CreateOrderDto dto = new CreateOrderDto
            {
                CustomerId = customer.Id,
                Article = "Sample Article",
                TotalAmount = 99.99m
            };
            var orderService = new OrderService(repo);
            await orderService.CreateAsync(dto);
            Assert.Equal(1, await context.Orders.CountAsync());
        }

        [Fact]
        public async Task CanGetAllOrder()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("OrdersDb_GetAllOrder")
            .Options;
            using var context = new AppDbContext(options);
            await context.Customers.AddAsync(new Customer("customer 2", "correo2", ""));
            await context.SaveChangesAsync();

            var customer = await context.Customers.FirstAsync();

            await context.Orders.AddAsync(new Order("Sample Article", 99.99m, customer));
            await context.SaveChangesAsync();
            var repo = new OrderRepository(context);
            CreateOrderDto dto = new CreateOrderDto
            {
                CustomerId = Guid.NewGuid(),
                Article = "Sample Article",
                TotalAmount = 99.99m
            };
            var orderService = new OrderService(repo);
            var orders = await orderService.GetAllAsync();
            Assert.Single(orders);
        }

        [Fact]
        public async Task CanUpdateOrder()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("OrdersDb_UpdateOrder")
            .Options;
            using var context = new AppDbContext(options);
            await context.Customers.AddAsync(new Customer("customer 2", "correo2", ""));
            await context.SaveChangesAsync();
            var customer = await context.Customers.FirstAsync();
            var order = new Order("Sample Article", 99.99m, customer);
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            var repo = new OrderRepository(context);
            var orderService = new OrderService(repo);
            OrderDto dto = new OrderDto
            {
                Id = order.Id,
                Article = "Updated Article",
                TotalAmount = 199.99m,
            };
            await orderService.UpdateAsync(dto);
            var updatedOrder = await context.Orders.FirstAsync();
            Assert.Equal("Updated Article", updatedOrder.Article);
            Assert.Equal(199.99m, updatedOrder.TotalAmount);
        }

        [Fact]
        public async Task CanDeleteOrder()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("OrdersDb_DeleteOrder")
            .Options;

            using var context = new AppDbContext(options);
            await context.Customers.AddAsync(new Customer("customer 2", "correo2", ""));
            await context.SaveChangesAsync();

            var customer = await context.Customers.FirstAsync();
            var order = new Order("Sample Article", 99.99m, customer);
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();

            var repo = new OrderRepository(context);
            var orderService = new OrderService(repo);
            await repo.DeleteAsync(order.Id);
            Assert.Equal(0, await context.Orders.CountAsync());
        }
    }
}
