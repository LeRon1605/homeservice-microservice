using BuildingBlocks.Domain.Models;

namespace Shopping.Domain.OrderAggregate;

public record OrderLine : ValueObject
{
    public Guid OrderId { get; private set; }
    
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    
    public string? UnitName { get; private set; }
    public string Color { get; private set; }
    public int Quantity { get; private set; }
    public double Tax { get; private set; }
    public decimal Cost { get; private set; }

    public OrderLine(
        Guid productId, 
        string productName,
        Guid orderId,
        string? unitName,
        string color, 
        int quantity, 
        double tax, 
        decimal cost)
    {
        ProductId = productId;
        ProductName = productName;
        OrderId = orderId;
        UnitName = unitName;
        Color = color;
        Quantity = quantity;
        Tax = tax;
        Cost = cost;
    }
}