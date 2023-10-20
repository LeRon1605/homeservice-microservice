using BuildingBlocks.Domain.Models;

namespace Installations.Domain.InstallerAggregate;

public class Installer : AggregateRoot
{
    public string FullName { get; private set; } 
    public string Email { get; private set; }
    public string?  Phone { get; private set; }
    
    public bool IsDeactivated { get; private set; }
    
    public Installer(Guid id, string fullName, string email, string? phone, bool isDeactivated = false)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Phone = phone;
        IsDeactivated = isDeactivated;
    }

    public void Update(string fullName,
                       string email,
                       string? phone)
    {
        FullName = fullName;
        Email = email;
        Phone = phone;
    }
    
    private Installer()
    {
    }

    public void Deactivate()
    {
        IsDeactivated = true;
    }
}