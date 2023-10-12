using BuildingBlocks.Application.CQRS;
using Employees.Application.Dtos;

namespace Employees.Application.Queries;

public class GetEmployeeByIdQuery : IQuery<GetEmployeesDto>
{
    public Guid Id { get; private set; }

    public GetEmployeeByIdQuery(Guid id)
    {
        Id = id;
    }
}