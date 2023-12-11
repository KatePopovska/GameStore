namespace MVC.Models.Response
{
    public class GetDataResponse<T>
    {
        public int Count { get; set; }
        public IEnumerable<T> Data { get; init; } = null!;
    }
}
