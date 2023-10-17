using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Employees.Application.Dtos;
using Employees.Domain.EmployeeAggregate;
using Employees.Domain.EmployeeAggregate.Specifications;
using Employees.Domain.Exceptions;
using Employees.Domain.RoleAggregate.Exceptions;
using Employees.Domain.RoleAggregate.Specifications;
using Microsoft.Extensions.Logging;

namespace Employees.Application.Command.Employees;

public class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand, GetEmployeesDto>
{
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IRepository<Domain.RoleAggregate.Role> _roleRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateEmployeeCommand> _logger;

    public UpdateEmployeeCommandHandler(IRepository<Employee> employeeRepository,
        IRepository<Domain.RoleAggregate.Role> roleRepository, IMapper mapper,
        IUnitOfWork unitOfWork, ILogger<CreateEmployeeCommand> logger)
    {
        _employeeRepository = employeeRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<GetEmployeesDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.FindAsync(new RoleByIdSpecification(request.RoleId));
        if (role == null)
            throw new RoleNotFoundException(request.RoleId);

        var employee = await _employeeRepository.FindAsync(new EmployeeByIdSpecification(request.Id));
        if (employee == null)
            throw new EmployeeNotFoundException(request.Id);

        await employee.UpdateAsync(request.EmployeeCode, request.FullName,
            request.Position, request.Email,
            request.Phone, request.RoleId,
            role.Name, _employeeRepository);
        
        _employeeRepository.Update(employee);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<GetEmployeesDto>(employee);
    }
}