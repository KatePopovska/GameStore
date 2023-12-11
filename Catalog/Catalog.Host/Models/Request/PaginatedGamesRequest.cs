using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Request
{
    public class PaginatedGamesRequest<T>
        where T : notnull
    {
        [Required]
        public int PageIndex { get; set; }

        [Required]
        public int PageSize { get; set; }

        public Dictionary<T, int>? Filters { get; set; }
    }
}
