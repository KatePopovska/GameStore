using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.ViewModels.Pagination;

namespace MVC.ViewModels.CatalogViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<CatalogGame> Games { get; set; }

        public IEnumerable<SelectListItem> Genres { get; set; }

        public int? GenreFilterApplied { get; set; }

        public int? PlatformFilterApplied { get; set; }

        public bool CancelFilters { get; set; }

        public PaginationInfo PaginationInfo { get; set; }
    }
}
