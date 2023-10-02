using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

namespace Contracts.Domain.ContractAggregate;

public class ContractLine : Entity
{
    public Guid ContractId { get; private set; }
    
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    
    public Guid UnitId { get; private set; }
    public string UnitName { get; private set; }
    
    public string? Color { get; private set; }
    public int Quantity { get; private set; }
    public decimal Cost { get; private set; }
    public decimal SellPrice { get; private set; }
    
    public ContractLine(
        Guid productId, 
        string productName,
        Guid contractId,
        Guid unitId,
        string unitName,
        string? color, 
        int quantity,
        decimal cost,
        decimal sellPrice)
    {
        ProductId = Guard.Against.NullOrEmpty(productId, nameof(ProductId));
        ProductName = Guard.Against.NullOrEmpty(productName, nameof(ProductName));
        ContractId = Guard.Against.Null(contractId);
        UnitId = Guard.Against.Null(unitId, nameof(UnitId));
        UnitName = unitName;
        Color = color;
        Quantity = Guard.Against.NegativeOrZero(quantity, nameof(Quantity));
        Cost = Guard.Against.Negative(cost, nameof(Cost));
        SellPrice = Guard.Against.Negative(sellPrice, nameof(SellPrice));
    }
}