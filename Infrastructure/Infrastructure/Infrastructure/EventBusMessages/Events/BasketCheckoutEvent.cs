using Infrastructure.EventBusMessages.Common;
using Infrastructure.Events;

namespace Infrastructure.EventBusMessages.Events
{
    public class BasketCheckoutEvent : IntegrationBaseEvent
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }

        public int TotalPrice { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string EmailAddress { get; set; }

        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
