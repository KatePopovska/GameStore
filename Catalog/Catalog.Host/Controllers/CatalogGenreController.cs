using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Response;
using Catalog.Host.Services;
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
    [Scope("catalog.cataloggenre")]
    [Route(ComponentDefaults.DefaultRoute)]
    public class CatalogGenreController : ControllerBase
    {
        private readonly ILogger<CatalogGenreController> _looger;
        private readonly IBaseCatalogServices<CatalogGenre> _catalogGenreService;

        public CatalogGenreController(ILogger<CatalogGenreController> looger, IBaseCatalogServices<CatalogGenre> catalogGenreService)
        {
            _looger = looger;
            _catalogGenreService = catalogGenreService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddGameResponse<int?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddAsync( string title)
        {
            var result = await _catalogGenreService.Add(title, Repositories.Enums.EntityType.Genre);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _catalogGenreService.Delete(id);
            return Ok(result);
        }
    }
}
