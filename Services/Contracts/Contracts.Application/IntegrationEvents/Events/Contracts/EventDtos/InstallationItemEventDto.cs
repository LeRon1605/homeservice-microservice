namespace Contracts.Application.IntegrationEvents.Events.Contracts.EventDtos;

public class InstallationItemEventDto
{
    public Guid MaterialId { get; set; }
    public string MaterialName { get; set; } = null!;
    public int Quantity { get; set; } 
    public decimal Cost { get; set; }
    public decimal SellPrice { get; set; }
    public Guid UnitId { get; set; }
    public string UnitName { get; set; } = null!;
}