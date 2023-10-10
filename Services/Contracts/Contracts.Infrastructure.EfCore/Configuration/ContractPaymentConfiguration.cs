using Contracts.Domain.ContractAggregate;
using Contracts.Domain.PaymentMethodAggregate;
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

        builder.HasOne<Contract>()
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.ContractId);

        builder.HasOne<PaymentMethod>()
            .WithMany()
            .HasForeignKey(x => x.PaymentMethodId)
            .IsRequired(false);
    }
}