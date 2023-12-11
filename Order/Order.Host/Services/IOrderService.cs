using Order.Host.Models.Dto;

namespace Order.Host.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetOrdersAsync(string userName);
        Task<int> AddOrderAsync(OrderDto order);
        Task<int> UpdateOrderAsync(int id, string userName, int totalProce, string givenName,
            string familyName, string emailAddress, string country, string state, string zipCode);
        Task<bool> DeleteOrderAsync(int id);
    }
}
