using BuildingBlocks.Domain.Models;

namespace Installations.Domain.InstallationAggregate;

public class InstallationItem : AuditableEntity
{
    public Guid InstallationId { get; private set; } 
    
    public Guid MaterialId { get; private set; }
    public string MaterialName { get; private set; } = null!;
    
    public int Quantity { get; private set; }
    
    public Guid? UnitId { get; private set; }
    public string? UnitName { get; private set; }
    
    public decimal? Cost { get; private set; }
    public decimal SellPrice { get; private set; }
    
    public Installation Installation { get; private set; } = null!;
    
    private InstallationItem() { }
    
    public InstallationItem(Guid installationId, 
                            Guid materialId, 
                            string materialName,
                            int quantity, 
                            Guid? unitId, 
                            string? unitName,
                            decimal? cost, 
                            decimal sellPrice)
    {
        InstallationId = installationId;
        MaterialId = materialId;
        MaterialName = materialName;
        Quantity = quantity;
        UnitId = unitId;
        UnitName = unitName;
        Cost = cost;
        SellPrice = sellPrice;
    }
}