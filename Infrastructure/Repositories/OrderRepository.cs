using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetAsync(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task CreateAsync(Guid customerId, string article, decimal totalAmount)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == customerId) ?? throw new Exception("Customer not found");

            if (totalAmount <= 0)
            {
                throw new Exception("Total amount must be greater than zero");
            }

            var order = new Order(article, totalAmount, customer);            

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, string article, decimal totalAmount)
        {
            Order? order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Order not found");
            if (totalAmount <= 0)
            {
                throw new Exception("Total amount must be greater than zero");
            }
            order.Article = article;
            order.TotalAmount = totalAmount;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Order? order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Order not found");
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}