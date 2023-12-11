using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntityConfiguration
{
    public class CatalogGamePlatformEntityConfiguration
        : IEntityTypeConfiguration<CatalogGamePlatform>
    {
        public void Configure(EntityTypeBuilder<CatalogGamePlatform> builder)
        {
            builder.HasKey(k => new { k.CatalogGameId, k.CatalogPlatformId });

            builder.HasOne(x => x.CatalogGame)
                .WithMany(d => d.GamePlatforms)
                .HasForeignKey(x => x.CatalogGameId);

            builder.HasOne(x => x.CatalogPlatform)
                .WithMany(x => x.GamePlatforms)
                .HasForeignKey(x => x.CatalogPlatformId);
        }
    }
}
