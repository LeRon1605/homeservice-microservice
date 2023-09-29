using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.BuyerAggregate;
using Shopping.Domain.Constants;
using Shopping.Domain.OrderAggregate;

namespace Shopping.Infrastructure.EfCore.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.ContactInfo)
               .Property(x => x.ContactName)
               .HasColumnName(nameof(Order.ContactInfo.ContactName));

        builder.OwnsOne(x => x.ContactInfo)
               .Property(x => x.Email)
               .HasColumnName(nameof(Order.ContactInfo.Email));

        builder.OwnsOne(x => x.ContactInfo)
               .Property(x => x.Phone)
               .HasColumnName(nameof(Order.ContactInfo.Phone));

        builder.OwnsOne(x => x.ContactInfo)
               .Property(x => x.City)
               .HasColumnName(nameof(Order.ContactInfo.City));

        builder.OwnsOne(x => x.ContactInfo)
               .Property(x => x.PostalCode)
               .HasColumnName(nameof(Order.ContactInfo.PostalCode));

        builder.OwnsOne(x => x.ContactInfo)
               .Property(x => x.CustomerName)
               .HasColumnName(nameof(Order.ContactInfo.CustomerName));

        builder.OwnsOne(x => x.ContactInfo)
               .Property(x => x.State)
               .HasColumnName(nameof(Order.ContactInfo.State));

        builder.OwnsOne(x => x.ContactInfo)
               .Property(x => x.Address)
               .HasColumnName(nameof(Order.ContactInfo.Address));

        builder.HasOne<Buyer>()
               .WithMany()
               .HasForeignKey(x => x.BuyerId);

        builder.Property(x => x.OrderValue)
               .HasPrecision(20, 2)
               .IsRequired();

        builder.Property(x => x.PlacedDate)
               .IsRequired();

        builder.Property(x => x.Status)
               .IsRequired();
    }
}