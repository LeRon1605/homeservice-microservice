namespace Installations.Application.Dtos;

public class InstallationItemDto
{
    public Guid InstallationId { get; private set; } 
    
    public Guid MaterialId { get; private set; }
    public string MaterialName { get; private set; } = null!;
    
    public int Quantity { get; private set; }
    
    public Guid UnitId { get; private set; }
    public string UnitName { get; private set; } = null!;
    
    public decimal Cost { get; private set; }
    public decimal SellPrice { get; private set; }
}