using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetAsync(Guid id);
        Task SaveAsync(Guid customerId, string article, decimal totalAmount);
    }
}