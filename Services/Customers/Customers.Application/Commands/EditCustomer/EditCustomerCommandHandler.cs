using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Customers.Application.Dtos;
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

    public EditCustomerCommandHandler(ICustomerRepository customerRepository,
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
        {
            _logger.LogWarning("Customer with id: {customerId} was not found!", request.Id);
            throw new CustomerNotFoundException(request.Id);
        }

        customer.Update(request.Name,
            request.ContactName,
            request.Email,
            request.Address,
            request.City,
            request.State,
            request.PostalCode,
            request.Phone);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Customer with id: {customerId} was updated!", request.Id);
        
        return _mapper.Map<CustomerDto>(customer); 
    }
}