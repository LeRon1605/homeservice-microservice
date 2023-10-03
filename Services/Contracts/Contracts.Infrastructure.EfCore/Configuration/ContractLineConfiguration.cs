using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ProductAggregate;
using Contracts.Domain.ProductUnitAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contracts.Infrastructure.EfCore.Configuration;

public class ContractLineConfiguration : IEntityTypeConfiguration<ContractLine>
{
    public void Configure(EntityTypeBuilder<ContractLine> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Color)
            .IsRequired(false);

        builder.Property(x => x.Quantity)
            .IsRequired();
        
        builder.Property(x=> x.Cost)
            .HasPrecision(20, 2)
            .IsRequired();
        
        builder.Property(x=> x.SellPrice)
            .HasPrecision(20, 2)
            .IsRequired();

        builder.Property(x => x.UnitName)
            .IsRequired(false);

        builder.Property(x => x.ProductName)
            .IsRequired();

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Contract>()
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.ContractId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<ProductUnit>()
            .WithMany()
            .HasForeignKey(x => x.UnitId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}