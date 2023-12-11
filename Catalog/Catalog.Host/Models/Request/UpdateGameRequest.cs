using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Request
{
    public class UpdateGameRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get;set; }
        public string Description { get; set; } = null!;

        [Required]
        public int Price { get; set; }

        public int Year { get; set; }

        public string PictureFileName { get; set; } = null!;

        public bool InStock { get; set; }

        public int PublisherId { get; set; }

        public int GenreId { get; set; }
    }
}
