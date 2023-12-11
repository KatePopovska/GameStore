using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntityConfiguration
{
    public class CatalogPublisherEntityConfiguration
       : IEntityTypeConfiguration<CatalogPublisher>
    {
        public void Configure(EntityTypeBuilder<CatalogPublisher> builder)
        {
            builder.ToTable("CatalogPublisher");
            builder.Property(x => x.Id)
               .UseHiLo("catalog_genre_hilo")
               .IsRequired();
        }
    }
}
