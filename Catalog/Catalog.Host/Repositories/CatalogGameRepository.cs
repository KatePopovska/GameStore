using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogGameRepository : ICatalogGameRepository
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly ILogger<CatalogGameRepository> _logger;

        public CatalogGameRepository(ApplicationDbContext dbContext, ILogger<CatalogGameRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<int?> Add(string title, string? description, int price, int year, string pictureFileName, bool inStock, int publisherId, int genreId)
        {
            var newGame = new CatalogGame
            {
                Title = title,
                Description = description,
                Price = price,
                Year = year,
                PictureFileName = pictureFileName,
                InStock = inStock,
                PublisherId = publisherId,
                GenreId = genreId,
            };

            var game =  await _dbContext.AddAsync(newGame);
            await _dbContext.SaveChangesAsync();

            return game.Entity.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var gameToDelete = await _dbContext.CatalogGames.FirstOrDefaultAsync(x => x.Id == id);
            if (gameToDelete != null)
            {
                _dbContext.Remove(gameToDelete);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            throw new KeyNotFoundException();
        }

        public async Task<CatalogGame> GetById(int id)
        {
            var game = await _dbContext.CatalogGames
                .Include(s => s.Genre)
                .Include(s => s.Publisher)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (game == null)
            {
                throw new KeyNotFoundException();
            }
            return game;
        }

        public async Task<PaginatedItems<CatalogGame>> GetByPageAsync(int pageIndex, int pageSize, int? platformFilter, int? genreFilter)
        {
            _logger.LogInformation("GetByPageAsync execution");
            IQueryable<CatalogGame> query = _dbContext.CatalogGames;

            if (platformFilter.HasValue)
            {
                query = query.Where(game => game.GamePlatforms.Any(p => p.CatalogPlatformId == platformFilter));
            }

            if (genreFilter.HasValue)
            {
                query = query.Where(game => game.GenreId == genreFilter);
            }
            _logger.LogInformation($"Query filters {query.ToList()}");
            var totalItems = await query.LongCountAsync();

            var gamesOnPage = await query.OrderBy(x => x.Title)
                .Include(x => x.Genre)
                .Include(x => x.Publisher)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            _logger.LogInformation($"Query before return data {gamesOnPage.ToList()}");

            return new PaginatedItems<CatalogGame>() {  TotalCount = totalItems, Data = gamesOnPage };
        }

        public async Task<bool> Update(int id, string title, string? description, int price, int year, string pictureFileName, bool inStock, int publisherId, int genreId)
        {
            var gameToUpdate = new CatalogGame()
            {
                Id = id,
                Title = title,
                Description = description,
                Price = price,
                Year = year,
                PictureFileName = pictureFileName,
                InStock = inStock,
                PublisherId = publisherId,
                GenreId = genreId,
            };

            var game = _dbContext.Update(gameToUpdate);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
