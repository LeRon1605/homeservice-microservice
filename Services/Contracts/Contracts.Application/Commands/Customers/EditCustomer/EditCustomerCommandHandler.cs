using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Dtos.Customers;
using Contracts.Domain.CustomerAggregate;
using Contracts.Domain.CustomerAggregate.Exceptions;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Commands.Customers.EditCustomer;

public class EditCustomerCommandHandler : ICommandHandler<EditCustomerCommand, CustomerDto>
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EditCustomerCommandHandler> _logger;
    private readonly IMapper _mapper;

    public EditCustomerCommandHandler(IRepository<Customer> customerRepository,
                                      IUnitOfWork unitOfWork,
                                      ILogger<EditCustomerCommandHandler> logger,
                                      IMapper mapper)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(EditCustomerCommand request,
                                    CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id);
        if (customer is null)
            throw new CustomerNotFoundException(request.Id);
        
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

        return _mapper.Map<CustomerDto>(customer);
    }
}