using Application.DTOs;

namespace Application.IServices
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetAsync(Guid id);
        Task CreateAsync(CreateCustomerDto dto);
    }
}