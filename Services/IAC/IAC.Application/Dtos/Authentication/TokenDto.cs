using System.Text.Json.Serialization;
using IAC.Application.Dtos.Users;

namespace IAC.Application.Dtos.Authentication;

public class TokenDto
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public UserDto? User { get; set; }
}