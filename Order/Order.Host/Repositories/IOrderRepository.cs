using Order.Host.Data.Entities;

namespace Order.Host.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderEntity>> GetOrdersAsync(string userName);
        Task<int> AddOrderAsync(OrderEntity order);
        Task<int> UpdateOrderAsync(int id, string userName, int totalProce, string givenName,
            string familyName, string emailAddress, string country, string state, string zipCode);
        Task<bool> DeleteOrderAsync(int id);
    }
}
