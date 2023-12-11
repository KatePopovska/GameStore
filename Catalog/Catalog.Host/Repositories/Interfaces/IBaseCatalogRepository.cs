using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Enums;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface IBaseCatalogRepository<T>
    {
        Task<List<T>> GetAll();

        Task<int?> Add(string title, EntityType entityType);

        Task<bool> Delete(int id);
    }
}
