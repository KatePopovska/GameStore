using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogGameService : BaseDataService<ApplicationDbContext>, ICatalogGameService
    {
        private readonly ICatalogGameRepository _catalogGameRepository;

        public CatalogGameService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogGameRepository catalogGameRepository)
            : base(dbContextWrapper, logger)
        {
            _catalogGameRepository = catalogGameRepository;
        }

        public Task<int?> AddAsync(string title, string description, int price, int year, string pictureFileName, bool inStock, int publisherId, int genreId)
        {
            return ExecuteSafeAsync(() => _catalogGameRepository.Add(title, description, price, year, pictureFileName, inStock, publisherId, genreId));
        }

        public Task<bool> DeleteAsync(int id)
        {
            return ExecuteSafeAsync(() => _catalogGameRepository.Delete(id));
        }

        public Task<bool> UpdateAsync(int id, string title, string? description, int price, int year, string pictureFileName, bool inStock, int publisherId, int genreId)
        {
            return ExecuteSafeAsync(() => _catalogGameRepository.Update(id, title, description, price, year, pictureFileName, inStock, publisherId, genreId));
        }
    }
}
