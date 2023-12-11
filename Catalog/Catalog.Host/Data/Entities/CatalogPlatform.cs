using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Data.Entities
{
    public class CatalogPlatform
    {
        public int Id { get; set; }

        [Required]
        public string Platform { get; set; }

        public ICollection<CatalogGamePlatform> GamePlatforms { get; set; } = new List<CatalogGamePlatform>();
    }
}
