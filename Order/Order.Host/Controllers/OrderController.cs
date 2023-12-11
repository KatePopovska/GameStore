using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Order.Host.Models.Dto;
using Order.Host.Models.Requests;
using Order.Host.Services;
using System.Net;

namespace Order.Host.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUserName([FromBody] string userName)
        {
            return Ok(await _orderService.GetOrdersAsync(userName));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CheckoutOrder(OrderDto orderDto)
        {
            return Ok(await _orderService.AddOrderAsync(orderDto));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> UpdateOrder(UpdateOrderRequest request)
        {
            return Ok(await _orderService.UpdateOrderAsync(request.Id, request.UserName, request.TotalPrice, request.GivenName, request.FamilyName, request.EmailAddress, request.Country, request.State, request.ZipCode));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> DeleteOrder([FromBody] int id)
        {
            return Ok(await _orderService.DeleteOrderAsync(id));
        }
    }
}
