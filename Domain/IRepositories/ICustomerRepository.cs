using Domain.Entities;

namespace Domain.IRepositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetAsync(Guid id);
        Task<ICollection<Customer>> GetAllAsync();
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, string name, string email, string phone);
        Task CreateAsync(string name, string email, string phone);
    }
}