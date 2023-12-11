
namespace CatalogHost.Tests.Services
{
    public class CatalogPlatformServiceTests
    {
        private readonly IBaseCatalogServices<CatalogPlatform> _catalogPlatformService;

        private readonly Mock<IBaseCatalogRepository<CatalogPlatform>> _catalogPlatformRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<BaseDataService<ApplicationDbContext>>> _logger;
        private readonly CatalogPlatform tetstPlatform = new CatalogPlatform()
        {
            Platform = "platform",
        };
        private readonly EntityType entityType = EntityType.Platform;

        public CatalogPlatformServiceTests()
        {
            _catalogPlatformRepository = new Mock<IBaseCatalogRepository<CatalogPlatform>>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();

            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dbContextTransaction.Object);

            _catalogPlatformService = new CatalogPlatformService(_dbContextWrapper.Object, _logger.Object, _catalogPlatformRepository.Object);
        }

        [Fact]
        public async Task AddSuccess()
        {
            int testResult = 1;

            _catalogPlatformRepository.Setup(s => s.Add(It.IsAny<string>(), It.IsAny<EntityType>())).ReturnsAsync(testResult);

            var result = await _catalogPlatformService.Add(tetstPlatform.Platform, entityType);

            result.Should().Be(testResult);

        }

        [Fact]
        public async Task AddFailed()
        {
            int? testResult = null;

            _catalogPlatformRepository.Setup(s => s.Add(It.IsAny<string>(), It.IsAny<EntityType>())).ReturnsAsync(testResult);

            var result = await _catalogPlatformService.Add(tetstPlatform.Platform, entityType);

            result.Should().Be(testResult);

        }

        [Fact]
        public async Task DeleteSuccess()
        {
            bool tetstResult = true;
            int id = 1;

            _catalogPlatformRepository.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(tetstResult);

            var result = await _catalogPlatformService.Delete(id);

            result.Should().Be(tetstResult);
        }

        [Fact]
        public async Task DeleteFailed()
        {
            int id = 1;

            _catalogPlatformRepository.Setup(s => s.Delete(It.IsAny<int>())).ThrowsAsync(new KeyNotFoundException());

           Func<Task> result = async () => await _catalogPlatformService.Delete(id);

            result.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}
