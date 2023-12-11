﻿using MVC.ViewModels.BasketViewModel;

namespace MVC.ViewModels.OrderViewModels
{
    public class OrderResponseModel
    {
        public string UserName { get; set; }
        public List<OrderItemModel> Items { get; set; }
        public int TotalPrice { get; set; }

        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string EmailAddress { get; set; }

        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
