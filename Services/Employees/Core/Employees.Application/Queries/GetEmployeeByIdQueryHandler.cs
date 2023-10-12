using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Employees.Application.Dtos;
using Employees.Domain.EmployeeAggregate;
using Employees.Domain.EmployeeAggregate.Specifications;
using Employees.Domain.Exceptions;

namespace Employees.Application.Queries;

public class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, GetEmployeesDto>
{
    private readonly IReadOnlyRepository<Employee> _employeeRepository;
    private readonly IMapper _mapper;

    public GetEmployeeByIdQueryHandler(IReadOnlyRepository<Employee> employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<GetEmployeesDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employeeSpecification = new EmployeeByIdSpecification(request.Id);
        var employee = await _employeeRepository.FindAsync(employeeSpecification);
        if (employee == null)
            throw new EmployeeNotFoundException(request.Id);
        return _mapper.Map<GetEmployeesDto>(employee);
    }
}