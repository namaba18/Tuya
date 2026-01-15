using Application.DTOs;

namespace Application.IServices
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetAsync(Guid id);
        Task<List<CustomerDto>> GetAllAsync();
        Task CreateAsync(CreateCustomerDto dto);
        Task UpdateAsync(CustomerDto dto);
        Task DeleteAsync(Guid id);
    }
}