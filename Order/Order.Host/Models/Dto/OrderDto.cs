using Infrastructure.EventBusMessages.Common;
using Order.Host.Data.Entities;

namespace Order.Host.Models.Dto
{
    public class OrderDto
    {
        public string UserName { get; set; }
        public int TotalPrice { get; set; }
        public List<ItemDto> Items { get; set; } = new List<ItemDto>();
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string EmailAddress { get; set; }

        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
