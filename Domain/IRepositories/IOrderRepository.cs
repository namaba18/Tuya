using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetAsync(Guid id);
        Task<List<Order>> GetAllAsync();
        Task CreateAsync(Guid customerId, string article, decimal totalAmount);
        Task UpdateAsync(Guid id, string article, decimal totalAmount);
        Task DeleteAsync(Guid id);
    }
}