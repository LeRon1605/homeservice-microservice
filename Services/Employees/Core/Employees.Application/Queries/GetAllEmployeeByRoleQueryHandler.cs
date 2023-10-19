using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Employees.Application.Dtos;
using Employees.Domain.EmployeeAggregate;
using Employees.Domain.EmployeeAggregate.Specifications;

namespace Employees.Application.Queries;

public class GetAllEmployeeByRoleQueryHandler : IQueryHandler<GetAllEmployeeByRoleQuery, IEnumerable<GetEmployeesDto>>
{
    private readonly IReadOnlyRepository<Employee> _employeeRepository;
    private readonly IMapper _mapper;

    public GetAllEmployeeByRoleQueryHandler(IReadOnlyRepository<Employee> employeeRepository,
                                            IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetEmployeesDto>> Handle(GetAllEmployeeByRoleQuery request,
                                                    CancellationToken cancellationToken)
    {
        var employeeSpecification = new EmployeeByRoleSpecification(request.Role);
        var employees = await _employeeRepository.FindListAsync(employeeSpecification);

        var employeesDto = _mapper.Map<IEnumerable<GetEmployeesDto>>(employees);
        return employeesDto; 
    }
}