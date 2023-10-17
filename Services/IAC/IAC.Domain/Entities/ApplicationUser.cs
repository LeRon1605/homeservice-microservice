using IAC.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace IAC.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = null!;
    public new string PhoneNumber { get; set; } = null!;
    public Status Status { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = null!;
}