using AutoMapper;
using BuildingBlocks.Application.IntegrationEvent;
using Installations.Application.Commands;
using Installations.Application.Dtos;
using Installations.Application.IntegrationEvents.Events.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Installations.Application.IntegrationEvents.Handlers;

public class ContractCreatedIntegrationEventHandler : IIntegrationEventHandler<ContractCreatedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<ContractCreatedIntegrationEventHandler> _logger;

    public ContractCreatedIntegrationEventHandler(IMediator mediator,
                                                  IMapper mapper,
                                                  ILogger<ContractCreatedIntegrationEventHandler> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(ContractCreatedIntegrationEvent @event)
    {
        _logger.LogInformation($"Handling integration event: {@event.Id} at {nameof(ContractCreatedIntegrationEventHandler)}");
        foreach (var installation in @event.Installations)
        {
            await _mediator.Send(new AddInstallationCommand
            {
                ContractId = @event.ContractId,
                ContractLineId = installation.ContractLineId,
                CustomerId = @event.CustomerId,
                SalespersonId = @event.SalespersonId,
                SupervisorId = @event.SupervisorId,
                InstallerId = installation.InstallerId,
                FullAddress = @event.InstallationAddress?.FullAddress,
                City = @event.InstallationAddress?.City,
                State = @event.InstallationAddress?.State,
                PostalCode = @event.InstallationAddress?.PostalCode,
                EstimatedStartTime = installation.EstimatedStartTime,
                EstimatedFinishTime = installation.EstimatedFinishTime,
                ActualStartTime = installation.ActualStartTime,
                ActualFinishTime = installation.ActualFinishTime,
                InstallationComment = installation.InstallationComment,
                FloorType = installation.FloorType,
                InstallationMetres = installation.InstallationMetres,
                InstallationItems = _mapper.Map<List<InstallationItemCreateDto>>(installation.Items),
                InstallationAddress = _mapper.Map<InstallationAddressDto>(@event.InstallationAddress)
            });
        }
        
        _logger.LogInformation($"Handled integration event: {@event.Id} at {nameof(ContractCreatedIntegrationEventHandler)}");
    }
}