using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories
{
    public class CatalogPlatformRepository : BaseCatalogRepository<CatalogPlatform>
    {
        public CatalogPlatformRepository(ApplicationDbContext dbContext, ILogger<CatalogPlatform> logger) : base(dbContext, logger)
        {
        }
    }
}
