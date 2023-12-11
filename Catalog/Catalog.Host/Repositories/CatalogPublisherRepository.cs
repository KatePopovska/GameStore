using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories
{
    public class CatalogPublisherRepository : BaseCatalogRepository<CatalogPublisher>
    {
        public CatalogPublisherRepository(ApplicationDbContext dbContext, ILogger<CatalogPublisher> logger) : base(dbContext, logger)
        {
        }
    }
}
