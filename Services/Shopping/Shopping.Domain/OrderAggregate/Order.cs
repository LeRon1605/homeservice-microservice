using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

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
    public ProductStatus Status { get; private set; }

    public Order(string orderNo, string contactName, Guid buyerId,string phoneNumber, string? emailAddress, decimal orderValue, DateTime placedDate, ProductStatus status)
    {
        OrderNo = Guard.Against.NullOrWhiteSpace(orderNo, nameof(OrderNo));
        ContactName = Guard.Against.NullOrWhiteSpace(contactName,nameof(ContactName));
        BuyerId = buyerId;
        PhoneNumber = Guard.Against.NullOrWhiteSpace(phoneNumber, nameof(PhoneNumber));
        EmailAddress = emailAddress;
        OrderValue = orderValue;
        PlacedDate = placedDate;
        Status = status;
    }
}