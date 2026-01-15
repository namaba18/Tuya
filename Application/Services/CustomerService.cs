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

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            List<CustomerDto> customerDtos = new();
            var customers = await _customerRepository.GetAllAsync();
            foreach (var customer in customers)
            {
                customerDtos.Add(new CustomerDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber
                });
            }
            return customerDtos;
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
            await _customerRepository.CreateAsync(dto.Name, dto.Email, dto.PhoneNumber);
        }

        public async Task UpdateAsync(CustomerDto dto)
        {
            await _customerRepository.UpdateAsync(dto.Id, dto.Name, dto.Email, dto.PhoneNumber);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _customerRepository.DeleteAsync(id);
        }
    }
}