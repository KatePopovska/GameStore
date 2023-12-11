using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Dto
{
    public class CatalogPlatformDto
    {
        public int Id { get; set; }

        public string Platform { get; set; } = null!;
    }
}
