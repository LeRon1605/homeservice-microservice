using BuildingBlocks.Application.CQRS;
using Employees.Application.Dtos;

namespace Employees.Application.Queries;

public class GetAllEmployeeByRoleQuery : IQuery<IEnumerable<GetEmployeesDto>>
{
    public string Role { get; set; } 
    
    public GetAllEmployeeByRoleQuery(string role)
    {
        Role = role;
    }
}