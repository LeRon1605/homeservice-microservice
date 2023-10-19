using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Event;
using BuildingBlocks.EventBus.Interfaces;
using Contracts.Application.IntegrationEvents.Events.Orders;
using Contracts.Domain.CustomerAggregate;
using Contracts.Domain.PendingOrdersAggregate.Events;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.DomainEventHandlers.Orders;

public class OrderFinishedDomainEventHandler : IDomainEventHandler<OrderFinishedDomainEvent>
{
    private readonly IEventBus _eventBus;
    private readonly IRepository<Customer> _customerRepository;
    private readonly ILogger<OrderFinishedDomainEventHandler> _logger;

    public OrderFinishedDomainEventHandler(
        IRepository<Customer> customerRepository,
        ILogger<OrderFinishedDomainEventHandler> logger,
        IEventBus eventBus)
    {
        _customerRepository = customerRepository;
        _logger = logger;
        _eventBus = eventBus;
    }

    public async Task Handle(OrderFinishedDomainEvent @event, CancellationToken cancellationToken)
    {
        if (!await _customerRepository.AnyAsync(@event.Order.BuyerId))
        {
            var customer = Customer.CreateWithId(
                @event.Order.BuyerId, 
                @event.Order.ContactInfo.ContactName,
                @event.Order.ContactInfo.Phone,
                @event.Order.ContactInfo.Email, 
                @event.Order.ContactInfo.Address, 
                @event.Order.ContactInfo.City, 
                @event.Order.ContactInfo.State, 
                @event.Order.ContactInfo.PostalCode);
        
            _customerRepository.Add(customer);
        
            _logger.LogInformation("Customer {customerId} is successfully created.", customer.Id);    
        }

        var integrationEvent = new OrderFinishedIntegrationEvent(@event.Order.Id);
        _eventBus.Publish(integrationEvent);
        
        _logger.LogInformation("Published integration event {IntegrationEvent} for Order {OrderId}", 
            integrationEvent.GetType().Name, @event.Order.Id);
    }
}