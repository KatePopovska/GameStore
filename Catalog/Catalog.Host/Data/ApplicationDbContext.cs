using Catalog.Host.Data.Entities;
using Catalog.Host.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CatalogGame> CatalogGames { get; set; }
        public DbSet<CatalogGenre> CatalogGenres { get; set; }
        public DbSet<CatalogPlatform> CatalogPlatforms { get; set; }
        public DbSet<CatalogPublisher> CatalogPublishers { get; set; }
        public DbSet<CatalogGamePlatform> CatalogGamePlatforms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CatalogGameEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogGenreEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogPlatformEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogPublisherEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogGamePlatformEntityConfiguration());

        }
    }
}
