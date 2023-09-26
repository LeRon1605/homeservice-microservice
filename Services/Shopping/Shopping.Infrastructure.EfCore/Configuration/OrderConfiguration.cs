using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.OrderAggregate;

namespace Shopping.Infrastructure.EfCore.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ContactName)
            .IsRequired();
        
        builder.Property(x => x.BuyerId)
            .IsRequired();
        
        builder.Property(x => x.PhoneNumber)
            .IsRequired();

        builder.Property(x => x.EmailAddress);

        builder.Property(x => x.OrderValue)
            .HasPrecision(20, 2)
            .IsRequired();

        builder.Property(x => x.PlacedDate)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();
    }
}