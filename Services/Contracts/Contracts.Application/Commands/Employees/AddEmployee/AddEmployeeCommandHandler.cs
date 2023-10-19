using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Domain.EmployeeAggregate;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Commands.Employees.AddEmployee;

public class AddEmployeeCommandHandler : ICommandHandler<AddEmployeeCommand>
{
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddEmployeeCommandHandler> _logger;
    
    public AddEmployeeCommandHandler(
        IRepository<Employee> employeeRepository, 
        IUnitOfWork unitOfWork,
        ILogger<AddEmployeeCommandHandler> logger)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee(request.EmployeeId, request.FullName, request.Role);
        _employeeRepository.Add(employee);

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Employee with id: {Id} added successfully", request.EmployeeId);
    }
}