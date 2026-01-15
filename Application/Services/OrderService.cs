using Application.DTOs;
using Application.IServices;
using Domain.IRepositories;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto?> GetOrderAsync(Guid id)
        {
            var order = await _orderRepository.GetAsync(id);
            if (order != null)
            {
                return new OrderDto
                {
                    Id = order.Id,
                    Article = order.Article,
                    TotalAmount = order.TotalAmount,
                    CustomerName = order.Customer.Name,
                    CustomerEmail = order.Customer.Email,
                    CustomerPhoneNumber = order.Customer.PhoneNumber
                };
            }
            else
            {
                return null;
            }
        }

        public async Task CreateOrderAsync(CreateOrderDto dto)
        {
            await _orderRepository.SaveAsync(dto.CustomerId, dto.Article, dto.TotalAmount);
        }
    }
}