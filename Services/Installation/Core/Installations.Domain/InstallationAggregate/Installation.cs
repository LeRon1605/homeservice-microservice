using BuildingBlocks.Domain.Models;
using Installations.Domain.InstallationAggregate.Enums;

namespace Installations.Domain.InstallationAggregate;

public class Installation : AggregateRoot
{
    public Guid ContractId { get; private set; }
    public Guid ContractLineId { get; private set; } 
    public Guid InstallerId { get; private set; }
    public Guid CustomerId { get; init; }
    public Guid? SalespersonId { get; init; }
    public Guid? SupervisorId { get; init; }
    
    public string? InstallationComment { get; set; }
    public string? FloorType { get; set; }
    public double InstallationMetres { get; set; }
    
    public DateTime? EstimatedStartTime { get; private set; }
    public DateTime? EstimatedFinishTime { get; private set; }
    public DateTime? ActualStartTime { get; private set; }
    public DateTime? ActualFinishTime { get; private set; }
    
    public InstallationStatus Status { get; private set; }
    public InstallationAddress InstallationAddress { get; private set; }
    
    
    public ICollection<InstallationItem> Items { get; private set; } = new List<InstallationItem>();
    
    private Installation() { }
    
    public Installation(Guid contractId, 
                        Guid contractLineId, 
                        Guid installerId, 
                        Guid customerId, 
                        Guid? salespersonId, 
                        Guid? supervisorId, 
                        string? installationComment, 
                        string? floorType, 
                        double installationMetres, 
                        DateTime? estimatedStartTime, 
                        DateTime? estimatedFinishTime, 
                        DateTime? actualStartTime, 
                        DateTime? actualFinishTime, 
                        InstallationStatus status,
                        string? fullAddress = null,
                        string? city = null,
                        string? state = null,
                        string? postalCode = null)
    {
        ContractId = contractId;
        ContractLineId = contractLineId;
        InstallerId = installerId;
        CustomerId = customerId;
        SalespersonId = salespersonId;
        SupervisorId = supervisorId;
        InstallationComment = installationComment;
        FloorType = floorType;
        InstallationMetres = installationMetres;
        EstimatedStartTime = estimatedStartTime;
        EstimatedFinishTime = estimatedFinishTime;
        ActualStartTime = actualStartTime;
        ActualFinishTime = actualFinishTime;
        Status = status;
        InstallationAddress = new InstallationAddress(fullAddress, city, state, postalCode);
    }

    public void AddItem(Guid materialId, string materialName, int quantity, Guid unitId, string unitName, decimal cost, decimal sellPrice)
    {
        var item = new InstallationItem(Id, materialId, materialName, quantity, unitId, unitName, cost, sellPrice);
        Items.Add(item);
    }
}