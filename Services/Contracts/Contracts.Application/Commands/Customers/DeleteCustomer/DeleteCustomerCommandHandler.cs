using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Domain.CustomerAggregate;
using Contracts.Domain.CustomerAggregate.Exceptions;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Commands.Customers.DeleteCustomer;

public class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand>
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly ILogger<DeleteCustomerCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    

    public DeleteCustomerCommandHandler(IRepository<Customer> customerRepository,
                                        ILogger<DeleteCustomerCommandHandler> logger,
                                        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteCustomerCommand request,
                             CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id);
        if (customer is null)
        {
            _logger.LogWarning("Customer with id: {customerId} was not found!", request.Id);
            throw new CustomerNotFoundException(request.Id);
        }

        // This delete method is not implemented
        customer.Delete();
        _customerRepository.Delete(customer);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Customer with id: {customerId} was deleted!", request.Id);
    }
}