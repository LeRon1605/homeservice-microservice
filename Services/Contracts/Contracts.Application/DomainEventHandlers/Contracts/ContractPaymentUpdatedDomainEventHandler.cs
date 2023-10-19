using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Event;
using Contracts.Domain.ContractAggregate.Events;

namespace Contracts.Application.DomainEventHandlers.Contracts;

public class ContractPaymentUpdatedDomainEventHandler : IDomainEventHandler<ContractPaymentUpdatedDomainEvent>
{
    private readonly ICurrentUser _currentUser;
    
    public ContractPaymentUpdatedDomainEventHandler(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }
    
    public Task Handle(ContractPaymentUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (_currentUser.IsAuthenticated)
        {
            notification.Contract.AddAction(
                $"Payment updated in contract.",
                DateTime.Now,
                "System tracking",
                Guid.Parse(_currentUser.Id!));
        }
        
        return Task.CompletedTask;
    }
}