using BuildingBlocks.Application.CQRS;

namespace Contracts.Application.Commands.Employees.AddEmployee;

public class AddEmployeeCommand : ICommand
{
    public Guid EmployeeId { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }
    
    public AddEmployeeCommand(Guid employeeId, string fullName, string role)
    {
        EmployeeId = employeeId;
        FullName = fullName;
        Role = role;
    }
}