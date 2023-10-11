using Installations.Domain.InstallationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Installations.Infrastructure.Configurations;

public class InstallationItemConfiguration : IEntityTypeConfiguration<InstallationItem>
{
    public void Configure(EntityTypeBuilder<InstallationItem> builder)
    {
        builder.HasOne(i => i.Installation)
            .WithMany(i => i.Items)
            .HasForeignKey(i => i.InstallationId);
    }
}