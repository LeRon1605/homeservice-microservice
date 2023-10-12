using System.ComponentModel.DataAnnotations;
using Employees.Domain.EmployeeAggregate.Enums;

namespace Employees.Application.Dtos;

public class GetEmployeesDto
{
    public Guid Id { get; private set; }
    public int EmployeeCode { get; private set; }
    public string FullName { get; private set; }
    public string Position { get; private set; }
    public string Email { get; private set; }
    public string? Phone { get; private set; }
    public string RoleId { get; private set; }
    public string RoleName { get; private set; }
    public Status Status { get; private set; }
}