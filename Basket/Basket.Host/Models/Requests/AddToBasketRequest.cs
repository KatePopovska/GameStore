using Basket.Host.Entities;

namespace Basket.Host.Models.Requests
{
    public class AddToBasketRequest
    {
        public string UserName { get; set; }

        public ShoppingCartItem Item { get; set; }
    }
}
