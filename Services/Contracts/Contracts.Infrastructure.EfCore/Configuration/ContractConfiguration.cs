using Contracts.Domain.ContractAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contracts.Infrastructure.EfCore.Configuration;

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.No)
            .ValueGeneratedOnAdd()
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        builder.Property(x => x.Balance)
            .HasPrecision(20, 2)
            .IsRequired();

        builder.Property(x => x.CustomerNote)
            .IsRequired(false);

        builder.Property(x => x.PurchaseOrderNo)
            .IsRequired(false);

        builder.Property(x => x.InvoiceNo)
            .IsRequired(false);

        builder.Property(x => x.InvoiceDate)
            .IsRequired(false);

        builder.Property(x => x.EstimatedInstallationDate)
            .IsRequired(false);
        
        builder.Property(x => x.ActualInstallationDate)
            .IsRequired(false);

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
        
        builder.Property(x => x.QuotedAt)
            .IsRequired();

        builder.Property(x => x.SoldAt)
            .IsRequired(false);

        builder.HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId);

        builder.HasOne(x => x.SalePerson)
            .WithMany()
            .HasForeignKey(x => x.SalePersonId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Supervisor)
            .WithMany()
            .HasForeignKey(x => x.SupervisorId)
            .IsRequired(false);

        builder.HasOne(x => x.CustomerServiceRep)
            .WithMany()
            .HasForeignKey(x => x.CustomerServiceRepId)
            .IsRequired(false);
    }
}