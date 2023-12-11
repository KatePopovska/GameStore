using Catalog.Host.Configurations;
using Catalog.Host.Models.Dto;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [Scope("catalog.catalogbff")]
    [Route(ComponentDefaults.DefaultRoute)]
    public class CatalogBffController : ControllerBase
    {
        private readonly ILogger<CatalogBffController> _logger;
        private readonly ICatalogService _catalogService;
        private readonly IOptions<CatalogConfig> _config;

        public CatalogBffController(ILogger<CatalogBffController> logger, ICatalogService catalogService, IOptions<CatalogConfig> config)
        {
            _logger = logger;
            _catalogService = catalogService;
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PaginatedGamesResponse<CatalogGameDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Games(PaginatedGamesRequest<CatalogTypeFilter> request)
        {
            var result = await _catalogService.GetGames(request.PageIndex, request.PageSize, request.Filters);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(GetDataResponse<CatalogPlatformDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Platforms()
        {
            var result = await _catalogService.GetPlatforms();
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GetDataResponse<CatalogGenreDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Genres()
        {
            var result = await _catalogService.GetGenres();
            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(typeof(GetDataResponse<CatalogPublisherDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Publishers()
        {
            var result = await _catalogService.GetPublisher();
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CatalogGameDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetGameById([FromBody] int id)
        {
            return Ok(await _catalogService.GetGameById(id));
        }

    }
}
