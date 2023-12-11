namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogGameService
    {
        Task<int?> AddAsync(string title, string? description, int price, int year, string pictureFileName, bool inStock, int publisherId, int genreId);

        Task<bool> UpdateAsync(int id, string title, string description, int price, int year, string pictureFileName, bool inStock, int publisherId, int genreId);

        Task<bool> DeleteAsync(int id);

    }
}
