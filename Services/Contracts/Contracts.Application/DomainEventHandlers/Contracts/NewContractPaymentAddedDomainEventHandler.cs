using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Event;
using Contracts.Domain.ContractAggregate.Events;

namespace Contracts.Application.DomainEventHandlers.Contracts;

public class NewContractPaymentAddedDomainEventHandler : IDomainEventHandler<NewContractPaymentAddedDomainEvent>
{
    private readonly ICurrentUser _currentUser;
    
    public NewContractPaymentAddedDomainEventHandler(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }
    
    public Task Handle(NewContractPaymentAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (_currentUser.IsAuthenticated)
        {
            notification.Contract.AddAction(
                $"New payment added to contract.",
                DateTime.Now,
                "System tracking",
                Guid.Parse(_currentUser.Id!));
        }
        
        return Task.CompletedTask;
    }
}