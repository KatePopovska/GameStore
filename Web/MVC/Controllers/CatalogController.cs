using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services.Interfaces;
using MVC.ViewModels.CatalogViewModels;
using MVC.ViewModels.Pagination;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ILogger<CatalogController> _logger;
        public CatalogController(ILogger<CatalogController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _catalogService.GetById(id);

            return View(result);
        }
        public async Task<IActionResult> Index(int? page, int? gamesOnPage, int? genreFilterApplied, int? platformFilterApplied, bool cancelFilter)
        {
            page ??= 0;
            gamesOnPage ??= 6;

            if (cancelFilter)
            {
                genreFilterApplied = null;
                platformFilterApplied = null;
            }

            var catalog = await _catalogService.GetCatalogGames(page.Value, gamesOnPage.Value, genreFilterApplied, platformFilterApplied);

            if (catalog == null)
            {
                return View("Error");
            }
            var info = new PaginationInfo()
            {
                ActualPage = page.Value,
                ItemsPerPage = catalog.Data.Count,
                TotalItems = catalog.Count,
                TotalPages = (int)Math.Ceiling((decimal)catalog.Count / gamesOnPage.Value)
            };

            var vm = new IndexViewModel()
            {
                Games = catalog.Data,
                Genres = await _catalogService.GetGenre(),
                PaginationInfo = info
            };
            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return View(vm);
        }
    }
}