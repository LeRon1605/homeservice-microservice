namespace IAC.Application.Dtos.Users;

public class UserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = default!;
}