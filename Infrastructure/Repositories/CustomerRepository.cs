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

        public async Task<Customer?> GetAsync(Guid id)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync(string name, string email, string phone)
        {
            var customer = new Customer
            {
                Name = name,
                Email = email,
                PhoneNumber = phone
            };
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
    }
}