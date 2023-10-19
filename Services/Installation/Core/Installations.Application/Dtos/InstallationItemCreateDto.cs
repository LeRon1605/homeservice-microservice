namespace Installations.Application.Dtos;

public class InstallationItemCreateDto
{
    public Guid MaterialId { get; set; }
    public int Quantity { get; set; }
}