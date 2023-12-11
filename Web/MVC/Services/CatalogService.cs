using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MVC.Models.Enums;
using MVC.Models.Request;
using MVC.Models.Response;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly IOptions<AppSettings> _settings;
        private readonly ILogger<CatalogService> _logger;
        public CatalogService(IHttpClientService httpClientService, ILogger<CatalogService> logger, IOptions<AppSettings> options)
        {
            _httpClientService = httpClientService;
            _logger = logger;
            _settings = options;
        }

        public async Task<CatalogGame> GetById(int id)
        {
            var response = await _httpClientService
                .SendAsync<CatalogGame, int>($"{_settings.Value.CatalogUrl}/GetGameById", HttpMethod.Post, id);
            return response;
        }

        public async Task<Catalog> GetCatalogGames(int page, int take, int? genre, int? platform)
        {
            var filters = new Dictionary<CatalogTypeFilter, int>();

            if (genre.HasValue)
            {
                filters.Add(CatalogTypeFilter.Genre, genre.Value);
            }

            if (platform.HasValue)
            {
                filters.Add(CatalogTypeFilter.Platform, platform.Value);
            }

            var request = new PaginatedGamesRequest<CatalogTypeFilter>()
            {
                PageIndex = page,
                PageSize = take,
                Filters = filters
            };

            var result = await _httpClientService
                .SendAsync<Catalog, PaginatedGamesRequest<CatalogTypeFilter>>($"{_settings.Value.CatalogUrl}/games", HttpMethod.Post, request);

            return result;
        }

        public async Task<IEnumerable<SelectListItem>> GetGenre()
        {
            var result = await _httpClientService
                .SendAsync<GetDataResponse<CatalogGenre>, object>($"{_settings.Value.CatalogUrl}/genres", HttpMethod.Post, null);

            var selectedGenres = result.Data.Select(genre => new SelectListItem
            {
                Value = genre.Id.ToString(),
                Text = $"{genre.Genre}"
            });

            return selectedGenres;
        }
    }
}
