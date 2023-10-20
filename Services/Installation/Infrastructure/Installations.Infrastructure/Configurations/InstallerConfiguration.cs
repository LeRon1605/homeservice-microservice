using Installations.Domain.Constants;
using Installations.Domain.InstallerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Installations.Infrastructure.Configurations;

public class InstallerConfiguration : IEntityTypeConfiguration<Installer>
{
    public void Configure(EntityTypeBuilder<Installer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName)
            .HasMaxLength(StringLength.Name).IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(StringLength.Email).IsRequired();

        builder.Property(x => x.Phone)
            .HasMaxLength(StringLength.Phone);
    }
}