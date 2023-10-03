using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;
using Contracts.Domain.CustomerAggregate;

namespace Contracts.Domain.ContractAggregate;

public class Contract : AggregateRoot
{
    public int No { get; private set; }
    
    public decimal Balance { get; private set; }
    public string? CustomerNote { get; private set; }
    
    public Guid SalePersonId { get; private set; }
    public Guid? SupervisorId { get; private set; }
    public Guid? CustomerServiceRepId { get; private set; }
    
    public int? PurchaseOrderNo { get; private set; }
    
    public int? InvoiceNo { get; private set; }
    public DateTime? InvoiceDate { get; private set; }
    
    public DateTime? EstimatedInstallationDate { get; private set; }
    public DateTime? ActualInstallationDate { get; private set; }
    public InstallationAddress InstallationAddress { get; private set; }
    
    public DateTime QuotedAt { get; private set; }
    public DateTime? SoldAt { get; private set; }
    
    public Guid CustomerId { get; private set; }
    public Customer? Customer { get; private set; }
    
    public ContractStatus Status { get; private set; }
    
    public List<ContractLine> Items { get; private set; }

    public Contract(
        Guid customerId,
        string? customerNote, 
        Guid salePersonId, 
        Guid? supervisorId, 
        Guid? customerServiceRepId,
        int? purchaseOrderNo,
        int? invoiceNo,
        DateTime? invoiceDate,
        DateTime? estimatedInstallationDate,
        DateTime? actualInstallationDate,
        string? fullInstallationAddress,
        string? installationCity,
        string? installationState,
        string? installationPostalCode
        )
    {
        Balance = 0;
        CustomerId = Guard.Against.Null(customerId, nameof(CustomerId));
        CustomerNote = Guard.Against.NullOrWhiteSpace(customerNote, nameof(CustomerNote));
        SalePersonId = Guard.Against.Null(salePersonId, nameof(SalePersonId));
        SupervisorId = supervisorId;
        CustomerServiceRepId = customerServiceRepId;
        PurchaseOrderNo = purchaseOrderNo;
        InvoiceNo = invoiceNo;
        InvoiceDate = invoiceDate;
        EstimatedInstallationDate = estimatedInstallationDate;
        ActualInstallationDate = actualInstallationDate;
        InstallationAddress = new InstallationAddress(fullInstallationAddress, installationCity, installationState, installationPostalCode);
        QuotedAt = DateTime.UtcNow;
        SoldAt = null;
        Status = ContractStatus.Quotation;

        Items = new List<ContractLine>();
    }

    private Contract()
    {
        
    }
}