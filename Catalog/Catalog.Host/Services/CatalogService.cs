using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dto;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using IdentityModel.Client;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
    {
        private readonly ICatalogGameRepository _catalogGameRepository;
        private readonly IBaseCatalogRepository<CatalogGenre> _catalogGenreRepository;
        private readonly IBaseCatalogRepository<CatalogPlatform> _catalogPlatformRepository;
        private readonly IBaseCatalogRepository<CatalogPublisher> _catalogPublisherRepository;
        private readonly ILogger<CatalogService> _logger;
        private readonly IMapper _mapper;

        public CatalogService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogService> logger,
            ICatalogGameRepository catalogGameRepository,
            IBaseCatalogRepository<CatalogGenre> catalogGenreRepository,
            IBaseCatalogRepository<CatalogPlatform> catalogPlatformRepository,
            IBaseCatalogRepository<CatalogPublisher> catalogPublisherRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogGameRepository = catalogGameRepository;
            _catalogPlatformRepository = catalogPlatformRepository;
            _catalogPublisherRepository = catalogPublisherRepository;
            _catalogGenreRepository = catalogGenreRepository;
            _mapper = mapper;
            _logger= logger;
        }

        public async Task<CatalogGameDto> GetGameById(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var game = await _catalogGameRepository.GetById(id);
                var gameDto = _mapper.Map<CatalogGameDto>(game);
                return gameDto;
            });
        }

        public async Task<PaginatedGamesResponse<CatalogGameDto?>> GetGames(int pageIndex, int pageSize, Dictionary<CatalogTypeFilter, int>? filters)
        {
            return await ExecuteSafeAsync(async () =>
            {
                int? genreFilter = null;
                int? platformFilter = null;

                if (filters != null)
                {
                    if (filters.TryGetValue(CatalogTypeFilter.Genre, out var genre))
                    {
                        genreFilter = genre;
                    }

                    if (filters.TryGetValue(CatalogTypeFilter.Platform, out var platform))
                    {
                        platformFilter = platform;
                    }
                }

                _logger.LogInformation("CatalogService GetGames execution");
                var result = await _catalogGameRepository.GetByPageAsync(pageIndex, pageSize, platformFilter, genreFilter);
                _logger.LogInformation($"result: {result}");

                if (result == null)
                {
                    return null;
                }

                return new PaginatedGamesResponse<CatalogGameDto?>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = result.TotalCount,
                    Data = result.Data.Select(x => _mapper.Map<CatalogGameDto>(x)).ToList(),
                };
            });
        }

        public async Task<GetDataResponse<CatalogGenreDto>> GetGenres()
        {
            return await ExecuteSafeAsync(async () =>
            {
                _logger.LogInformation("CatalogService GetGenres execution");
                var result = await _catalogGenreRepository.GetAll();
                _logger.LogInformation($"result: {result}");
                return new GetDataResponse<CatalogGenreDto>()
                {
                    Data = result.Select(x => _mapper.Map<CatalogGenreDto>(x)).ToList(),
                    Count = result.Count,
                };
            });
        }

        public async Task<GetDataResponse<CatalogPlatformDto>> GetPlatforms()
        {
            return await ExecuteSafeAsync(async () =>
            {
                _logger.LogInformation("CatalogService GetPlatforms execution");
                var result = await _catalogPlatformRepository.GetAll();
                _logger.LogInformation($"result: {result}");

                return new GetDataResponse<CatalogPlatformDto>()
                {
                    Count = result.Count,
                    Data = result.Select(x => _mapper.Map<CatalogPlatformDto>(x)).ToList(),
                };
            });
        }

        public async Task<GetDataResponse<CatalogPublisherDto>> GetPublisher()
        {
            return await ExecuteSafeAsync(async () =>
            {
                _logger.LogInformation("CatalogService GetPublisher execution");
                var result = await _catalogPublisherRepository.GetAll();
                _logger.LogInformation($"result: {result}");

                return new GetDataResponse<CatalogPublisherDto>()
                {
                    Count = result.Count,
                    Data = result.Select(x => _mapper.Map<CatalogPublisherDto>(x)).ToList(),
                };
            });
        }
    }
}
