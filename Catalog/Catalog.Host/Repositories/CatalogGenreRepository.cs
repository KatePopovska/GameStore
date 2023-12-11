using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories
{
    public class CatalogGenreRepository : BaseCatalogRepository<CatalogGenre>
    {
        public CatalogGenreRepository(ApplicationDbContext dbContext, ILogger<CatalogGenre> logger) : base(dbContext, logger)
        {
        }
    }
}
