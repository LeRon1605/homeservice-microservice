using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Commands.Employees.AddEmployee;
using Contracts.Domain.EmployeeAggregate;
using Contracts.Domain.EmployeeAggregate.Exceptions;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Commands.Employees.UpdateEmployee;

public class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand>
{
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateEmployeeCommandHandler> _logger;
    
    public UpdateEmployeeCommandHandler(
        IRepository<Employee> employeeRepository, 
        IUnitOfWork unitOfWork,
        ILogger<UpdateEmployeeCommandHandler> logger)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
        if (employee == null)
        {
            throw new EmployeeNotFoundException(request.EmployeeId);
        }
        
        employee.Update(request.FullName, request.Role);
        _employeeRepository.Update(employee);
        
        _logger.LogInformation("Employee with id: {Id} updated successfully", request.EmployeeId);
    }
}