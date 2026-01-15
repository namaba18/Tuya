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

        [HttpGet(Name = "GetCustomer")]
        public async Task<CustomerDto?> Get(Guid id)
        {
            var response = await _customerService.GetAsync(id);
            if (response == null)
            {
                _logger.LogWarning("Customer with ID {CustomerId} not found.", id);
                Response.StatusCode = 404;
                return null;
            }
            return response;

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
    }
}