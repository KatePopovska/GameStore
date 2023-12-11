using IdentityModel;
using Microsoft.Extensions.Options;
using MVC.Services.Interfaces;
using MVC.ViewModels.OrderViewModels;

namespace MVC.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientService _httpClientService;
        private readonly IOptions<AppSettings> _settings;

        public OrderService(ILogger<OrderService> logger, IHttpContextAccessor httpContextAccessor, IHttpClientService httpClientService, IOptions<AppSettings> settings)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _httpClientService = httpClientService;
            _settings = settings;
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName()
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(JwtClaimTypes.GivenName)?.Value;

            _logger.LogInformation($"UserName From OrderService GetOrdersByUserName method: {user}");

            var response = await _httpClientService
                .SendAsync<IEnumerable<OrderResponseModel>, string>($"{_settings.Value.OrderUrl}/GetOrdersByUserName", HttpMethod.Post, user);
            return response;
        }
    }
}
