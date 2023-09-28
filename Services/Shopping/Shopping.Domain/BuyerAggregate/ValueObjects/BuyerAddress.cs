using BuildingBlocks.Domain.Models;

namespace Shopping.Domain.BuyerAggregate.ValueObjects;

public record BuyerAddress(string? FullAddress = null,
                              string? City = null,
                              string? State = null,
                              string? PostalCode = null)
    : ValueObject
{
    public string? FullAddress { get; private set; } = FullAddress;
    public string? City { get; private set; } = City;
    public string? State { get; private set; } = State;
    public string? PostalCode { get; private set; } = PostalCode;
}