using AutoMapper;
using BuildingBlocks.Application.IntegrationEvent;
using BuildingBlocks.Domain.Data;
using Installations.Application.IntegrationEvents.Events.Contracts;
using Installations.Domain.ContractAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Installations.Application.IntegrationEvents.Handlers;

public class ContractCreatedIntegrationEventHandler : IIntegrationEventHandler<ContractCreatedIntegrationEvent>
{
    private readonly IRepository<Contract> _contractRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ContractCreatedIntegrationEventHandler> _logger;

    public ContractCreatedIntegrationEventHandler(ILogger<ContractCreatedIntegrationEventHandler> logger,
                                                  IUnitOfWork unitOfWork,
                                                  IMapper mapper,
                                                  IRepository<Contract> contractRepository)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contractRepository = contractRepository;
    }

    public async Task Handle(ContractCreatedIntegrationEvent @event)
    {
        _logger.LogInformation($"Handling integration event: {@event.Id} at {nameof(ContractCreatedIntegrationEventHandler)}");
        
        var installationAddress = _mapper.Map<InstallationAddress?>(@event.InstallationAddress);
        var contract = new Contract(@event.ContractId, @event.ContractNo, @event.CustomerId, @event.CustomerName, installationAddress);
        foreach (var contractLineEventDto in @event.ContractLines)
        {
            contract.AddContractLine(contractLineEventDto.Id, contractLineEventDto.ProductId, contractLineEventDto.ProductName, contractLineEventDto.Color); 
        }
        
        _contractRepository.Add(contract);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation($"Handled integration event: {@event.Id} at {nameof(ContractCreatedIntegrationEventHandler)}");
    }
}