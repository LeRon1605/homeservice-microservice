using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Exceptions;
using Contracts.Domain.ContractAggregate.Specifications;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Commands.Contracts.AddActionToContract;

public class AddActionToContractCommandHandler : ICommandHandler<AddActionToContractCommand>
{
    private readonly IRepository<Contract> _contractRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddActionToContractCommandHandler> _logger;

    public AddActionToContractCommandHandler(
        IRepository<Contract> contractRepository,
        IUnitOfWork unitOfWork,
        ILogger<AddActionToContractCommandHandler> logger)
    {
        _contractRepository = contractRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task Handle(AddActionToContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.FindAsync(new ContractByIdSpecification(request.ContractId));
        if (contract == null)
        {
            throw new ContractNotFoundException(request.ContractId);
        }
        
        contract.AddAction(request.Name, request.Date, request.Comment, request.ActionByEmployeeId);
        _contractRepository.Update(contract);

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Added action to contract with id: {ContractId}", contract.Id);
    }
}