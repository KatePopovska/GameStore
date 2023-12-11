using Microsoft.EntityFrameworkCore;
using Order.Host.Data;
using Order.Host.Data.Entities;

namespace Order.Host.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private ILogger<OrderRepository> _logger;

        public OrderRepository(ApplicationDbContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;

        }
        public async Task<int> AddOrderAsync(OrderEntity order)
        {
           var result = _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.FindAsync<OrderEntity>(id);
            try
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Exception from OrderRepository:{ex.Message}");
            }
            return false;
        }

        public async Task<IEnumerable<OrderEntity>> GetOrdersAsync(string userName)
        {
            var orders = await _context.Orders
            .Include(o => o.Items)
            .Where(o => o.UserName == userName)
            .ToListAsync();
            _logger.LogInformation("LOG FROM GetOrdersAsync");
            return orders;
        }

        public async Task<int> UpdateOrderAsync(int id, string userName, int totalProce, string givenName,
            string familyName, string emailAddress, string country, string state, string zipCode)
        {
            var order = new OrderEntity()
            {
                Id = id,
                UserName = userName,
                TotalPrice = totalProce,
                GivenName = givenName,
                FamilyName = familyName,
                EmailAddress = emailAddress,
                Country = country,
                State = state,
                ZipCode = zipCode,
                CreatedBy = userName

            };
            var result = _context.Orders.Update(order);

            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }
    }
}
