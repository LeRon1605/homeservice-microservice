using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Data;
using Employees.Application.Dtos;
using Employees.Domain.EmployeeAggregate;
using Employees.Domain.EmployeeAggregate.Specifications;

namespace Employees.Application.Queries;

public class
    GetEmployeesWithPaginationQueryHandler : IQueryHandler<GetEmployeesWithPaginationQuery,
        PagedResult<GetEmployeesDto>>
{
    private readonly IReadOnlyRepository<Employee> _employeeRepository;
    private readonly IMapper _mapper;

    public GetEmployeesWithPaginationQueryHandler(IReadOnlyRepository<Employee> employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<GetEmployeesDto>> Handle(GetEmployeesWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var employeeSpecification =
            new EmployeeFilterAndPagingSpecification(request.Search, request.Status, request.PageIndex,
                request.PageSize);
        var (employees, totalCount) = await _employeeRepository.FindWithTotalCountAsync(employeeSpecification);
        
        var employeesDto = _mapper.Map<IEnumerable<GetEmployeesDto>>(employees);
        return new PagedResult<GetEmployeesDto>(employeesDto, totalCount, request.PageIndex, request.PageSize);
    }
}