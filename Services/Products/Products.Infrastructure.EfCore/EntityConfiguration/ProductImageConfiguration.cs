using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Domain.ProductAggregate;

namespace Products.Infrastructure.EfCore.EntityConfiguration;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasKey(x => x.Id);
        
        // builder.Property(x => x.Id)
        //        .ValueGeneratedNever()
        //        .IsRequired();

        builder.Property(x => x.Url)
               .IsRequired();
    }
}