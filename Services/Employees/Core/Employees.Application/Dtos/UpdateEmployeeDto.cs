﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Employees.Domain.EmployeeAggregate.Enums;

namespace Employees.Application.Dtos;

public class UpdateEmployeeDto
{
    [Required] public int EmployeeCode { get; set; }
    [Required] public string FullName { get; set; }
    [Required] public string Position { get; set; }
    [Required] public string Email { get; set; }
    public string? Phone { get; set; }
    [Required] public Guid RoleId { get; set; }
}