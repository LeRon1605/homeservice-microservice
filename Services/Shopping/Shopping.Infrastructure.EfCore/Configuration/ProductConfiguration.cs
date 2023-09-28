using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.Constants;
using Shopping.Domain.ProductAggregate;
using Shopping.Domain.ProductUnitAggregate;

namespace Shopping.Infrastructure.EfCore.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .HasMaxLength(StringLength.Name)
               .IsRequired();

        builder.Property(x => x.ProductGroupId)
               .IsRequired();

        builder.HasOne(x => x.ProductUnit)
                .WithMany()
                .HasForeignKey(x => x.ProductUnitId);

        builder.Property(x => x.Price)
               .HasPrecision(20, 2)
               .IsRequired();
    }
}