using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Dtos.Contracts;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Exceptions;
using Contracts.Domain.ContractAggregate.Specifications;
using Contracts.Domain.EmployeeAggregate;
using Contracts.Domain.EmployeeAggregate.Exceptions;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Commands.Contracts.UpdateContractAction;

public class UpdateContractActionCommandHandler : ICommandHandler<UpdateContractActionCommand, ContractActionDto>
{
    private readonly IRepository<Contract> _contractRepository;
    private readonly IReadOnlyRepository<Employee> _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateContractActionCommandHandler> _logger;
    private readonly IMapper _mapper;

    public UpdateContractActionCommandHandler(
        IRepository<Contract> contractRepository,
        IReadOnlyRepository<Employee> employeeRepository,
        IUnitOfWork unitOfWork,
        ILogger<UpdateContractActionCommandHandler> logger,
        IMapper mapper)
    {
        _contractRepository = contractRepository;
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<ContractActionDto> Handle(UpdateContractActionCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.FindAsync(new ContractByIdSpecification(request.ContractId));
        if (contract == null)
        {
            throw new ContractNotFoundException(request.ContractId);
        }
        
        if (!await _employeeRepository.AnyAsync(request.ActionByEmployeeId))
        {
            throw new EmployeeNotFoundException(request.ActionByEmployeeId);
        }
        
        var contractAction = contract.UpdateAction(
            request.ActionId,
            request.Name,
            request.Date,
            request.ActionByEmployeeId,
            request.Comment);
        _contractRepository.Update(contract);

        await _unitOfWork.SaveChangesAsync();
                                                                                                                                        
        _logger.LogInformation("Update action from contract with id: {ContractId}", contract.Id);
        return _mapper.Map<ContractActionDto>(contractAction);
    }
}