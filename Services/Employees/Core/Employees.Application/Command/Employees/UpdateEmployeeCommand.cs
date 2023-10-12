using System.Text.Json.Serialization;
using BuildingBlocks.Application.CQRS;
using Employees.Application.Dtos;
using Employees.Domain.EmployeeAggregate;
using Employees.Domain.EmployeeAggregate.Enums;

namespace Employees.Application.Command.Employees;

public class UpdateEmployeeCommand : ICommand<GetEmployeesDto>
{
    public Guid Id { get; private set; }
    public int EmployeeCode { get; private set; }
    public string FullName { get; private set; }
    public string Position { get; private set; }
    public string Email { get; private set; }
    public string? Phone { get; private set; }
    public string RoleId { get; private set; }

    public UpdateEmployeeCommand(Guid id, UpdateEmployeeDto updateEmployeeDto)
    {
        Id = id;
        EmployeeCode = updateEmployeeDto.EmployeeCode;
        FullName = updateEmployeeDto.FullName;
        Position = updateEmployeeDto.Position;
        Email = updateEmployeeDto.Email;
        Phone = updateEmployeeDto.Phone;
        RoleId = updateEmployeeDto.RoleId;
    }
}