using AutoMapper;
using FluentAssertions;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Models.Dto;
using Order.Host.Repositories;
using Order.Host.Services;

namespace OrderHost.Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<ILogger<OrderService>> _logger;
        private readonly IOrderService _orderService;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<IOrderRepository> _orderRepository;

        public OrderServiceTests()
        { 
            _logger = new Mock<ILogger<OrderService>>();
            _mapper = new Mock<IMapper>();
            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dbContextTransaction.Object);
            _orderRepository = new Mock<IOrderRepository>();
            _orderService = new OrderService(_dbContextWrapper.Object, _logger.Object,
                _orderRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task AddOrderAsyncSuccess()
        {
            var item = new ItemDto()
            {
                Id = 1,
                Quantity = 1,
                ProductName = "test",
                Price = 100
            };

            var testResult = 1;
            var testOrder = new OrderDto()
            {
                TotalPrice = 100,
                Country = "test",
                EmailAddress = "test",
                FamilyName = "test",
                GivenName = "test",
                State = "test",
                ZipCode = "test",
                UserName = "test",
            };
            var orderEntity = new OrderEntity()
            {
                TotalPrice = 100,
                Country = "test",
                EmailAddress = "test",
                FamilyName = "test",
                GivenName = "test",
                State = "test",
                ZipCode = "test",
                UserName = "test",
                CreatedBy = "test",
                CreatedDate= DateTime.UtcNow,
                Id = 2,
                Items = new List<Item>() { new Item() {
                Id = 1,
                Quantity = 1,
                ProductName = "test",
                Price = 100 } },
                ItemsId= 2,
            };

            _mapper.Setup((mapper => mapper.Map<OrderEntity>(testOrder))).Returns(orderEntity);
            _orderRepository.Setup(r => r.AddOrderAsync(It.IsAny<OrderEntity>())).ReturnsAsync(testResult);

            var result = await _orderService.AddOrderAsync(testOrder);

            result.Should().Be(testResult);


        }

        [Fact]
        public async Task AddOrderAsyncFailed()
        {
            var item = new ItemDto()
            {
                Id = 1,
                Quantity = 1,
                ProductName = "test",
                Price = 100
            };

            var testResult = 1;
            var testOrder = new OrderDto()
            {
                TotalPrice = 100,
                Country = "test",
                EmailAddress = "test",
                FamilyName = "test",
                GivenName = "test",
                State = "test",
                ZipCode = "test",
                UserName = "test",
            };
            var orderEntity = new OrderEntity()
            {
                TotalPrice = 100,
                Country = "test",
                EmailAddress = "test",
                FamilyName = "test",
                GivenName = "test",
                State = "test",
                ZipCode = "test",
                UserName = "test",
                CreatedBy = "test",
                CreatedDate = DateTime.UtcNow,
                Id = 2,
                Items = new List<Item>() { new Item() {
                Id = 1,
                Quantity = 1,
                ProductName = "test",
                Price = 100 } },
                ItemsId = 2,
            };

            _mapper.Setup((mapper => mapper.Map<OrderEntity>(testOrder))).Returns(orderEntity);
            _orderRepository.Setup(r => r.AddOrderAsync(It.IsAny<OrderEntity>())).ThrowsAsync(new Exception());

            Func<Task> result = async () => await _orderService.AddOrderAsync(testOrder);

            result.Should().ThrowAsync<Exception>();

        }

        [Fact]
        public async Task DeleteOrderAsyncSuccess()
        {
            int id = 1;
            _orderRepository.Setup(r => r.DeleteOrderAsync(It.IsAny<int>())).ReturnsAsync(true);

            var result = await _orderService.DeleteOrderAsync(id);

            result.Should().Be(true);
        }

        [Fact]
        public async Task DeleteOrderAsyncFailed()
        {
            int id = 1;
            _orderRepository.Setup(r => r.DeleteOrderAsync(It.IsAny<int>())).ThrowsAsync(new KeyNotFoundException());

            Func<Task> result = async () => await _orderService.DeleteOrderAsync(id);

            result.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task GetOrdersAsyncSuccess()
        {
            var userName = "test";
            var itemDto = new ItemDto()
            {
                Id = 1,
                Quantity = 1,
                ProductName = "test",
                Price = 100
            };
            var orderEntity = new OrderEntity()
            {
                TotalPrice = 100,
                Country = "test",
                EmailAddress = "test",
                FamilyName = "test",
                GivenName = "test",
                State = "test",
                ZipCode = "test",
                UserName = "test",
                CreatedBy = "test",
                CreatedDate = DateTime.UtcNow,
                Id = 2,
                Items = new List<Item>() { new Item() {
                Id = 1,
                Quantity = 1,
                ProductName = "test",
                Price = 100 } },
                ItemsId = 2,
            };
            var testOrder = new OrderDto()
            {
                TotalPrice = 100,
                Country = "test",
                EmailAddress = "test",
                FamilyName = "test",
                GivenName = "test",
                State = "test",
                ZipCode = "test",
                UserName = "test",
            };
            var orderList = new List<OrderEntity>() { orderEntity };
            var testResult = new List<OrderDto>() { testOrder };
            _mapper.Setup(mapper => mapper.Map<OrderDto>(It.IsAny<OrderEntity>())).Returns(testOrder);
            _mapper.Setup(mapper => mapper.Map<ItemDto>(It.IsAny<Item>())).Returns(itemDto);

            _orderRepository.Setup(s => s.GetOrdersAsync(It.IsAny<string>())).ReturnsAsync(orderList);

            var result = await _orderService.GetOrdersAsync(userName);

            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.First().Should().BeEquivalentTo(testResult.First(), options => options.ExcludingMissingMembers());
        }
        [Fact]
        public async Task GetOrdersAsyncFailed()
        {
            var userName = "test";
            var itemDto = new ItemDto()
            {
                Id = 1,
                Quantity = 1,
                ProductName = "test",
                Price = 100
            };
            var orderEntity = new OrderEntity()
            {
                TotalPrice = 100,
                Country = "test",
                EmailAddress = "test",
                FamilyName = "test",
                GivenName = "test",
                State = "test",
                ZipCode = "test",
                UserName = "test",
                CreatedBy = "test",
                CreatedDate = DateTime.UtcNow,
                Id = 2,
                Items = new List<Item>() { new Item() {
                Id = 1,
                Quantity = 1,
                ProductName = "test",
                Price = 100 } },
                ItemsId = 2,
            };
            var testOrder = new OrderDto()
            {
                TotalPrice = 100,
                Country = "test",
                EmailAddress = "test",
                FamilyName = "test",
                GivenName = "test",
                State = "test",
                ZipCode = "test",
                UserName = "test",
            };
            var orderList = new List<OrderEntity>() { orderEntity };
            var testResult = new List<OrderDto>() { testOrder };
            _mapper.Setup(mapper => mapper.Map<OrderDto>(It.IsAny<OrderEntity>())).Returns(testOrder);
            _mapper.Setup(mapper => mapper.Map<ItemDto>(It.IsAny<Item>())).Returns(itemDto);

            _orderRepository.Setup(s => s.GetOrdersAsync(It.IsAny<string>())).ThrowsAsync(new Exception());

            Func<Task> result = async () =>  await _orderService.GetOrdersAsync(userName);

            result.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task UpdateOrderAsyncSuccess()
        {
            var orderDto = new OrderDto()
            {
                TotalPrice = 100,
                Country = "test",
                EmailAddress = "test",
                FamilyName = "test",
                GivenName = "test",
                State = "test",
                ZipCode = "test",
                UserName = "test",
            };

            _orderRepository.Setup(s => s.UpdateOrderAsync(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
                )).ReturnsAsync(1);

            var result = await _orderService.UpdateOrderAsync(orderDto.TotalPrice, orderDto.UserName, orderDto.TotalPrice, orderDto.GivenName, orderDto.FamilyName, orderDto.EmailAddress, orderDto.Country, orderDto.State, orderDto.ZipCode);

            result.Should().Be(1);
        }

        [Fact]
        public async Task UpdateOrderAsyncFailed()
        {
            var orderDto = new OrderDto()
            {
                TotalPrice = 100,
                Country = "test",
                EmailAddress = "test",
                FamilyName = "test",
                GivenName = "test",
                State = "test",
                ZipCode = "test",
                UserName = "test",
            };

            _orderRepository.Setup(s => s.UpdateOrderAsync(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
                )).ThrowsAsync(new Exception());

            Func<Task> result = async () => await _orderService.UpdateOrderAsync(orderDto.TotalPrice, orderDto.UserName, orderDto.TotalPrice, orderDto.GivenName, orderDto.FamilyName, orderDto.EmailAddress, orderDto.Country, orderDto.State, orderDto.ZipCode);

            result.Should().ThrowAsync<Exception>();
        }
    }
}