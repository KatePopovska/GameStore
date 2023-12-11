using Basket.Host.Entities;

namespace Basket.Host.Services
{
    public interface IBasketService
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> AddToBasket(string userName, ShoppingCartItem item);
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        Task<bool> DeleteBasket(string userName);

        Task<ShoppingCart> DeleteItem(string userName, int productId);
    }
}
