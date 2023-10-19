using BuildingBlocks.Domain.Models;

namespace Installations.Domain.MaterialAggregate;

public class Material : AuditableAggregateRoot
{
    public string Name { get; private set; } = null!;
    
    public Guid ProductTypeId { get; private set; }
    
    public Guid? SellUnitId { get; private set; }
    public string? SellUnitName { get; private set; }
    
    public decimal SellPrice { get; private set; }
    public decimal? Cost { get; private set; }
    
    public bool IsObsolete { get; private set; }
    
    private Material()
    {
    }
    
    public Material(Guid id, 
                    string name, 
                    Guid productTypeId, 
                    Guid? sellUnitId, 
                    string? sellUnitName, 
                    decimal sellPrice, 
                    decimal? cost, 
                    bool isObsolete)
    {
        Id = id;
        Name = name;
        ProductTypeId = productTypeId;
        SellUnitId = sellUnitId;
        SellUnitName = sellUnitName;
        SellPrice = sellPrice;
        Cost = cost;
        IsObsolete = isObsolete;
    }
    
    public void Update(string name, Guid productTypeId, Guid? sellUnitId, string? sellUnitName, decimal sellPrice, decimal? cost, bool isObsolete)
    {
        Name = name;
        ProductTypeId = productTypeId;
        SellUnitId = sellUnitId;
        SellUnitName = sellUnitName;
        SellPrice = sellPrice;
        Cost = cost;
        IsObsolete = isObsolete;
    }
}