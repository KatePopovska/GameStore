using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.EventBusMessages.Common
{
    
    public class ShoppingCartItem
    {
        public int Quantity { get; set; }

        public int Price { get; set; }

        public string ProductName { get; set; }

        public int ProductId { get; set; }
    }
}
