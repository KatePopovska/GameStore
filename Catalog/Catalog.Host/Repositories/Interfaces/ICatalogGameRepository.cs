using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogGameRepository
    {
        Task<PaginatedItems<CatalogGame>> GetByPageAsync(int pageIndex, int pageSize, int? platformFilter, int? genreFilter);

        Task<int?> Add(string title, string? description, int price, int year, string pictureFileName, bool inStock, int publisherId, int genreId);

        Task<bool> Update(int id, string title, string? description, int price, int year, string pictureFileName, bool inStock, int publisherId, int genreId);

        Task<bool> Delete(int id);
        Task<CatalogGame> GetById(int id);
    }
}
