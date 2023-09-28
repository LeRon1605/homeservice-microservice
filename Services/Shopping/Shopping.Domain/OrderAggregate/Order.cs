using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Models;
using Shopping.Domain.Exceptions;
using Shopping.Domain.ProductAggregate;

namespace Shopping.Domain.OrderAggregate;

public class Order : AggregateRoot
{
    public string OrderNo { get; private set; }
    public string ContactName { get; private set; }
    public Guid BuyerId { get; private set; }
    public string PhoneNumber { get; private set; }
    public string? EmailAddress { get; private set; }
    public decimal OrderValue { get; private set; }
    public DateTime PlacedDate { get; private set; }
    public OrderStatus Status { get; private set; }

    private readonly List<OrderLine> _orderLines;
    public IReadOnlyCollection<OrderLine> OrderLines => _orderLines;

    //public ICollection<OrderLine> OrderLines { get; set; }

    public Order(
        string orderNo,
        string contactName,
        Guid buyerId,
        string phoneNumber,
        string? emailAddress,
        decimal orderValue)
    {
        OrderNo = Guard.Against.NullOrWhiteSpace(orderNo, nameof(OrderNo));
        ContactName = Guard.Against.NullOrWhiteSpace(contactName, nameof(ContactName));
        PhoneNumber = Guard.Against.NullOrWhiteSpace(phoneNumber, nameof(PhoneNumber));
        BuyerId = buyerId;
        EmailAddress = emailAddress;
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

    public void AddOrderLine(Guid productId, Guid orderId,
        string color, int quantity, double tax, decimal cost)
    {
        
        var orderLine = new OrderLine(productId, orderId, color, quantity, tax, cost);
        _orderLines.Add(orderLine);
    }
}