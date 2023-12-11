namespace Catalog.Host.Models.Response
{
    public class AddGameResponse<T>
    {
        public T Id { get; set; } = default(T)!;
    }
}
