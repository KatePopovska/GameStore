using Infrastructure.EventBusMessages.Common;

namespace Basket.Host.Models
{
    public class ShoppingCartDto
    {
        public string UserName { get; set; }

        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public int TotalPrice { get; set; }
    }
}
