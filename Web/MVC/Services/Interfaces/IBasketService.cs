using MVC.ViewModels.BasketViewModel;

namespace MVC.Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketModel> GetBasket();

        Task<BasketModel> AddToBasket(BasketItemModel basket);
        Task<BasketModel> UpdateBasket(BasketModel basket);
        Task CheckoutBasket(BasketCheckoutModel order);
        Task<BasketModel> DeleteItem(int productId);
    }
}
