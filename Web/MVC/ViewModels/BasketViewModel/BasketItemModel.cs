namespace MVC.ViewModels.BasketViewModel
{
    public class BasketItemModel
    {
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
    }
}
