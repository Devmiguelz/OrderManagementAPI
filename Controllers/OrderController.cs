using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.Application.Contracts;
using OrderManagementAPI.Application.DTO.Order;

namespace OrderManagementAPI.Controllers 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    public class OrderController : ControllerBase 
    { 
        private readonly IOrderService _orderService; 
        public OrderController(IOrderService orderService) 
        { 
            _orderService = orderService; 
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<OrderCreateDto>> GetOrderAsync()
        {
            var orders = await _orderService.GetOrderAsync();
            if (!orders.Any())
                return NoContent();

            return Ok(orders);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<bool>> SaveOrderAsync([FromBody] OrderCreateDto orderCreate)
        {
            var orderSaved = await _orderService.SaveOrderAsync(orderCreate);
            if (!orderSaved)
                return BadRequest();

            return Ok(orderSaved);
        }
    } 
} 
