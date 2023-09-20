using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Customers.Application.Dtos;
using Customers.Domain.CustomerAggregate;
using Customers.Domain.CustomerAggregate.Specifications;
using Customers.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Customers.Application.Commands.AddCustomer;

public class AddCustomerCommandHandler : ICommandHandler<AddCustomerCommand, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddCustomerCommandHandler> _logger;
    private readonly IMapper _mapper;

    public AddCustomerCommandHandler(ICustomerRepository customerRepository,
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
        var isEmailAlreadyExist = await _customerRepository.AnyAsync(new CustomerEmailAlreadyExistSpecification(request.Email));
        if (isEmailAlreadyExist)
            throw new CustomerEmailAlreadyExistException(request.Email!);
        
        var isPhoneAlreadyExist = await _customerRepository.AnyAsync(new CustomerPhoneAlreadyExistSpecification(request.Phone));
        if (isPhoneAlreadyExist)
            throw new CustomerPhoneAlreadyExistException(request.Phone!);
        
        var customer = new Customer(request.Name, request.ContactName, request.Email, request.Address, 
            request.City, request.State, request.PostalCode, request.Phone);
        
        _customerRepository.Add(customer);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Customer {customerId} is successfully created.", customer.Id);
        
        return _mapper.Map<CustomerDto>(customer);
    }
}