using Microsoft.AspNetCore.Identity;

namespace IAC.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = null!;
    public new string PhoneNumber { get; set; } = null!;
    public ICollection<RefreshToken> RefreshTokens { get; set; } = null!;
}