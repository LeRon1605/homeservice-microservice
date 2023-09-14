using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Domain.Constant;
using Products.Domain.ProductGroupAggregate;

namespace Products.Infrastructure.EfCore.EntityConfiguration;

public class ProductGroupConfiguration : IEntityTypeConfiguration<ProductGroup>
{
    public void Configure(EntityTypeBuilder<ProductGroup> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Name).HasMaxLength(StringLength.Name);
    }
}