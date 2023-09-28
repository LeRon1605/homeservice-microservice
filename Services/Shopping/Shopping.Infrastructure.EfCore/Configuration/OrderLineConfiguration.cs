using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.OrderAggregate;
using Shopping.Domain.ProductAggregate;

namespace Shopping.Infrastructure.EfCore.Configuration;

public class OrderLineConfiguration: IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.HasKey(x => new { x.ProductId, x.OrderId });

        builder.Property(x => x.Color)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.Tax)
            .IsRequired();
        
        builder.Property(x=>x.Cost)
            .HasPrecision(20, 2)
            .IsRequired();

        builder.Property(x => x.UnitName)
            .IsRequired(false);

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Order>()
            .WithMany(x => x.OrderLines)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}