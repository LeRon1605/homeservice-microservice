using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Event;
using Contracts.Domain.ContractAggregate.Events;
using Contracts.Domain.EmployeeAggregate;

namespace Contracts.Application.DomainEventHandlers.Contracts;

public class NewContractItemAddedDomainEventHandler : IDomainEventHandler<NewContractItemAddedDomainEvent>
{
    private readonly ICurrentUser _currentUser;
    private readonly IReadOnlyRepository<Employee> _employeeRepository;
    
    public NewContractItemAddedDomainEventHandler(
        ICurrentUser currentUser,
        IReadOnlyRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
        _currentUser = currentUser;
    }
    
    public async Task Handle(NewContractItemAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (_currentUser.IsAuthenticated && await _employeeRepository.AnyAsync(Guid.Parse(_currentUser.Id!)))
        {
            notification.Contract.AddAction(
                "New product added to contract.",
                DateTime.Now,
                $"New product {notification.ContractLine.ProductName} added to contract.",
                Guid.Parse(_currentUser.Id!));    
        }
    }
}