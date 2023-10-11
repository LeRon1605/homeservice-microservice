using BuildingBlocks.Domain.Models;

namespace Installations.Domain.MaterialAggregate;

public class Material : AggregateRoot
{
    public string Name { get; private set; } = null!;
    
    public Guid ProductTypeId { get; private set; }
    
    public Guid? SellUnitId { get; private set; }
    
    public decimal? SellPrice { get; private set; }
    public decimal? Cost { get; private set; }
    
    public bool IsObsolete { get; private set; }
}