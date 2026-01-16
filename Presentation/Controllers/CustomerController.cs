using Application.DTOs;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet(Name = "GetCustomers")]
        public async Task<List<CustomerDto>> Get()
        {
            try
            {
                var response = await _customerService.GetAllAsync();
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching customers.");
                return new List<CustomerDto>();
            }
        }

        [HttpPost(Name = "CreateCustomer")]
        public async Task<IActionResult> Post([FromBody] CreateCustomerDto dto)
        {
            _logger.LogInformation("CreateCustomer endpoint called.");
            try
            {
                await _customerService.CreateAsync(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        [HttpDelete(Name = "DeleteCustomer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("DeleteCustomer endpoint called.");
            try
            {
                await _customerService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        [HttpPut(Name = "UpdateCustomer")]
        public async Task<IActionResult> Put([FromBody] CustomerDto dto)
        {
            _logger.LogInformation("UpdateCustomer endpoint called.");
            try
            {
                await _customerService.UpdateAsync(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }
    }
}