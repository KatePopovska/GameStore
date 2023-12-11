
using Catalog.Host.Services.Interfaces;

namespace CatalogHost.Tests.Services
{
    public class CatalogGameServiceTests
    {
        private readonly ICatalogGameService _catalogGameService;

        private readonly Mock<ICatalogGameRepository> _catalogGameRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<BaseDataService<ApplicationDbContext>>>  _logger;

        private readonly CatalogGame _gameTest = new CatalogGame()
        {
            Title = "test",
            Description = "test description",
            Price = 1000,
            Year = 2022,
            InStock = false,
            PublisherId = 3,
            GenreId = 2,
            PictureFileName = "1.png",
        };

        public CatalogGameServiceTests()
        {
            _catalogGameRepository = new Mock<ICatalogGameRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();

            _dbContextWrapper.Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dbContextTransaction.Object);

            _catalogGameService = new CatalogGameService(_dbContextWrapper.Object, _logger.Object, _catalogGameRepository.Object);
        }

        [Fact]
        public async Task AddAsyncSuccess()
        {
            var testResult = 1;

            _catalogGameRepository.Setup(s => s.Add(
              It.IsAny<string>(),
              It.IsAny<string>(),
              It.IsAny<int>(),
              It.IsAny<int>(),
              It.IsAny<string>(),
              It.IsAny<bool>(),
              It.IsAny<int>(),
              It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogGameService.AddAsync(_gameTest.Title, _gameTest.Description, _gameTest.Price, _gameTest.Year, _gameTest.PictureFileName, _gameTest.InStock, _gameTest.PublisherId, _gameTest.GenreId);

            result.Should().Be(testResult);
        }

        [Fact]
        public async Task AddAsyncFailed()
        {
            int? testResult = null;

            _catalogGameRepository.Setup(s => s.Add(
              It.IsAny<string>(),
              It.IsAny<string>(),
              It.IsAny<int>(),
              It.IsAny<int>(),
              It.IsAny<string>(),
              It.IsAny<bool>(),
              It.IsAny<int>(),
              It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogGameService.AddAsync(_gameTest.Title, _gameTest.Description, _gameTest.Price, _gameTest.Year, _gameTest.PictureFileName, _gameTest.InStock, _gameTest.PublisherId, _gameTest.GenreId);

            result.Should().Be(testResult);
        }

        [Fact]
        public async Task DeleteAsyncSuccess()
        {
            bool testResult = true;
            int id = 1;
            _catalogGameRepository.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogGameService.DeleteAsync(id);

            result.Should().Be(testResult);
        }


        [Fact]
        public async Task DeleteAsyncFailed()
        {
            int id = 1;
            _catalogGameRepository.Setup(s => s.Delete(It.IsAny<int>())).ThrowsAsync(new KeyNotFoundException());

            Func<Task> result = async () => await _catalogGameService.DeleteAsync(id);

            result.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task UpdateAsyncSuccess()
        {
            bool testResult = true;
            int id = 1;
            _catalogGameRepository.Setup(s => s.Update(
              It.IsAny<int>(),
              It.IsAny<string>(),
              It.IsAny<string>(),
              It.IsAny<int>(),
              It.IsAny<int>(),
              It.IsAny<string>(),
              It.IsAny<bool>(),
              It.IsAny<int>(),
              It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogGameService.UpdateAsync(id, _gameTest.Title, _gameTest.Description, _gameTest.Price, _gameTest.Year, _gameTest.PictureFileName, _gameTest.InStock, _gameTest.PublisherId, _gameTest.GenreId);

            result.Should().Be(testResult);
        }



        [Fact]
        public async Task UpdateAsyncFailed()
        {
            int id = 1;
            _catalogGameRepository.Setup(s => s.Update(
              It.IsAny<int>(),
              It.IsAny<string>(),
              It.IsAny<string>(),
              It.IsAny<int>(),
              It.IsAny<int>(),
              It.IsAny<string>(),
              It.IsAny<bool>(),
              It.IsAny<int>(),
              It.IsAny<int>())).ThrowsAsync(new Exception());

           Func<Task> result = async () => await _catalogGameService.UpdateAsync(id, _gameTest.Title, _gameTest.Description, _gameTest.Price, _gameTest.Year, _gameTest.PictureFileName, _gameTest.InStock, _gameTest.PublisherId, _gameTest.GenreId);

            result.Should().ThrowAsync<Exception>();
        }
    }
}
