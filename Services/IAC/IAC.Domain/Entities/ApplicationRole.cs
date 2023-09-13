using Microsoft.AspNetCore.Identity;

namespace IAC.Domain.Entities;

public class ApplicationRole : IdentityRole
{
    public ApplicationRole(string name) : base(name)
    {
    }
}