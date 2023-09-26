using System.ComponentModel.DataAnnotations;

namespace IAC.Application.Dtos.Roles;

public class RoleCreateDto
{
    [Required]
    public string Name { get; set; } = null!;
}