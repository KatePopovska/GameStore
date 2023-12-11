using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Enums;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class BaseCatalogRepository<T>
        : IBaseCatalogRepository<T>
        where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<T> _logger;

        public BaseCatalogRepository(ApplicationDbContext dbContext, ILogger<T> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<int?> Add(string title, EntityType entityType)
        {
            if (entityType == EntityType.Genre)
            {
                var genre1 = new CatalogGenre() { Genre = title };
                var genre = await _dbContext.AddAsync(genre1);
                await _dbContext.SaveChangesAsync();

                return genre.Entity.Id;
            }

            if (entityType == EntityType.Platform)
            {
                var platform1 = new CatalogPlatform() { Platform = title };
                var platform = await _dbContext.AddAsync(platform1);
                await _dbContext.SaveChangesAsync();

                return platform.Entity.Id;
            }

            var publisher1 = new CatalogPublisher() { Publisher = title };
            var publisher = await _dbContext.AddAsync(publisher1);
            await _dbContext.SaveChangesAsync();

            return publisher.Entity.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var entityToDelete = await _dbContext.FindAsync<T>(id);
            if (entityToDelete == null)
            {
                throw new KeyNotFoundException("Not Found");
            }

            _dbContext.Set<T>().Remove(entityToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<T>> GetAll()
        {
            var entities = await _dbContext.Set<T>().ToListAsync();
            return entities;
        }
    }
}
