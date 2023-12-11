using Catalog.Host.Data;
using Catalog.Host.Repositories.Enums;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class BaseCatalogService<T> : BaseDataService<ApplicationDbContext>, IBaseCatalogServices<T>
    {
        private readonly IBaseCatalogRepository<T> _repository;

        public BaseCatalogService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper, 
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IBaseCatalogRepository<T> repository)
            : base(dbContextWrapper, logger)
        {
            _repository = repository;
        }

        public Task<int?> Add(string title, EntityType entityType)
        {
            return ExecuteSafeAsync(() => _repository.Add(title, entityType));
        }

        public Task<bool> Delete(int id)
        {
            return ExecuteSafeAsync(() => _repository.Delete(id));
        }
    }
}
