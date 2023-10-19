namespace Installations.Application.IntegrationEvents.Events.Contracts.EventDtos;

public class ContractLineEventDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string? Color { get; set; }
}