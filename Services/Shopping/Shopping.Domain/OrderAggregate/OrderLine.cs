using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

namespace Shopping.Domain.OrderAggregate;

public class OrderLine : Entity
{
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string? UnitName { get; private set; }
    public string? Color { get; private set; }
    public int Quantity { get; private set; }
    public decimal Cost { get; private set; }
    
    public Order Order { get; private set; } = null!;

    public OrderLine(
        Guid productId, 
        string productName,
        Guid orderId,
        string? unitName,
        string? color, 
        int quantity,
        decimal cost)
    {
        ProductId = Guard.Against.NullOrEmpty(productId, nameof(ProductId));
        ProductName = Guard.Against.NullOrEmpty(productName, nameof(ProductName));
        OrderId = Guard.Against.Null(orderId);
        UnitName = unitName;
        Color = color;
        Quantity = Guard.Against.NegativeOrZero(quantity, nameof(Quantity));
        Cost = Guard.Against.Negative(cost, nameof(Cost));
    }
}