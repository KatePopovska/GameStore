using Catalog.Host.Repositories.Enums;

namespace Catalog.Host.Services.Interfaces
{
    public interface IBaseCatalogServices<T>
    {
        Task<int?> Add(string title, EntityType entityType);

        Task<bool> Delete(int id);
    }
}
