using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Event;
using Contracts.Domain.ContractAggregate.Events;

namespace Contracts.Application.DomainEventHandlers.Contracts;

public class ContractItemUpdatedDomainEventHandler : IDomainEventHandler<ContractItemUpdatedDomainEvent>
{
    private readonly ICurrentUser _currentUser;
    
    public ContractItemUpdatedDomainEventHandler(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }
    
    public Task Handle(ContractItemUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (_currentUser.IsAuthenticated)
        {
            notification.Contract.AddAction(
                $"Updated product info for {notification.ContractLine.ProductName} in contract.",
                DateTime.Now,
                "System tracking",
                Guid.Parse(_currentUser.Id!));    
        }
        
        return Task.CompletedTask;
    }
}