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
    
    internal Material(
        string materialCode,
        string name,
        Guid productTypeId,
        Guid? sellUnitId = null,
        decimal? sellPrice = null,
        decimal? cost = null,
        bool isObsolete = false)
    {
        SetMaterialCode(materialCode);
        SetName(name);
        SetProductType(productTypeId);
        SetSellUnit(sellUnitId);
        SetSellPrice(sellPrice);
        SetCost(cost);
        IsObsoleteYet(isObsolete);
    }
    
    internal void SetMaterialCode(string materialCode)
    {
        MaterialCode = Guard.Against.NullOrWhiteSpace(materialCode, nameof(MaterialCode));
    }

    internal void SetName(string name)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
    }

    internal void SetProductType(Guid productTypeId)
    {
        ProductTypeId = Guard.Against.Default(productTypeId, nameof(ProductTypeId));
    }
    
    internal void SetSellUnit(Guid? sellUnitId)
    {
        SellUnitId = sellUnitId;
    }
    
    internal void SetSellPrice(decimal? sellPrice)
    {
        SellPrice = sellPrice;
    }
    
    internal void SetCost(decimal? cost)
    {
        Cost = cost;
    }
    
    internal void IsObsoleteYet(bool isObsolete)
    {
        IsObsolete = isObsolete;
    }
}