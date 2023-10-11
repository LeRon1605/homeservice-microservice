using Installations.Domain.Constants;
using Installations.Domain.MaterialAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Installations.Infrastructure.Configurations;

public class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.Property(m => m.Name)
            .HasMaxLength(StringLength.Name)
            .IsRequired();

        builder.Property(m => m.ProductTypeId)
            .IsRequired();

        builder.Property(m => m.SellUnitId)
            .IsRequired(false);

        builder.Property(m => m.SellPrice)
            .IsRequired(false);

        builder.Property(m => m.Cost)
            .IsRequired(false);

        builder.Property(m => m.IsObsolete)
            .IsRequired();
    }
}