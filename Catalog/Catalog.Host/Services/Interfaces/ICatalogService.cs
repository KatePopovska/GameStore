using Catalog.Host.Models.Dto;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<PaginatedGamesResponse<CatalogGameDto?>> GetGames(int pageIndex, int pageSize, Dictionary<CatalogTypeFilter, int>? filters);

        Task<GetDataResponse<CatalogGenreDto>> GetGenres();

        Task<GetDataResponse<CatalogPlatformDto>> GetPlatforms();

        Task<GetDataResponse<CatalogPublisherDto>> GetPublisher();
        Task<CatalogGameDto> GetGameById(int id);
    }
}
