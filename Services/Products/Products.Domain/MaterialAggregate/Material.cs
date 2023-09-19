using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;
using Products.Domain.ProductTypeAggregate;
using Products.Domain.ProductUnitAggregate;

namespace Products.Domain.MaterialAggregate;

public class Material : AggregateRoot
{
    public string MaterialCode { get; private set; }
    public string Name { get; private set; }
    
    public Guid ProductTypeId { get; private set; }
    public ProductType? Type { get; private set; }
    
    public Guid? SellUnitId { get; private set; }
    public ProductUnit? SellUnit { get; private set; }
    
    public decimal? SellPrice { get; private set; }
    public decimal? Cost { get; private set; }
    
    public bool IsObsolete { get; private set; }
    
    public Material(
        string materialCode,
        string name,
        Guid productTypeId,
        Guid? sellUnitId = null,
        decimal? sellPrice = null,
        decimal? cost = null,
        bool isObsolete = false)
    {
        MaterialCode = Guard.Against.NullOrWhiteSpace(materialCode, nameof(MaterialCode));
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
        ProductTypeId = Guard.Against.Default(productTypeId, nameof(ProductTypeId));
        SellUnitId = sellUnitId;
        SellPrice = sellPrice;
        Cost = cost;
        IsObsolete = isObsolete;
    }
}