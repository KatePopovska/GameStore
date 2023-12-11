using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Data.Entities
{
    public class CatalogGenre
    {
        public int Id { get; set; }
        [Required]
        public string Genre { get; set; }
    }
}
