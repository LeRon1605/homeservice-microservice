using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Employees.Application.Dtos;
using Employees.Domain.EmployeeAggregate;
using Employees.Domain.EmployeeAggregate.Specifications;
using Employees.Domain.RoleAggregate.Exceptions;
using Employees.Domain.RoleAggregate.Specifications;
using Microsoft.Extensions.Logging;

namespace Employees.Application.Command.Employees;

public class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, GetEmployeesDto>
{
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IRepository<Domain.RoleAggregate.Role> _roleRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateEmployeeCommand> _logger;

    public CreateEmployeeCommandHandler(IRepository<Employee> employeeRepository, IMapper mapper,
        IUnitOfWork unitOfWork, ILogger<CreateEmployeeCommand> logger,
        IRepository<Domain.RoleAggregate.Role> roleRepository)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _roleRepository = roleRepository;
    }

    public async Task<GetEmployeesDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.FindAsync(new RoleByIdSpecification(request.RoleId));
        if (role == null)
            throw new RoleNotFoundException(Guid.Parse(request.RoleId));

        var employee = await Employee.InitAsync(request.EmployeeCode, request.FullName, request.Position, request.Email,
            request.Phone, request.RoleId, role.Name, request.Status, _employeeRepository);

        _employeeRepository.Add(employee);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Employee with name: {Name} added successfully", employee.FullName);

        var employeeSpecification = new EmployeeByIdSpecification(employee.Id);

        var employeeCreated = await _employeeRepository.FindAsync(employeeSpecification);

        return _mapper.Map<GetEmployeesDto>(employeeCreated);
    }
}