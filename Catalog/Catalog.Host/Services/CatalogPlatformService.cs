using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogPlatformService : BaseCatalogService<CatalogPlatform>
    {
        public CatalogPlatformService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IBaseCatalogRepository<CatalogPlatform> repository) 
            : base(dbContextWrapper, logger, repository)
        {
        }
    }
}
