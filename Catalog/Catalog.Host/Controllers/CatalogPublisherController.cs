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
    [Scope("catalog.catalogpublisher")]
    [Route(ComponentDefaults.DefaultRoute)]
    public class CatalogPublisherController : ControllerBase
    {
        private readonly ILogger<CatalogPublisherController> _looger;
        private readonly IBaseCatalogServices<CatalogPublisher> _catalogPublisherService;

        public CatalogPublisherController(ILogger<CatalogPublisherController> looger, IBaseCatalogServices<CatalogPublisher> catalogPublisherService)
        {
            _looger = looger;
            _catalogPublisherService= catalogPublisherService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddGameResponse<int?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddAsync(string title)
        {
            var result = await _catalogPublisherService.Add(title, Repositories.Enums.EntityType.Publisher);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _catalogPublisherService.Delete(id);
            return Ok(result);
        }
    }
}
