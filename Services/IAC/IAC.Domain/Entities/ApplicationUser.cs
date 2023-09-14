using Microsoft.AspNetCore.Identity;

namespace IAC.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public override string PhoneNumber { get; set; }
}