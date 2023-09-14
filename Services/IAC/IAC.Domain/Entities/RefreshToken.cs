namespace IAC.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; } = null!;
    public DateTime Expires { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public string UserId { get; set; } = null!;
    
    public ApplicationUser User { get; set; } = null!;
}