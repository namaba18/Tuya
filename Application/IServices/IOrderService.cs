using Application.DTOs;

namespace Application.IServices
{
    public interface IOrderService
    {
        Task<OrderDto?> GetOrderAsync(Guid id);
        Task CreateOrderAsync(CreateOrderDto dto);
    }
}