using AutoMapper;
using Basket.Host.Entities;
using Basket.Host.Models;
using Basket.Host.Models.Requests;
using Basket.Host.Services;
using Infrastructure;
using Infrastructure.EventBusMessages.Common;
using Infrastructure.EventBusMessages.Events;
using Infrastructure.Identity;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Basket.Host.Controllers
{
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    [ApiController]
    public class BasketBffController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ILogger<BasketBffController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public BasketBffController(IBasketService basketRepository, ILogger<BasketBffController> logger,
            IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _basketService = basketRepository;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetAsync([FromBody] string userName)
        {
            var basket = await _basketService.GetBasket(userName);
            _logger.LogInformation($"Request from BasketBffController GetAsync: {JsonConvert.SerializeObject(basket)}");

            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> AddAsync(AddToBasketRequest request)
        {
            _logger.LogInformation($"Request from BasketBffController AddAsync: {request.UserName}, {JsonConvert.SerializeObject(request.Item)}");

            return Ok(await _basketService.AddToBasket(request.UserName, request.Item));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateAsync([FromBody] ShoppingCart basket)
        {
            return Ok(await _basketService.UpdateBasket(basket));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsync(string userName)
        {
            await _basketService.DeleteBasket(userName);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> DeleteItemAsync(DeleteItemRequest request)
        {
            return Ok(await _basketService.DeleteItem(request.UserName, request.ProductId));
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            _logger.LogInformation("BASKET CHECKOUT!!!");
            var basket = await _basketService.GetBasket(basketCheckout.UserName);
            if (basket == null)
            {
                return BadRequest();
            }

            var basket1 = new ShoppingCartCheckout()
            {
                UserName = basket.UserName,
                TotalPrice = basket.TotalPrice,
                Items = basket.Items.Select(item => _mapper.Map<Infrastructure.EventBusMessages.Common.ShoppingCartItem>(item)).ToList(),
            };
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            
            eventMessage.Items = basket1.Items;
            eventMessage.TotalPrice =  basket1.TotalPrice;
            eventMessage.UserName = basket1.UserName;
            await _publishEndpoint.Publish(eventMessage);

            await _basketService.DeleteBasket(basket.UserName);
            return Accepted();
        }
    }
}
