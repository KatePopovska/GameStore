using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntityConfiguration
{
    public class CatalogPlatformEntityConfiguration
        : IEntityTypeConfiguration<CatalogPlatform>
    {
        public void Configure(EntityTypeBuilder<CatalogPlatform> builder)
        {
            builder.ToTable("CatalogPlatform");

            builder.Property(x => x.Id)
                .UseHiLo("catalog_platform_hilo")
                .IsRequired();
            builder.Property(x => x.Platform)
                .IsRequired(true);
        }
    }
}
