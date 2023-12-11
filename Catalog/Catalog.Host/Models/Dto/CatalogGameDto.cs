using Catalog.Host.Data.Entities;

namespace Catalog.Host.Models.Dto
{
    public class CatalogGameDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int Price { get; set; }

        public string PictureUrl { get; set; } = null!;

        public int Year { get; set; }

        public bool InStock { get; set; }

        public CatalogPublisher Publisher { get; set; } = null!;

        public CatalogGenre Genre { get; set; } = null!;
    }
}
