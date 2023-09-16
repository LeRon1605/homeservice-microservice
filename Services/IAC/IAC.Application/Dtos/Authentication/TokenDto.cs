using IAC.Application.Dtos.Users;

namespace IAC.Application.Dtos.Authentication;

public class TokenDto
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    
    public UserDto? User { get; set; }
}