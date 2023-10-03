using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Dtos.Customers;
using Contracts.Domain.CustomerAggregate;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Commands.Customers.AddCustomer;

public class AddCustomerCommandHandler : ICommandHandler<AddCustomerCommand, CustomerDto>
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddCustomerCommandHandler> _logger;
    private readonly IMapper _mapper;

    public AddCustomerCommandHandler(IRepository<Customer> customerRepository,
                                     IUnitOfWork unitOfWork,
                                     ILogger<AddCustomerCommandHandler> logger,
                                     IMapper mapper)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(AddCustomerCommand request,
                                          CancellationToken cancellationToken)
    {
        var customer = new Customer(request.Name, request.ContactName, request.Phone, request.Email, request.Address, 
            request.City, request.State, request.PostalCode);
        
        _customerRepository.Add(customer);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Customer {customerId} is successfully created.", customer.Id);
        
        return _mapper.Map<CustomerDto>(customer);
    }
}