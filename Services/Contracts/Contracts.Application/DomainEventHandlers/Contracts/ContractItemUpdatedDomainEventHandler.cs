using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Event;
using Contracts.Domain.ContractAggregate.Events;
using Contracts.Domain.EmployeeAggregate;

namespace Contracts.Application.DomainEventHandlers.Contracts;

public class ContractItemUpdatedDomainEventHandler : IDomainEventHandler<ContractItemUpdatedDomainEvent>
{
    private readonly ICurrentUser _currentUser;
    private readonly IReadOnlyRepository<Employee> _employeeRepository;
    
    public ContractItemUpdatedDomainEventHandler(
        ICurrentUser currentUser,
        IReadOnlyRepository<Employee> employeeRepository)
    {
        _currentUser = currentUser;
        _employeeRepository = employeeRepository;
    }
    
    public async Task Handle(ContractItemUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (_currentUser.IsAuthenticated && await _employeeRepository.AnyAsync(Guid.Parse(_currentUser.Id!)))
        {
            notification.Contract.AddAction(
                $"Updated product in contract.",
                DateTime.Now,
                $"Updated product info for {notification.ContractLine.ProductName} in contract.",
                Guid.Parse(_currentUser.Id!));    
        }
    }
}