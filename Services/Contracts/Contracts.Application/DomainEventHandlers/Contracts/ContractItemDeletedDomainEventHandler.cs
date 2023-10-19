using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Event;
using Contracts.Domain.ContractAggregate.Events;
using Contracts.Domain.EmployeeAggregate;

namespace Contracts.Application.DomainEventHandlers.Contracts;

public class ContractItemDeletedDomainEventHandler : IDomainEventHandler<ContractItemDeletedDomainEvent>
{
    private readonly ICurrentUser _currentUser;
    private readonly IReadOnlyRepository<Employee> _employeeRepository;
    
    public ContractItemDeletedDomainEventHandler(
        ICurrentUser currentUser,
        IReadOnlyRepository<Employee> employeeRepository)
    {
        _currentUser = currentUser;
        _employeeRepository = employeeRepository;
    }
    
    public async Task Handle(ContractItemDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (_currentUser.IsAuthenticated && await _employeeRepository.AnyAsync(Guid.Parse(_currentUser.Id!)))
        {
            notification.Contract.AddAction(
                "Product deleted from contract.",
                DateTime.Now, 
                $"Product {notification.ContractLine.ProductName} deleted from contract.",
                Guid.Parse(_currentUser.Id!));    
        }
    }
}