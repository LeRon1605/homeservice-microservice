using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Customers.Application.Commands.AddCustomer;
using Customers.Domain.CustomerAggregate;
using Microsoft.Extensions.Logging;

namespace Customers.Application.Commands.AddSignedUpCustomer;

public class AddSignedUpCustomerCommandHandler : ICommandHandler<AddSignedUpCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddCustomerCommandHandler> _logger;

    public AddSignedUpCustomerCommandHandler(ICustomerRepository customerRepository,
                                             IUnitOfWork unitOfWork,
                                             ILogger<AddCustomerCommandHandler> logger)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public Task Handle(AddSignedUpCustomerCommand request,
                       CancellationToken cancellationToken)
    {
        var customer = Customer.CreateWithId(request.Id, request.FullName, request.Phone, request.Email);
        _customerRepository.Add(customer);
        _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Customer {customerId} is successfully created.", customer.Id);
        return Task.CompletedTask;
    }
}