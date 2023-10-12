using System.ComponentModel.DataAnnotations;
using BuildingBlocks.Application.CQRS;
using Employees.Application.Dtos;
using Employees.Domain.EmployeeAggregate.Enums;

namespace Employees.Application.Command.Employees;

public class CreateEmployeeCommand : ICommand<GetEmployeesDto>
{
    [Required] public int EmployeeCode { get; private set; }
    [Required] public string FullName { get; private set; }
    [Required] public string Position { get; private set; }
    [Required] public string Email { get; private set; }
    [Required] public string? Phone { get; private set; }
    [Required] public string RoleId { get; private set; }
    [Required] public Status Status { get; private set; }

    public CreateEmployeeCommand(CreateEmployeeDto createEmployeeDto)
    {
        EmployeeCode = createEmployeeDto.EmployeeCode;
        FullName = createEmployeeDto.FullName;
        Position = createEmployeeDto.Position;
        Email = createEmployeeDto.Email;
        Phone = createEmployeeDto.Phone;
        RoleId = createEmployeeDto.RoleId;
        Status = Status.Active;
    }
}