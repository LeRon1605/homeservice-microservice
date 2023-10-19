using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Event;
using Contracts.Domain.ContractAggregate.Events;

namespace Contracts.Application.DomainEventHandlers.Contracts;

public class ContractItemDeletedDomainEventHandler : IDomainEventHandler<ContractItemDeletedDomainEvent>
{
    private readonly ICurrentUser _currentUser;
    
    public ContractItemDeletedDomainEventHandler(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }
    
    public Task Handle(ContractItemDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (_currentUser.IsAuthenticated)
        {
            notification.Contract.AddAction(
                $"Product {notification.ContractLine.ProductName} deleted from contract.",
                DateTime.Now, 
                "System tracking",
                Guid.Parse(_currentUser.Id!));    
        }

        return Task.CompletedTask;
    }
}