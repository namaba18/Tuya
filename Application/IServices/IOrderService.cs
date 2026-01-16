using Application.DTOs;

namespace Application.IServices
{
    public interface IOrderService
    {
        Task<OrderDto?> GetAsync(Guid id);
        Task<List<OrderDto>> GetAllAsync();
        Task CreateAsync(CreateOrderDto dto);
        Task UpdateAsync(OrderDto dto);
        Task DeleteAsync(Guid id);
    }
}