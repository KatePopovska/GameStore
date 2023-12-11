using MVC.ViewModels.OrderViewModels;

namespace MVC.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName();
    }
}
