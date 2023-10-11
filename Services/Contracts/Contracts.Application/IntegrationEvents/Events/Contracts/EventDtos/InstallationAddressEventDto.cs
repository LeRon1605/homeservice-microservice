namespace Contracts.Application.IntegrationEvents.Events.Contracts.EventDtos;

public class InstallationAddressEventDto
{
    public string? FullAddress { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
}