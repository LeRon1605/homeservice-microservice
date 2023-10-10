using Contracts.Domain.ContractAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contracts.Infrastructure.EfCore.Configuration;

public class ContractPaymentConfiguration : IEntityTypeConfiguration<ContractPayment>
{
    public void Configure(EntityTypeBuilder<ContractPayment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DatePaid)
            .IsRequired();

        builder.Property(x => x.PaidAmount)
            .IsRequired()
            .HasPrecision(20, 2);

        builder.Property(x => x.Surcharge)
            .IsRequired()
            .HasPrecision(20, 2);

        builder.Property(x => x.Reference)
            .IsRequired(false);

        builder.Property(x => x.Comments)
            .IsRequired(false);

        builder.Property(x => x.PaymentMethodName)
            .IsRequired(false);

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(x => x.Contract)
            .WithMany()
            .HasForeignKey(x => x.ContractId);

        builder.HasOne(x => x.PaymentMethod)
            .WithMany()
            .HasForeignKey(x => x.PaymentMethodId)
            .IsRequired(false);
    }
}