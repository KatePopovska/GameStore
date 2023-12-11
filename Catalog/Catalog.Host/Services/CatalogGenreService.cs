using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogGenreService : BaseCatalogService<CatalogGenre>
    {
        public CatalogGenreService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IBaseCatalogRepository<CatalogGenre> repository) 
            : base(dbContextWrapper, logger, repository)
        {
        }
    }
}
