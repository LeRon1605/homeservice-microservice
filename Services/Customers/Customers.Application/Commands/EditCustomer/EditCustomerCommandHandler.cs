using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.EventBus.Interfaces;
using Customers.Application.Dtos;
using Customers.Application.IntegrationEvents.Events;
using Customers.Domain.CustomerAggregate;
using Customers.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Customers.Application.Commands.EditCustomer;

public class EditCustomerCommandHandler : ICommandHandler<EditCustomerCommand, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EditCustomerCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    public EditCustomerCommandHandler(ICustomerRepository customerRepository,
                                      IUnitOfWork unitOfWork,
                                      ILogger<EditCustomerCommandHandler> logger,
                                      IMapper mapper,
                                      IEventBus eventBus)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _eventBus = eventBus;
    }

    public async Task<CustomerDto> Handle(EditCustomerCommand request,
                                    CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id);
        if (customer is null)
            throw new CustomerNotFoundException(request.Id);
        
        var isIdentityInfoChanged = customer.ContactName != request.ContactName ||
                                    customer.Phone != request.Phone ||
                                    customer.Email != request.Email;
        
        customer.Update(request.Name,
            request.ContactName,
            request.Phone,
            request.Email,
            request.Address,
            request.City,
            request.State,
            request.PostalCode);
        
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Customer with id: {customerId} was updated!", request.Id);

        if (isIdentityInfoChanged)
        {
            var customerNameChangedEvent =
                new CustomerInfoChangedIntegrationEvent(customer.Id, customer.ContactName, customer.Phone, customer.Email);
            
            _logger.LogInformation("Publishing {integrationEvent} integration event", nameof(CustomerInfoChangedIntegrationEvent));
            
            _eventBus.Publish(customerNameChangedEvent);
            
            _logger.LogInformation("{integrationEvent} integration event was published", nameof(CustomerInfoChangedIntegrationEvent));
        }
        
        return _mapper.Map<CustomerDto>(customer);
    }
}