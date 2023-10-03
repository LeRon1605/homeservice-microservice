using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Commands.Customers.AddCustomer;
using Contracts.Domain.CustomerAggregate;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Commands.Customers.AddRetailCustomer;

public class AddRetailCustomerCommandHandler : ICommandHandler<AddRetailCustomerCommand>
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddCustomerCommandHandler> _logger;

    public AddRetailCustomerCommandHandler(IRepository<Customer> customerRepository,
                                             IUnitOfWork unitOfWork,
                                             ILogger<AddCustomerCommandHandler> logger)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(AddRetailCustomerCommand request,
                       CancellationToken cancellationToken)
    {
        var customer = Customer.CreateWithId(request.Id, request.FullName, request.Phone, request.Email, request.Address, 
            request.City, request.State, request.PostalCode);
        
        _customerRepository.Add(customer);
        
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Customer {customerId} is successfully created.", customer.Id);
    }
}