using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntityConfiguration
{
    public class CatalogGenreEntityConfiguration :
        IEntityTypeConfiguration<CatalogGenre>
    {
        public void Configure(EntityTypeBuilder<CatalogGenre> builder)
        {
            builder.ToTable("CatalogGenre");
            builder.Property(x => x.Id)
               .UseHiLo("catalog_genre_hilo")
               .IsRequired();
            builder.Property(x => x.Genre)
                .IsRequired(true);
        }
    }
}
