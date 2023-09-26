using System.ComponentModel.DataAnnotations;

namespace IAC.Application.Dtos.Roles;

public class RoleUpdateDto
{
    [Required]
    public string Name { get; set; } = null!;
}