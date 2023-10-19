using BuildingBlocks.Application.CQRS;

namespace Contracts.Application.Commands.Employees.UpdateEmployee;

public class UpdateEmployeeCommand : ICommand
{
    public Guid EmployeeId { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }
    
    public UpdateEmployeeCommand(Guid employeeId, string fullName, string role)
    {
        EmployeeId = employeeId;
        FullName = fullName;
        Role = role;
    }
}