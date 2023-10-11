using Contracts.Domain.Constants;
using Contracts.Domain.MaterialAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contracts.Infrastructure.EfCore.Configuration;

public class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(StringLength.Name);

        builder.Property(p => p.IsObsolete);
    }
}