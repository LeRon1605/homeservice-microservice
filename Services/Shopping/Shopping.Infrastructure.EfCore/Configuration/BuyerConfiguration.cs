using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.BuyerAggregate;

namespace Shopping.Infrastructure.EfCore.Configuration;

public class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
{
    public void Configure(EntityTypeBuilder<Buyer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName)
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