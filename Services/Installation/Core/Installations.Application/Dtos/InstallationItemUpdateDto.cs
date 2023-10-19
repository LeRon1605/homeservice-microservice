namespace Installations.Application.Dtos;

public class InstallationItemUpdateDto
{
    public Guid MaterialId { get; set; }
    public int Quantity { get; set; }
}