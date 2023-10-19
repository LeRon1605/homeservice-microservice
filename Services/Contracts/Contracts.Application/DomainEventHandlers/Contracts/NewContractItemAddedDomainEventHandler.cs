using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Event;
using Contracts.Domain.ContractAggregate.Events;

namespace Contracts.Application.DomainEventHandlers.Contracts;

public class NewContractItemAddedDomainEventHandler : IDomainEventHandler<NewContractItemAddedDomainEvent>
{
    private readonly ICurrentUser _currentUser;
    
    public NewContractItemAddedDomainEventHandler(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }
    
    public Task Handle(NewContractItemAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (_currentUser.IsAuthenticated)
        {
            notification.Contract.AddAction(
                $"New product {notification.ContractLine.ProductName} added to contract.",
                DateTime.Now,
                "System tracking.",
                Guid.Parse(_currentUser.Id!));    
        }

        return Task.CompletedTask;
    }
}