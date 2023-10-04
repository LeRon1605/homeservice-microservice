using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.BuyerAggregate;
using Shopping.Domain.OrderAggregate;

namespace Shopping.Infrastructure.EfCore.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrderNo)
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);;

        builder.OwnsOne(x => x.ContactInfo)
               .Property(x => x.ContactName)
               .IsRequired(true)
               .HasColumnName(nameof(Order.ContactInfo.ContactName));

        builder.OwnsOne(x => x.ContactInfo)
               .Property(x => x.Email)
               .IsRequired(false)
               .HasColumnName(nameof(Order.ContactInfo.Email));

        builder.OwnsOne(x => x.ContactInfo)
               .Property(x => x.Phone)
               .IsRequired(true)
               .HasColumnName(nameof(Order.ContactInfo.Phone));

        builder.OwnsOne(x => x.ContactInfo)
               .Property(x => x.City)
               .IsRequired(false)
               .HasColumnName(nameof(Order.ContactInfo.City));

        builder.OwnsOne(x => x.ContactInfo)
               .Property(x => x.PostalCode)
               .IsRequired(false)
               .HasColumnName(nameof(Order.ContactInfo.PostalCode));

        builder.OwnsOne(x => x.ContactInfo)
               .Property(x => x.CustomerName)
               .IsRequired(true)
               .HasColumnName(nameof(Order.ContactInfo.CustomerName));

        builder.OwnsOne(x => x.ContactInfo)
               .Property(x => x.State)
               .IsRequired(false)
               .HasColumnName(nameof(Order.ContactInfo.State));

        builder.OwnsOne(x => x.ContactInfo)
               .Property(x => x.Address)
               .IsRequired(false)
               .HasColumnName(nameof(Order.ContactInfo.Address));

        builder.HasOne<Buyer>()
               .WithMany()
               .HasForeignKey(x => x.BuyerId);
        
        builder.Property(x => x.PlacedDate)
               .IsRequired();

        builder.Property(x => x.Status)
               .IsRequired();
    }
}