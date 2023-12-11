namespace MVC.ViewModels.BasketViewModel
{
    public class BasketModel
    {
        public string UserName { get; set; }

        public List<BasketItemModel> Items { get; set; }

        public int TotalPrice { get; set; }
        public string PictureUrl { get; set; }

    }
}
