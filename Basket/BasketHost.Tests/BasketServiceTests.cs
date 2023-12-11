using Basket.Host.Entities;
using Basket.Host.Services;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.Common.Exceptions;
using Moq;
using Newtonsoft.Json;

namespace BasketHost.Tests
{
    public class BasketServiceTests
    {
        private readonly Mock<ILogger<BasketService>> _logger;
        private readonly Mock<IDistributedCache> _redisCache;
        private readonly IBasketService _basketService;
        private readonly string _userName = "testUser";
        private readonly ShoppingCartItem _item = new ShoppingCartItem()
        {
            Price = 100,
            ProductId = 1,
            ProductName = "n",
            Quantity = 1,
        };
        private readonly ShoppingCart testResult = new ShoppingCart();


        public BasketServiceTests() 
        { 
            _logger = new Mock<ILogger<BasketService>>();
            _redisCache = new Mock<IDistributedCache>();
            _basketService = new BasketService(_redisCache.Object, _logger.Object);
            testResult.UserName = _userName;
            testResult.Items = new List<ShoppingCartItem>() { _item };
        }

        [Fact]
        public async Task AddToBasketSuccess()
        {
            var mockBasketService = new Mock<IBasketService>();
            mockBasketService.Setup(service => service.GetBasket(It.IsAny<string>())).ReturnsAsync(testResult);
            mockBasketService.Setup(service => service.UpdateBasket(It.IsAny<ShoppingCart>())).ReturnsAsync(testResult);

            var result = await mockBasketService.Object.AddToBasket(_userName, _item);

            result.Should().NotBeNull();  // Ensure that the result is not null
            result.Should().BeEquivalentTo(testResult);
        }

        [Fact]
        public async Task AddToBasketFailed()
        {;
            var result = await _basketService.AddToBasket("testfailed", _item);

            result.Should().NotBe(testResult);

        }

        [Fact]
        public async Task UpdateBasketSuccess()
        {
           
        }

    }
}