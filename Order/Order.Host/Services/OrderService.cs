using AutoMapper;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Models.Dto;
using Order.Host.Repositories;

namespace Order.Host.Services
{
    public class OrderService : BaseDataService<ApplicationDbContext>, IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<OrderService> logger,
            IOrderRepository orderRepository, IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<int> AddOrderAsync(OrderDto orderDto)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var order = _mapper.Map<OrderEntity>(orderDto);
                order.CreatedBy = orderDto.UserName;
                order.CreatedDate = DateTime.UtcNow;
                _logger.LogInformation($"Created Order From OrderService: {order.Id}");

                return await _orderRepository.AddOrderAsync(order);
            });
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
               _logger.LogInformation($"Deleted Order From OrderService: {id}");
                return await _orderRepository.DeleteOrderAsync(id);
            });
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync(string userName)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _orderRepository.GetOrdersAsync(userName);
                var orders = result.Select(order =>
                {
                    var orderDto = _mapper.Map<OrderDto>(order);

                    orderDto.Items = order.Items.Select(item => _mapper.Map<ItemDto>(item)).ToList();

                    return orderDto;
                });

                return orders;
            });
        }

        public async Task<int> UpdateOrderAsync(int id, string userName, int totalProce, string givenName,
            string familyName, string emailAddress, string country, string state, string zipCode)
        {
            return await ExecuteSafeAsync(async () =>
            {

                _logger.LogInformation($"Updated Order From OrderService: {id}");

                return await _orderRepository.UpdateOrderAsync(id, userName, totalProce, givenName, familyName, emailAddress, country, state, zipCode);
            });
        }
    }
}
