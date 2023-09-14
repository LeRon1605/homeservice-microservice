namespace IAC.Application.Dtos.Authentication;

public class LoginDto
{
    public string Identifier { get; set; } = null!;
    public string Password { get; set; } = null!;
}