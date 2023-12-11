using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogPublisherService : BaseCatalogService<CatalogPublisher>
    {
        public CatalogPublisherService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IBaseCatalogRepository<CatalogPublisher> repository) 
            : base(dbContextWrapper, logger, repository)
        {
        }
    }
}
