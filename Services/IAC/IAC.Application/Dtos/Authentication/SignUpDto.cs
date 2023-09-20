using System.ComponentModel.DataAnnotations;

namespace IAC.Application.Dtos.Authentication;

public class SignUpDto
{
    [Required]
    public string FullName { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    [Required]
    public string Password { get; set; }
    
}