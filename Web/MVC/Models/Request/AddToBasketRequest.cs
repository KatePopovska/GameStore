using MVC.ViewModels.BasketViewModel;

namespace MVC.Models.Request
{
    public class AddToBasketRequest
    {
        public string UserName { get; set; }

        public BasketItemModel Item { get; set; }
    }
}
