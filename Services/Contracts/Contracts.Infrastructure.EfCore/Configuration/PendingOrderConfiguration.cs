using Contracts.Domain.PendingOrdersAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contracts.Infrastructure.EfCore.Configuration;

public class PendingOrderConfiguration : IEntityTypeConfiguration<PendingOrder>
{
    public void Configure(EntityTypeBuilder<PendingOrder> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BuyerId)
            .IsRequired();

        builder.OwnsOne(x => x.ContactInfo)
            .Property(x => x.ContactName)
            .IsRequired(true)
            .HasColumnName(nameof(PendingOrder.ContactInfo.ContactName));

        builder.OwnsOne(x => x.ContactInfo)
            .Property(x => x.Email)
            .IsRequired(false)
            .HasColumnName(nameof(PendingOrder.ContactInfo.Email));

        builder.OwnsOne(x => x.ContactInfo)
            .Property(x => x.Phone)
            .IsRequired(true)
            .HasColumnName(nameof(PendingOrder.ContactInfo.Phone));

        builder.OwnsOne(x => x.ContactInfo)
            .Property(x => x.City)
            .IsRequired(false)
            .HasColumnName(nameof(PendingOrder.ContactInfo.City));

        builder.OwnsOne(x => x.ContactInfo)
            .Property(x => x.PostalCode)
            .IsRequired(false)
            .HasColumnName(nameof(PendingOrder.ContactInfo.PostalCode));

        builder.OwnsOne(x => x.ContactInfo)
            .Property(x => x.CustomerName)
            .IsRequired(true)
            .HasColumnName(nameof(PendingOrder.ContactInfo.CustomerName));

        builder.OwnsOne(x => x.ContactInfo)
            .Property(x => x.State)
            .IsRequired(false)
            .HasColumnName(nameof(PendingOrder.ContactInfo.State));

        builder.OwnsOne(x => x.ContactInfo)
            .Property(x => x.Address)
            .IsRequired(false)
            .HasColumnName(nameof(PendingOrder.ContactInfo.Address));
    }
}