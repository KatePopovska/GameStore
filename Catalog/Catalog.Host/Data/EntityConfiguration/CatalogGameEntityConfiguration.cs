using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntityConfiguration
{
    public class CatalogGameEntityConfiguration :
        IEntityTypeConfiguration<CatalogGame>
    {
        public void Configure(EntityTypeBuilder<CatalogGame> builder)
        {
            builder.ToTable("Catalog");

            builder.Property(x => x.Id)
                .UseHiLo("catalog_hilo")
                .IsRequired();
            builder.Property(x => x.Title)
                .IsRequired(true);

            builder.Property(x => x.Description)
                .IsRequired(false);

            builder.Property(x => x.Year)
                .IsRequired(true);

            builder.Property(x => x.InStock)
                .IsRequired(true);

            builder.Property(x => x.Price)
                .IsRequired(true);

            builder.Property(x => x.PictureFileName)
                .IsRequired(false);

            builder.HasOne(x => x.Genre)
                .WithMany()
                .HasForeignKey(x => x.GenreId);

            builder.HasOne(x => x.Publisher)
                .WithMany()
                .HasForeignKey(x => x.PublisherId);
        }
    }
}
