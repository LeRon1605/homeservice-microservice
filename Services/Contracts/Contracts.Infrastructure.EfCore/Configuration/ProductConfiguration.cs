using Contracts.Domain.ProductAggregate;
using Contracts.Domain.ProductUnitAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contracts.Infrastructure.EfCore.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.Colors)
            .IsRequired(false)
            .HasConversion(x => string.Join(",", x),
                x => x.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList()
            );
        
        builder.Property(x => x.Price)
            .HasPrecision(20, 2)
            .IsRequired();
        
        builder.HasOne<ProductUnit>(x => x.Unit)
            .WithMany()
            .HasForeignKey(x => x.Unit)
            .IsRequired(false);
    }
}