using Application.DTOs;
using Application.IServices;
using Domain.IRepositories;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto?> GetAsync(Guid id)
        {
            var customer = await _customerRepository.GetAsync(id);
            if (customer != null)
            {
                return new CustomerDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber
                };
            }
            else
            {
                return null;
            }
        }

        public async Task CreateAsync(CreateCustomerDto dto)
        {
            await _customerRepository.SaveAsync(dto.Name, dto.Email, dto.PhoneNumber);
        }
    }
}