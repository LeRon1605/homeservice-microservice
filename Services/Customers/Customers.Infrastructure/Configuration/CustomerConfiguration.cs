using Customers.Domain.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers.Infrastructure.Configuration;

public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.CustomerName)
            .IsRequired().HasMaxLength(20);
    }
}