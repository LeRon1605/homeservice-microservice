using BuildingBlocks.Application.CQRS;
using Employees.Application.Dtos;

namespace Employees.Application.Command.Employees;

public class DeactivateEmployeeCommand : ICommand<GetEmployeesDto>
{
    public Guid Id { get; set; }

    public DeactivateEmployeeCommand(Guid id)
    {
        Id = id;
    }
}