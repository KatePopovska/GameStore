using IdentityModel;
using Microsoft.Extensions.Options;
using MVC.Models.Request;
using MVC.Services.Interfaces;
using MVC.ViewModels.BasketViewModel;
using System.Security.Claims;

namespace MVC.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<BasketService> _logger;
        private readonly IOptions<AppSettings> _settings;

        public BasketService(IHttpClientService httpClientService,
            ILogger<BasketService> logger, IOptions<AppSettings> settings,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientService = httpClientService;
            _logger = logger;
            _settings = settings;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BasketModel> AddToBasket(BasketItemModel basket)
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(JwtClaimTypes.GivenName)?.Value;

            _logger.LogInformation($"UserName From AddToBasket method: {user}");

            var requestContent = new AddToBasketRequest()
            {
                UserName = user,
                Item = basket
            };
            var response = await _httpClientService
                .SendAsync<BasketModel, AddToBasketRequest>($"{_settings.Value.BasketUrl}/add", HttpMethod.Post, requestContent);
            return response;

        }

        public async Task CheckoutBasket(BasketCheckoutModel order)
        {
           await _httpClientService.SendAsync<Task, BasketCheckoutModel>($"{_settings.Value.BasketUrl}/Checkout/Checkout", HttpMethod.Post, order);
        }

        public async Task<BasketModel> DeleteItem(int productId)
        {
           var userName = _httpContextAccessor.HttpContext.User.FindFirst(JwtClaimTypes.GivenName)?.Value;
            if (userName == null )
            {
                return new BasketModel();
            }
            var requestContent = new DeleteItemRequest() { ProductId = productId, UserName = userName };
            var response = await _httpClientService
                .SendAsync<BasketModel, DeleteItemRequest>($"{_settings.Value.BasketUrl}/deleteItem", HttpMethod.Post, requestContent);
            return response;
        }

        public async Task<BasketModel> GetBasket()
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(JwtClaimTypes.GivenName)?.Value;
            if (userName == null)
            {
                return new BasketModel();
            }
            var response = await _httpClientService
                .SendAsync<BasketModel, string>($"{_settings.Value.BasketUrl}/get", HttpMethod.Post, userName);
            return response;
        }

        public async Task<BasketModel> UpdateBasket(BasketModel basket)
        {
            var response = await _httpClientService
                .SendAsync<BasketModel, BasketModel>($"{_settings.Value.BasketUrl}/update", HttpMethod.Post, basket);
            return response;
        }
    }
}
