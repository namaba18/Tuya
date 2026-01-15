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

        public async Task<OrderDto?> GetAsync(Guid id)
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

        public async Task<List<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            List<OrderDto> orderDtos = new List<OrderDto>();
            foreach (var order in orders)
            {
                orderDtos.Add(new OrderDto
                {
                    Id = order.Id,
                    Article = order.Article,
                    TotalAmount = order.TotalAmount,
                    CustomerName = order.Customer.Name,
                    CustomerEmail = order.Customer.Email,
                    CustomerPhoneNumber = order.Customer.PhoneNumber
                });
            }
            return orderDtos;             
        }

        public async Task CreateAsync(CreateOrderDto dto)
        {
            await _orderRepository.CreateAsync(dto.CustomerId, dto.Article, dto.TotalAmount);
        }
    }
}