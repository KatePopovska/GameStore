using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Data.Entities
{
    public class CatalogGame
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public string PictureFileName { get; set; }
        public int Year { get; set; }

        public bool InStock { get; set; }

        public CatalogPublisher Publisher { get; set; }

        public int PublisherId { get; set; }

        public CatalogGenre Genre { get; set; }

        public int GenreId { get; set; }

        public ICollection<CatalogGamePlatform> GamePlatforms { get; set; } = new List<CatalogGamePlatform>();

    }
}
