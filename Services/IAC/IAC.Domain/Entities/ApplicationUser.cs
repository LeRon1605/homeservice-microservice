using Microsoft.AspNetCore.Identity;

namespace IAC.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public ICollection<RefreshToken> RefreshTokens { get; set; } = null!;
}