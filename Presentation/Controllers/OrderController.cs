using Application.DTOs;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpGet(Name = "GetOrder")]
        public async Task<OrderDto?> Get(Guid id)
        {
            var response = await _orderService.GetOrderAsync(id);
            if (response == null)
            {
                _logger.LogWarning("Order with ID {OrderId} not found.", id);
                Response.StatusCode = 404;
                return null;
            }
            return response;

        }

        [HttpPost(Name = "CreateOrder")]
        public async Task<IActionResult> Post([FromBody] CreateOrderDto createOrderDto)
        {
            _logger.LogInformation("CreateOrder endpoint called.");
            try
            {
                await _orderService.CreateOrderAsync(createOrderDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }
    }
}