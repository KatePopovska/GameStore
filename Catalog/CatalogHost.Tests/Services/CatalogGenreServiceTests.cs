

namespace CatalogHost.Tests.Services
{
    public class CatalogGenreServiceTests
    {
        private readonly IBaseCatalogServices<CatalogGenre> _catalogGenreService;

        private readonly Mock<IBaseCatalogRepository<CatalogGenre>> _catalogGenreRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<BaseDataService<ApplicationDbContext>>> _logger;
        private readonly CatalogGenre _testGenre = new CatalogGenre()
        {
            Genre = "genre",
        };
        private readonly EntityType entityType = EntityType.Genre;

        public CatalogGenreServiceTests()
        {
            _catalogGenreRepository = new Mock<IBaseCatalogRepository<CatalogGenre>>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();

            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dbContextTransaction.Object);

            _catalogGenreService = new CatalogGenreService(_dbContextWrapper.Object, _logger.Object, _catalogGenreRepository.Object);
        }

        [Fact]
        public async Task AddSuccess()
        {
            int testResult = 1;

            _catalogGenreRepository.Setup(s => s.Add(It.IsAny<string>(), It.IsAny<EntityType>())).ReturnsAsync(testResult);

            var result = await _catalogGenreService.Add(_testGenre.Genre, entityType);

            result.Should().Be(testResult);

        }

        [Fact]
        public async Task AddFailed()
        {
            int? testResult = null;

            _catalogGenreRepository.Setup(s => s.Add(It.IsAny<string>(), It.IsAny<EntityType>())).ReturnsAsync(testResult);

            var result = await _catalogGenreService.Add(_testGenre.Genre, entityType);

            result.Should().Be(testResult);

        }

        [Fact]
        public async Task DeleteSuccess()
        {
            bool tetstResult = true;
            int id = 1;

            _catalogGenreRepository.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(tetstResult);

            var result = await _catalogGenreService.Delete(id);

            result.Should().Be(tetstResult);
        }

        [Fact]
        public async Task DeleteFailed()
        {
            int id = 1;

            _catalogGenreRepository.Setup(s => s.Delete(It.IsAny<int>())).ThrowsAsync(new KeyNotFoundException());

            Func<Task> result = async () => await _catalogGenreService.Delete(id);

            result.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}
