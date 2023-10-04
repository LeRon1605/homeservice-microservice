using BuildingBlocks.Domain.Models;
using Shopping.Domain.OrderAggregate.Events;
using Shopping.Domain.OrderAggregate.Exceptions;

namespace Shopping.Domain.OrderAggregate;

public class Order : AggregateRoot
{
    public int OrderNo { get; private set; }
    public Guid BuyerId { get; private set; }
    public OrderContactInfo ContactInfo { get; private set; }
    public DateTime PlacedDate { get; private set; }
    public OrderStatus Status { get; private set; }

    private readonly List<OrderLine> _orderLines;
    public IReadOnlyCollection<OrderLine> OrderLines => _orderLines;

    public Order(
        Guid buyerId,
        string contactName, 
        string? email, 
        string phone, 
        string? address, 
        string? city, 
        string? state, 
        string? postalCode)
    {
        BuyerId = buyerId;
        ContactInfo = new OrderContactInfo("Retail customer", contactName, email, phone, address, city, state, postalCode);
        PlacedDate = DateTime.UtcNow;
        Status = OrderStatus.Pending;

        _orderLines = new List<OrderLine>();
        
        AddDomainEvent(new OrderAddedDomainEvent(this));
    }

    public void Reject()
    {
        if (IsProcessedOrder())
        {
            throw new OrderProcessedException(Id);
        }

        Status = OrderStatus.Rejected;
        AddDomainEvent(new OrderRejectDomainEvent(this));
    }

    public void Finish()
    {
        if (IsProcessedOrder())
        {
            throw new OrderProcessedException(Id);
        }

        Status = OrderStatus.Finished;
    }

    private bool IsProcessedOrder()
    {
        return Status is OrderStatus.Finished or OrderStatus.Rejected;
    }

    public void AddOrderLine(
        Guid productId, 
        string productName,
        string? unitName,
        string? color, 
        int quantity,
        decimal cost)
    {
        if (!string.IsNullOrWhiteSpace(color))
        {
            var isExisting = _orderLines.Any(x => x.ProductId == productId && x.Color == color);

            if (isExisting)
            {
                throw new ProductAlreadyAddedToOrderException(productId, color);
            }   
        }

        var orderLine = new OrderLine(productId, productName, Id, unitName, color, quantity, cost);
        _orderLines.Add(orderLine);
    }

    private Order()
    {
        
    }
}