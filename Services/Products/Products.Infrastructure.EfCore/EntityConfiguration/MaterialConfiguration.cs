using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Domain.Constant;
using Products.Domain.MaterialAggregate;
using Products.Domain.ProductTypeAggregate;
using Products.Domain.ProductUnitAggregate;

namespace Products.Infrastructure.EfCore.EntityConfiguration;

public class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.HasIndex(x => x.MaterialCode).IsUnique();

        builder.Property(x => x.MaterialCode)
            .HasMaxLength(StringLength.ProductCode)
            .IsRequired();
        
        builder.Property(x => x.Name)
            .HasMaxLength(StringLength.Name)
            .IsRequired();

        builder.Property(x => x.IsObsolete)
            .IsRequired();
        
        builder.Property(x => x.SellUnitId)
               .IsRequired(false);

        builder.Property(x => x.SellPrice)
               .HasPrecision(20, 2)
               .IsRequired(false);
        
        builder.Property(x => x.Cost)
                .HasPrecision(20, 2)
                .IsRequired(false);
        
        builder.HasOne<ProductType>(x => x.Type)
               .WithMany()
               .HasForeignKey(x => x.ProductTypeId);
        
        builder.HasOne<ProductUnit>(x => x.SellUnit)
                .WithMany()
                .HasForeignKey(x => x.SellUnitId)
                .IsRequired(false);

    }
}