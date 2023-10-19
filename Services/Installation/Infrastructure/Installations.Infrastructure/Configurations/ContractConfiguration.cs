using Installations.Domain.ContractAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Installations.Infrastructure.Configurations;

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.InstallationAddress,
            installationAddress =>
            {
                installationAddress.Property(x => x.FullAddress)
                    .IsRequired(false);

                installationAddress.Property(x => x.City)
                    .IsRequired(false);

                installationAddress.Property(x => x.State)
                    .IsRequired(false);

                installationAddress.Property(x => x.PostalCode)
                    .IsRequired(false);
            });
    }
}