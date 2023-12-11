using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Response;
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
    [Scope("catalog.catalogplatform")]
    [Route(ComponentDefaults.DefaultRoute)]
    public class CatalogPlatformController : ControllerBase
    {
        private readonly ILogger<CatalogPlatformController> _looger;
        private readonly IBaseCatalogServices<CatalogPlatform> _catalogPlatformService;

        public CatalogPlatformController(ILogger<CatalogPlatformController> looger, IBaseCatalogServices<CatalogPlatform> catalogPlatformService)
        {
            _looger = looger;
            _catalogPlatformService= catalogPlatformService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddGameResponse<int?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddAsync(string title)
        {
            var result = await _catalogPlatformService.Add(title, Repositories.Enums.EntityType.Platform);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _catalogPlatformService.Delete(id);
            return Ok(result);
        }
    }
}
