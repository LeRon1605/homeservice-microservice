using Employees.Domain.Constants;
using Employees.Domain.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employees.Infrastructure.Configuration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.EmployeeCode)
            .IsRequired();

        builder.Property(x => x.FullName)
            .HasMaxLength(StringLength.FullName).IsRequired();

        builder.Property(x => x.Position)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(StringLength.Email).IsRequired();

        builder.Property(x => x.Phone)
            .HasMaxLength(StringLength.Phone);

        builder.Property(x => x.RoleId)
            .IsRequired();

        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Status).IsRequired();
    }
}