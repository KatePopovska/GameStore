using AutoMapper;
using Infrastructure.EventBusMessages.Events;
using MassTransit;
using Newtonsoft.Json;
using Order.Host.Models.Dto;
using Order.Host.Services;

namespace Order.Host.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly ILogger<BasketCheckoutConsumer> _logger;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public BasketCheckoutConsumer(ILogger<BasketCheckoutConsumer> logger, IOrderService orderService, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var orderDto = _mapper.Map<OrderDto>(context.Message);
            var items = context.Message.Items.Select(item => _mapper.Map<ItemDto>(item)).ToList();
            orderDto.Items = items;
            var result = await _orderService.AddOrderAsync(orderDto);

            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            _logger.LogInformation($"OrderCreated From BasketCheckoutConsumer message: {jsonMessage} result: {result}");
        }
    }
}
