using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Models;
using Shopping.Domain.Exceptions;
using Shopping.Domain.ProductAggregate;

namespace Shopping.Domain.OrderAggregate;

public class Order : AggregateRoot
{
    public string OrderNo { get; private set; }
    public OrderContactInfo ContactInfo { get; private set; }
    public decimal OrderValue { get; private set; }
    public DateTime PlacedDate { get; private set; }
    public OrderStatus Status { get; private set; }

    private readonly List<OrderLine> _orderLines;
    public IReadOnlyCollection<OrderLine> OrderLines => _orderLines;

    public Order(
        string orderNo,
        Guid buyerId,
        string customerName, 
        string contactName, 
        string email, 
        string phone, 
        string address, 
        string city, 
        string state, 
        string postalCode,
        decimal orderValue)
    {
        OrderNo = Guard.Against.NullOrWhiteSpace(orderNo, nameof(OrderNo));
        ContactInfo = new OrderContactInfo(buyerId, customerName, contactName, email, phone, address, city, state, postalCode);
        OrderValue = orderValue;
        PlacedDate = DateTime.UtcNow;
        Status = OrderStatus.Pending;

        _orderLines = new List<OrderLine>();
    }

    public void Reject()
    {
        if (IsProcessedOrder())
        {
            throw new OrderProcessedException(Id);
        }

        Status = OrderStatus.Rejected;
    }

    private bool IsProcessedOrder()
    {
        return Status is OrderStatus.Finished or OrderStatus.Rejected;
    }

    public void AddOrderLine(
        Guid productId, 
        string productName,
        Guid orderId,
        string color, 
        int quantity, 
        double tax, 
        decimal cost,
        string? unitName)
    {
        // Todo: validate
        var orderLine = new OrderLine(productId, productName, orderId, unitName, color, quantity, tax, cost);
        _orderLines.Add(orderLine);
    }

    private Order()
    {
        
    }
}