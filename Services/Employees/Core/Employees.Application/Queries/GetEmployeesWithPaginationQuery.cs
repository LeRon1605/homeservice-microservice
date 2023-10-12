using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using Employees.Application.Dtos;
using Employees.Domain.EmployeeAggregate;
using Employees.Domain.EmployeeAggregate.Enums;

namespace Employees.Application.Queries;

public class GetEmployeesWithPaginationQuery : PagingParameters, IQuery<PagedResult<GetEmployeesDto>>
{
    public List<Status>? Status { get; private set; }

    public GetEmployeesWithPaginationQuery(EmployeeFilterAndPagingDto employeeFilterAndPagingDto)
    {
        Status = employeeFilterAndPagingDto.Status;
        PageSize = employeeFilterAndPagingDto.PageSize;
        PageIndex = employeeFilterAndPagingDto.PageIndex;
        Search = employeeFilterAndPagingDto.Search;
    }
}