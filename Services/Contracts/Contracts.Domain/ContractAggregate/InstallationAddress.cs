using BuildingBlocks.Domain.Models;

namespace Contracts.Domain.ContractAggregate;

public record InstallationAddress : ValueObject
{
    public string? FullAddress { get; private set; }
    public string? City { get; private set; }
    public string? State { get; private set; }
    public string? PostalCode { get; private set; }
    
    public InstallationAddress(
        string? fullAddress = null,
        string? city = null,
        string? state = null,
        string? postalCode = null)
    {
        FullAddress = fullAddress;
        City = city;
        State = state;
        PostalCode = postalCode;
    }
}