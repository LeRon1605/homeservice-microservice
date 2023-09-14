using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Domain.Constant;
using Products.Domain.ProductUnitAggregate;

namespace Products.Infrastructure.EfCore.EntityConfiguration;

public class ProductUnitConfiguration : IEntityTypeConfiguration<ProductUnit>
{
    public void Configure(EntityTypeBuilder<ProductUnit> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Name).HasMaxLength(StringLength.Name);
    }
}