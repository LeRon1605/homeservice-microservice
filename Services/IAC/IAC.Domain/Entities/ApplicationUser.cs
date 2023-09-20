using Microsoft.AspNetCore.Identity;

namespace IAC.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public override string PhoneNumber { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = null!;
}