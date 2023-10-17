using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Exceptions;
using Contracts.Domain.ContractAggregate.Specifications;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Commands.Contracts.UpdateContractAction;

public class UpdateContractActionCommandHandler : ICommandHandler<UpdateContractActionCommand>
{
    private readonly IRepository<Contract> _contractRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateContractActionCommandHandler> _logger;

    public UpdateContractActionCommandHandler(
        IRepository<Contract> contractRepository,
        IUnitOfWork unitOfWork,
        ILogger<UpdateContractActionCommandHandler> logger)
    {
        _contractRepository = contractRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task Handle(UpdateContractActionCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.FindAsync(new ContractByIdSpecification(request.ContractId));
        if (contract == null)
        {
            throw new ContractNotFoundException(request.ContractId);
        }
        
        contract.UpdateAction(
            request.ActionId,
            request.Name,
            request.Date,
            request.ActionByEmployeeId,
            request.Comment);
        _contractRepository.Update(contract);

        await _unitOfWork.SaveChangesAsync();
                                                                                                                                        
        _logger.LogInformation("Update action from contract with id: {ContractId}", contract.Id);
    }
}