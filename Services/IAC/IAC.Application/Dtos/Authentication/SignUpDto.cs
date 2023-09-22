using System.ComponentModel.DataAnnotations;

namespace IAC.Application.Dtos.Authentication;

public class SignUpDto
{
    [Required] 
    public string FullName { get; set; } = null!;

    [Required] 
    public string PhoneNumber { get; set; } = null!;
    public string? Email { get; set; }
    
    [Required] 
    public string Password { get; set; } = null!;

}