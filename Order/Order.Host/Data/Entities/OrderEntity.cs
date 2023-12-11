using Infrastructure.EventBusMessages.Common;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using Order.Host.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Host.Data.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ItemsId { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public string UserName { get; set; }
        public int TotalPrice { get; set; }

        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string EmailAddress { get; set; }

        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        [JsonIgnore]
        public OrderEntity OrderEntity { get; set; }
        public int Price { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
