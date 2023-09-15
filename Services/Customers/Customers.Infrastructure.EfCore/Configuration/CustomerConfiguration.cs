using Customers.Domain.Constants;
using Customers.Domain.CustomerAggregate;
using Customers.Domain.CustomerAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers.Infrastructure.EfCore.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(StringLength.CustomerName);

        builder.Property(x => x.ContactName)
               .IsRequired(false);

        builder.Property(x => x.Email)
               .IsRequired(false);

        builder.Property(x => x.Phone)
               .IsRequired(false);

        builder.OwnsOne(x => x.Address,
               address =>
               {
                      address.Property(x => x.FullAddress)
                             .HasColumnName("Address");
                      
                      address.Property(x => x.City)
                             .HasColumnName("City");
                      
                      address.Property(x => x.State)
                             .HasColumnName("State");
                      
                      address.Property(x => x.PostalCode)
                             .HasColumnName("PostalCode");
               });
    }
}