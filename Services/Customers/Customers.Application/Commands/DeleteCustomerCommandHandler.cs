using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Customers.Domain.CustomerAggregate;
using Microsoft.Extensions.Logging;

namespace Customers.Application.Commands;

public class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<DeleteCustomerCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository,
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
            _logger.LogWarning("Customer with id: {Id} was not found!", request.Id);
            return;
        }

        _customerRepository.Delete(customer);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Customer with id: {Id} was deleted!", request.Id);
    }
}