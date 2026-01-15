using Domain.Entities;

namespace Domain.IRepositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetAsync(Guid id);
        Task SaveAsync(string name, string email, string phone);
    }
}