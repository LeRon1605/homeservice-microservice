using BuildingBlocks.Domain.Models;

namespace Contracts.Domain.CustomerAggregate;

public record CustomerAddress(string? FullAddress = null,
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