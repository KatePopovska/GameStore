using Basket.Host.Entities;
using Microsoft.Extensions.Caching.Distributed;

using Newtonsoft.Json;

namespace Basket.Host.Services
{
    public class BasketService : IBasketService
    {
        private readonly ILogger<BasketService> _logger;
        private readonly IDistributedCache _redisCache;

        public BasketService(IDistributedCache redisCache, ILogger<BasketService> logger)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
            _logger = logger;
        }

        public async Task<ShoppingCart> AddToBasket(string userName, ShoppingCartItem item)
        {
            var existingBasket = await GetBasket(userName) ?? new ShoppingCart { UserName = userName, Items = new List<ShoppingCartItem>() };

            _logger.LogInformation($"Log From BasketService AddToBAsket: {JsonConvert.SerializeObject(existingBasket)}");
            var existingItem = existingBasket.Items.FirstOrDefault(existingItem => existingItem.ProductId == item.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                existingBasket.Items.Add(item);
            }
            await UpdateBasket(existingBasket);

            _logger.LogInformation($"Existing Basket After executing UpdateBasket method: {JsonConvert.SerializeObject(existingBasket)}");

            return existingBasket;
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }

        public async Task<bool> DeleteBasket(string userName)
        {
            try
            {
                await _redisCache.RemoveAsync(userName);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
            }
            return false;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);

            _logger.LogInformation($"Log From BasketService GetBasket: {JsonConvert.SerializeObject(basket)}");

            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);

        }

        public async Task<ShoppingCart> DeleteItem(string userName, int productId)
        {
            var existingBasket = await GetBasket(userName);
            if (existingBasket == null)
            {
                return new ShoppingCart();
            }

            var itemToDelete = existingBasket.Items.FirstOrDefault(item => item.ProductId == productId);
            if (itemToDelete == null)
            {
                return new ShoppingCart();
            }
            existingBasket.Items.Remove(itemToDelete);

            await UpdateBasket(existingBasket);
            return existingBasket;
        }
    }
}
