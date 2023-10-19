using Contracts.Domain.ContractAggregate;
using Contracts.Domain.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contracts.Infrastructure.EfCore.Configuration;

public class ContractActionConfiguration : IEntityTypeConfiguration<ContractAction>
{
    public void Configure(EntityTypeBuilder<ContractAction> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.ActionByEmployeeId)
            .IsRequired();

        builder.Property(x => x.Comment)
            .IsRequired(false);

        builder.HasOne<Contract>()
            .WithMany(x => x.Actions)
            .HasForeignKey(x => x.ContractId);

        builder.HasOne<Employee>(x => x.ActionByEmployee)
            .WithMany()
            .HasForeignKey(x => x.ActionByEmployeeId);
    }
}