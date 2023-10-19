using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Event;
using Contracts.Domain.ContractAggregate.Events;

namespace Contracts.Application.DomainEventHandlers.Contracts;

public class ContractPaymentDeletedDomainEventHandler : IDomainEventHandler<ContractPaymentDeletedDomainEvent>
{
    private readonly ICurrentUser _currentUser;
    
    public ContractPaymentDeletedDomainEventHandler(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }
    
    public Task Handle(ContractPaymentDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (_currentUser.IsAuthenticated)
        {
            notification.Contract.AddAction(
                $"Payment deleted from contract.",
                DateTime.Now,
                "System tracking",
                Guid.Parse(_currentUser.Id!));
        }
        
        return Task.CompletedTask;
    }
}