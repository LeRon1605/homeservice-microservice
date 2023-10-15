using BuildingBlocks.Domain.Models;
using Installations.Domain.InstallationAggregate.Enums;

namespace Installations.Domain.InstallationAggregate;

public class Installation : AuditableAggregateRoot
{
    public int No { get; private set; }
    public Guid ContractId { get; private set; }
    public int ContractNo { get; private set; }
    public Guid ContractLineId { get; private set; } 
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } = null!;
    public string? ProductColor { get; private set;}
    
    public Guid InstallerId { get; private set; }
    public Guid CustomerId { get; init; }
    public string CustomerName { get; init; } = null!;
    
    public string? InstallationComment { get; set; }
    public string? FloorType { get; set; }
    public double InstallationMetres { get; set; }
    
    public DateTime? InstallDate { get; private set; }
    public DateTime? EstimatedStartTime { get; private set; }
    public DateTime? EstimatedFinishTime { get; private set; }
    public DateTime? ActualStartTime { get; private set; }
    public DateTime? ActualFinishTime { get; private set; }
    
    public InstallationStatus Status { get; private set; }
    public InstallationAddress InstallationAddress { get; private set; }
    
    
    public ICollection<InstallationItem> Items { get; private set; } = new List<InstallationItem>();
    
    private Installation() { }
    
    public Installation(Guid contractId, 
                        int contractNo,
                        Guid contractLineId, 
                        Guid productId,
                        string productName,
                        string? productColor,
                        Guid installerId, 
                        Guid customerId, 
                        string customerName,
                        string? installationComment, 
                        string? floorType, 
                        double installationMetres, 
                        DateTime? installDate,
                        DateTime? estimatedStartTime, 
                        DateTime? estimatedFinishTime, 
                        DateTime? actualStartTime, 
                        DateTime? actualFinishTime, 
                        string? fullAddress = null,
                        string? city = null,
                        string? state = null,
                        string? postalCode = null)
    {
        ContractId = contractId;
        ContractNo = contractNo;
        ContractLineId = contractLineId;
        ProductId = productId;
        ProductName = productName;
        ProductColor = productColor;
        
        InstallerId = installerId;
        CustomerId = customerId;
        CustomerName = customerName;
        
        InstallationComment = installationComment;
        FloorType = floorType;
        InstallationMetres = installationMetres;
        
        if (installDate is not null)
        {
            InstallDate = installDate;
            if (estimatedStartTime != null && estimatedFinishTime != null)
            {
                EstimatedStartTime = installDate.Value.Date + estimatedStartTime.Value.TimeOfDay;
                EstimatedFinishTime = installDate.Value.Date + estimatedFinishTime.Value.TimeOfDay;
            }
            
            if (actualStartTime != null && actualFinishTime != null)
            {
                ActualStartTime = installDate.Value.Date + actualStartTime.Value.TimeOfDay;
                ActualFinishTime = installDate.Value.Date + actualFinishTime.Value.TimeOfDay;
            }
        }
        
        Status = InstallationStatus.Pending;
        InstallationAddress = new InstallationAddress(fullAddress, city, state, postalCode);
    }

    public void AddItem(Guid materialId, string materialName, int quantity, Guid unitId, string unitName, decimal cost, decimal sellPrice)
    {
        var item = new InstallationItem(Id, materialId, materialName, quantity, unitId, unitName, cost, sellPrice);
        Items.Add(item);
    }
    
    public void RemoveItems()
    {
        Items.Clear();
    }

    public void Update(Guid contractLineId,
                       Guid productId,
                       string productName,
                       string? productColor,
                       Guid installerId,
                       string? installationComment,
                       string? floorType,
                       double installationMetres,
                       DateTime? installDate,
                       DateTime? estimatedStartTime,
                       DateTime? estimatedFinishTime,
                       DateTime? actualStartTime,
                       DateTime? actualFinishTime,
                       string? fullAddress = null,
                       string? city = null,
                       string? state = null,
                       string? postalCode = null)
    {
        ContractLineId = contractLineId;
        ProductId = productId;
        ProductName = productName;
        ProductColor = productColor;
        
        InstallerId = installerId;
        
        InstallationComment = installationComment;
        FloorType = floorType;
        InstallationMetres = installationMetres;
        
        if (installDate is not null)
        {
            InstallDate = installDate;
            if (estimatedStartTime != null && estimatedFinishTime != null)
            {
                EstimatedStartTime = installDate.Value.Date + estimatedStartTime.Value.TimeOfDay;
                EstimatedFinishTime = installDate.Value.Date + estimatedFinishTime.Value.TimeOfDay;
            }
            
            if (actualStartTime != null && actualFinishTime != null)
            {
                ActualStartTime = installDate.Value.Date + actualStartTime.Value.TimeOfDay;
                ActualFinishTime = installDate.Value.Date + actualFinishTime.Value.TimeOfDay;
            }
        }
        
        InstallationAddress = new InstallationAddress(fullAddress, city, state, postalCode);     
    }
    
    public void UpdateStatus(InstallationStatus status)
    {
        Status = status;
    }
    
    public void UpdateAddress(string? fullAddress = null,
                              string? city = null,
                              string? state = null,
                              string? postalCode = null)
    {
        InstallationAddress = new InstallationAddress(fullAddress, city, state, postalCode);
    }
}