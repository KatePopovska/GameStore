using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Dto
{
    public class CatalogGenreDto
    {
        public int Id { get; set; }
        public string Genre { get; set; } = null!;
    }
}
