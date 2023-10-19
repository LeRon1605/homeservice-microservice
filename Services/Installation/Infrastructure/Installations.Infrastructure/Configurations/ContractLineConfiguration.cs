using Installations.Domain.ContractAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Installations.Infrastructure.Configurations;

public class ContractLineConfiguration : IEntityTypeConfiguration<ContractLine>
{
    public void Configure(EntityTypeBuilder<ContractLine> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Color)
            .IsRequired(false);

        builder.Property(x => x.ProductName)
            .IsRequired();

        builder.HasOne<Contract>()
            .WithMany(x => x.ContractLines)
            .HasForeignKey(x => x.ContractId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}