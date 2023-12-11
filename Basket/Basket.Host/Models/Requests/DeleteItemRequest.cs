namespace Basket.Host.Models.Requests
{
    public class DeleteItemRequest
    {
        public string UserName { get; set; }

        public int ProductId { get; set; }
    }
}
