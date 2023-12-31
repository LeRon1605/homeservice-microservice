using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;
using Contracts.Domain.ContractAggregate.Events;
using Contracts.Domain.ContractAggregate.Exceptions;
using Contracts.Domain.CustomerAggregate;
using Contracts.Domain.EmployeeAggregate;

namespace Contracts.Domain.ContractAggregate;

public class Contract : AuditableAggregateRoot
{
    public int No { get; private set; }
    
    public decimal Balance { get; private set; }
    public string? CustomerNote { get; private set; }
    
    public Guid SalePersonId { get; private set; }
    public Employee? SalePerson { get; private set; }
    
    public Guid? SupervisorId { get; private set; }
    public Employee? Supervisor { get; private set; }
    
    public Guid? CustomerServiceRepId { get; private set; }
    public Employee? CustomerServiceRep { get; private set; }
    
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
    public List<ContractPayment> Payments { get; private set; }
    public List<ContractAction> Actions { get; private set; }

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
        string? installationPostalCode,
        DateTime? soldAt,
        ContractStatus status)
    {
        Balance = 0;
        CustomerId = Guard.Against.Null(customerId, nameof(CustomerId));
        CustomerNote = customerNote;
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
        SoldAt = soldAt;
        Status = status;

        Items = new List<ContractLine>();
        Payments = new List<ContractPayment>();
        Actions = new List<ContractAction>();
    }
    
    public void UpdateContractInfo(
        string? customerNote,
        int? purchaseOrderNo,
        int? invoiceNo,
        DateTime? invoiceDate,
        DateTime? estimatedInstallationDate,
        DateTime? actualInstallationDate,
        string? fullInstallationAddress,
        string? installationCity,
        string? installationState,
        string? installationPostalCode,
        ContractStatus status)
    {
        Balance = 0;
        CustomerNote = customerNote;
        PurchaseOrderNo = purchaseOrderNo;
        InvoiceNo = invoiceNo;
        InvoiceDate = invoiceDate;
        EstimatedInstallationDate = estimatedInstallationDate;
        ActualInstallationDate = actualInstallationDate;
        InstallationAddress = new InstallationAddress(fullInstallationAddress, installationCity, installationState, installationPostalCode);
        Status = status;
    }

    public void UpdateCustomer(Guid customerId)
    {
        CustomerId = Guard.Against.Null(customerId, nameof(CustomerId));
    }
    
    public void UpdateSalePerson(Guid salePersonId)
    {
        SalePersonId = Guard.Against.Null(salePersonId, nameof(SalePersonId));
    }
    
    public void UpdateSupervisor(Guid supervisorId)
    {
        SupervisorId = supervisorId;
    }
    
    public void UpdateCustomerServiceRep(Guid customerServiceRepId)
    {
        CustomerServiceRepId = customerServiceRepId;
    }
    
    public ContractLine AddContractLine(
        Guid productId, 
        string productName,
        Guid unitId,
        string unitName,
        Guid? taxId,
        string? taxName,
        double? taxValue,
        string? color, 
        int quantity,
        decimal cost,
        decimal sellPrice)
    {
        if (Items.Any(x => x.ProductId == productId && x.UnitId == unitId && (string.IsNullOrWhiteSpace(color) || x.Color == color)))
        {
            throw new ContractLineExistedException(productId, unitId, color);
        }
        
        var contractLine = new ContractLine(
            productId, 
            productName, 
            Id, 
            unitId, 
            unitName,
            taxId,
            taxName,
            taxValue,
            color, 
            quantity, 
            cost, 
            sellPrice);
        
        Items.Add(contractLine);
        AddDomainEvent(new NewContractItemAddedDomainEvent(this, contractLine));
        
        return contractLine;
    }

    public void RemoveItem(Guid id)
    {
        var item = Items.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            throw new ContractLineNotFoundException(id);
        }
        
        Items.Remove(item);
        AddDomainEvent(new NewContractItemAddedDomainEvent(this, item));
    }

    public ContractLine UpdateItem(
        Guid id,
        Guid productId, 
        string productName,
        Guid unitId,
        string unitName,
        Guid? taxId,
        string? taxName,
        double? taxValue,
        string? color, 
        int quantity,
        decimal cost,
        decimal sellPrice)
    {
        var item = Items.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            throw new ContractLineNotFoundException(id);
        }
        
        if (Items.Any(x => x.Id != id && x.ProductId == productId && x.UnitId == unitId && (string.IsNullOrWhiteSpace(color) || x.Color == color)))
        {
            throw new ContractLineExistedException(productId, unitId, color);
        }
        
        item.Update(
            productId, 
            productName, 
            unitId, 
            unitName,
            taxId,
            taxName,
            taxValue,
            color, 
            quantity, 
            cost, 
            sellPrice);
        
        AddDomainEvent(new ContractItemUpdatedDomainEvent(this, item));

        return item;
    }

    public ContractPayment AddPayment(
        DateTime datePaid, 
        decimal paidAmount, 
        decimal? surcharge, 
        string? reference, 
        string? comments,
        Guid? paymentMethodId,
        string? paymentMethodName)
    {
        var contractPayment = new ContractPayment(
            Id, 
            datePaid, 
            paidAmount, 
            surcharge, 
            reference, 
            comments,
            paymentMethodId,
            paymentMethodName);
        
        Payments.Add(contractPayment);
        AddDomainEvent(new NewContractPaymentAddedDomainEvent(this, contractPayment));

        return contractPayment;
    }

    public ContractPayment UpdatePayment(
        Guid id, 
        DateTime datePaid, 
        decimal paidAmount, 
        decimal? surcharge, 
        string? reference, 
        string? comments,
        Guid? paymentMethodId,
        string? paymentMethodName)
    {
        var payment = Payments.FirstOrDefault(x => x.Id == id);
        if (payment == null)
        {
            throw new ContractPaymentNotFoundException(id);
        }
        
        payment.Update(datePaid, paidAmount, surcharge, reference, comments, paymentMethodId, paymentMethodName);
        AddDomainEvent(new ContractPaymentUpdatedDomainEvent(this, payment));
        
        return payment;
    }

    public void RemovePayment(Guid id)
    {
        var payment = Payments.FirstOrDefault(x => x.Id == id);
        if (payment == null)
        {
            throw new ContractPaymentNotFoundException(id);
        }
        
        Payments.Remove(payment);
        AddDomainEvent(new ContractPaymentDeletedDomainEvent(this, payment));
    }
    
    public ContractAction AddAction(string name, DateTime date, string? comment, Guid actionByEmployeeId)
    {
        var contractAction = new ContractAction(Id, name, date, actionByEmployeeId, comment);
        Actions.Add(contractAction);

        return contractAction;
    }

    public ContractAction UpdateAction(Guid id, string name, DateTime date, Guid actionByEmployeeId, string? comment)
    {
        var action = Actions.FirstOrDefault(x => x.Id == id);
        if (action == null)
        {
            throw new ContractActionNotFoundException(id);
        }
        
        action.Update(name, date, actionByEmployeeId, comment);

        return action;
    }

    public void RemoveAction(Guid id)
    {
        var action = Actions.FirstOrDefault(x => x.Id == id);
        if (action == null)
        {
            throw new ContractActionNotFoundException(id);
        }

        Actions.Remove(action);
    }

    private Contract()
    {
        
    }
}