using BuildingBlocks.Domain.Models;
using Shopping.Domain.ProductAggregate;

namespace Shopping.Domain.OrderAggregate;

public record OrderLine : ValueObject
{
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }
    public Guid OrderId { get; private set; }
    public Order Order { get; private set; }
    public string Color { get; private set; }
    public int Quantity { get; private set; }
    public double Tax { get; private set; }
    public decimal Cost { get; private set; }

    public OrderLine(Guid productId, Guid orderId,
                    string color, int quantity, double tax, decimal cost)
    {
        ProductId = productId;
        OrderId = orderId;
        Color = color;
        Quantity = quantity;
        Tax = tax;
        Cost = cost;
    }
}