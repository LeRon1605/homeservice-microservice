using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Commands.Contracts.DeletePaymentFromContract;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Exceptions;
using Contracts.Domain.ContractAggregate.Specifications;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Commands.Contracts.DeleteActionFromContract;

public class DeleteActionFromContractCommandHandler : ICommandHandler<DeleteActionFromContractCommand>
{
    private readonly IRepository<Contract> _contractRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteActionFromContractCommandHandler> _logger;

    public DeleteActionFromContractCommandHandler(
        IRepository<Contract> contractRepository,
        IUnitOfWork unitOfWork,
        ILogger<DeleteActionFromContractCommandHandler> logger)
    {
        _contractRepository = contractRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task Handle(DeleteActionFromContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.FindAsync(new ContractByIdSpecification(request.ContractId));
        if (contract == null)
        {
            throw new ContractNotFoundException(request.ContractId);
        }
        
        contract.RemoveAction(request.ActionId);
        _contractRepository.Update(contract);

        await _unitOfWork.SaveChangesAsync();
                                                                                                                                        
        _logger.LogInformation("Deleted action from contract with id: {ContractId}", contract.Id);
    }
}