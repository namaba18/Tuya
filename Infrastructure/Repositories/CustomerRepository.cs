using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }
                
        public async Task<ICollection<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetAsync(Guid id)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            Customer? customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Customer not found");
            if (_context.Orders.Any(o => o.Customer == customer))
            {
                throw new Exception("The customer has an associated invoice, therefore it cannot be deleted.");
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, string name, string email, string phone)
        {
            Customer? customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Customer not found");
            customer.Name = name;
            customer.Email = email;
            customer.PhoneNumber = phone;
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(string name, string email, string phone)
        {
            var customer = new Customer(name, email, phone);
            
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
    }
}