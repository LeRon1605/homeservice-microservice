using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Event;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.CustomerAggregate.Events;
using Contracts.Domain.CustomerAggregate.Exceptions;
using Contracts.Domain.CustomerAggregate.Specifications;

namespace Contracts.Application.DomainEventHandlers.Customers;

public class CustomerDeletedDomainEventHandler : IDomainEventHandler<CustomerDeletedDomainEvent>
{
    private readonly IRepository<Contract> _contractRepository;

    public CustomerDeletedDomainEventHandler(IRepository<Contract> contractRepository)
    {
        _contractRepository = contractRepository;
    }

    public async Task Handle(CustomerDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        var isCustomerHasContract =
            await _contractRepository.AnyAsync(new CustomerHasContractSpecification(notification.CustomerId));
        
        if (isCustomerHasContract)
            throw new DeletedCustomerHasContractException(notification.CustomerId);
    }
}