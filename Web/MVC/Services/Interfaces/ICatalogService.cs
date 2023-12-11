using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.ViewModels;

namespace MVC.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<Catalog> GetCatalogGames(int page, int take, int? genre, int? platform);

        Task<IEnumerable<SelectListItem>> GetGenre();
        Task<CatalogGame> GetById(int id);
    }
}
