using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Domain.Constant;
using Products.Domain.ProductAggregate;
using Products.Domain.ProductGroupAggregate;
using Products.Domain.ProductTypeAggregate;
using Products.Domain.ProductUnitAggregate;

namespace Products.Infrastructure.EfCore.EntityConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.ProductCode)
               .HasMaxLength(StringLength.ProductCode)
               .IsRequired();

        builder.HasIndex(x => x.ProductCode)
               .IsUnique();
        
        builder.Property(x => x.Name)
               .HasMaxLength(StringLength.Name)
               .IsRequired();

        builder.Property(x => x.IsObsolete)
               .IsRequired();

        builder.Property(x => x.ProductTypeId)
               .IsRequired();

        builder.Property(x => x.ProductGroupId)
               .IsRequired();

        builder.Property(x => x.SellUnitId)
               .IsRequired(false);

        builder.Property(x => x.SellPrice)
               .HasPrecision(20, 2)
               .IsRequired();

        builder.Property(x => x.BuyUnitId)
               .IsRequired(false);

        builder.Property(x => x.BuyPrice)
               .HasPrecision(20, 2)
               .IsRequired(false);

        builder.Property(x => x.Colors)
               .IsRequired(false);

        builder.HasOne<ProductType>(x => x.Type)
               .WithMany()
               .HasForeignKey(x => x.ProductTypeId);

        builder.HasOne<ProductGroup>(x => x.Group)
               .WithMany()
               .HasForeignKey(x => x.ProductGroupId);

        builder.HasOne<ProductUnit>(x => x.SellUnit)
               .WithMany()
               .HasForeignKey(x => x.SellUnitId)
               .IsRequired(false);
        
        builder.HasOne<ProductUnit>(x => x.BuyUnit)
               .WithMany()
               .HasForeignKey(x => x.BuyUnitId)
               .IsRequired(false);
    }
}