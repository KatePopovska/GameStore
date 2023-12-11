using Catalog.Host.Models.Dto;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Scope("catalog.cataloggame")]
    [Route(ComponentDefaults.DefaultRoute)]
    public class CatalogGameController : ControllerBase
    {
        private readonly ILogger<CatalogGameController> _logger;
        private readonly ICatalogGameService _catalogGameService;

        public CatalogGameController(ILogger<CatalogGameController> logger, ICatalogGameService catalogGameService)
        {
            _logger = logger;
            _catalogGameService = catalogGameService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddGameResponse<int?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddAsync(CreateGameRequest request)
        {
            var result = await _catalogGameService.AddAsync(request.Name, request.Description, request.Price, request.Year, request.PictureFileName, request.InStock, request.PublisherId, request.GenreId);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAsync(UpdateGameRequest request)
        {
            var result = await _catalogGameService.UpdateAsync(request.Id, request.Name, request.Description, request.Price, request.Year, request.PictureFileName, request.InStock, request.PublisherId, request.GenreId);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _catalogGameService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
