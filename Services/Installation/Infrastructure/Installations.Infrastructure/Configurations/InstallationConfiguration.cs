using Installations.Domain.Constants;
using Installations.Domain.InstallationAggregate;
using Installations.Domain.InstallationAggregate.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Installations.Infrastructure.Configurations;

public class InstallationConfiguration : IEntityTypeConfiguration<Installation>
{
    public void Configure(EntityTypeBuilder<Installation> builder)
    {
        builder.Property(i => i.FloorType).HasMaxLength(StringLength.FloorType);
        
        builder.Property(i => i.InstallationComment).HasMaxLength(StringLength.Comment);
        
        builder.Property(i => i.InstallationMetres).HasPrecision(18, 2);
        
        builder.Property(i => i.Status)
            .HasConversion(v => v.ToString(), 
                v => (InstallationStatus) Enum.Parse(typeof(InstallationStatus), v));
        
        builder.OwnsOne(x => x.InstallationAddress,
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