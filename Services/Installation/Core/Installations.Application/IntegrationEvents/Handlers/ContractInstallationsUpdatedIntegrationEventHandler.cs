using AutoMapper;
using BuildingBlocks.Application.IntegrationEvent;
using Installations.Application.Commands;
using Installations.Application.Dtos;
using Installations.Application.IntegrationEvents.Events.Contracts;
using Installations.Application.IntegrationEvents.Events.Contracts.EventDtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Installations.Application.IntegrationEvents.Handlers;

public class ContractInstallationsUpdatedIntegrationEventHandler : IIntegrationEventHandler<ContractInstallationsUpdatedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<ContractInstallationsUpdatedIntegrationEventHandler> _logger;

    public ContractInstallationsUpdatedIntegrationEventHandler(IMediator mediator,
                                                               ILogger<ContractInstallationsUpdatedIntegrationEventHandler> logger,
                                                               IMapper mapper)
    {
        _mediator = mediator;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task Handle(ContractInstallationsUpdatedIntegrationEvent @event)
    {
        _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
        foreach (var installation in @event.Installations)
        {
            if (installation.Id is null)
            {
                _logger.LogInformation("Adding installation: {@IntegrationEvent}", @event);
                await _mediator.Send(GetAddInstallationCommand(@event, installation));
            }
            else
            {
                if (installation.IsDelete)
                {
                    _logger.LogInformation("Deleting installation: {InstallationId}", installation.Id.Value);
                    await _mediator.Send(new DeleteInstallationCommand { InstallationId = installation.Id.Value });
                }
                else
                {
                    _logger.LogInformation("Updating installation: {InstallationId}", installation.Id.Value);
                    await _mediator.Send(GetUpdateInstallationCommand(@event, installation));
                }
            }
        }

        // Some installations may not in the changed list, but the address may have changed
        if (@event.IsAddressChanged)
        {
            _logger.LogInformation("Updating installation address: {InstallationAddress}", @event.InstallationAddress);
            await _mediator.Send(new UpdateInstallationAddressCommand
            {
                ContractId = @event.ContractId,
                InstallationAddress = _mapper.Map<InstallationAddressDto>(@event.InstallationAddress)
            }); 
        }  
    }

    private UpdateInstallationCommand GetUpdateInstallationCommand(ContractInstallationsUpdatedIntegrationEvent @event,
                                                                   InstallationUpdatedEventDto installation)
    {
        return new UpdateInstallationCommand
        {
            InstallationId = installation.Id!.Value,
            ContractLineId = installation.ContractLineId,
            InstallerId = installation.InstallerId,
                        
            InstallDate = installation.InstallDate,
            EstimatedStartTime = installation.EstimatedStartTime,
            EstimatedFinishTime = installation.EstimatedFinishTime,
            ActualStartTime = installation.ActualStartTime,
            ActualFinishTime = installation.ActualFinishTime,
                        
            InstallationComment = installation.InstallationComment,
            FloorType = installation.FloorType,
            InstallationMetres = installation.InstallationMetres,
                    
            InstallationItems = _mapper.Map<List<InstallationItemUpdateDto>>(installation.Items),
        };
    }

    private AddInstallationCommand GetAddInstallationCommand(ContractInstallationsUpdatedIntegrationEvent @event,
                                                             InstallationUpdatedEventDto installation)
    {
        return new AddInstallationCommand
        {
            ContractId = @event.ContractId,
            // ContractNo = @event.ContractNo,
            // ProductName = installation.ProductName,
            ContractLineId = installation.ContractLineId,
                    
            // CustomerId = @event.CustomerId,
            // CustomerName = @event.CustomerName, 
            InstallerId = installation.InstallerId,
                    
            InstallDate = installation.InstallDate,
            EstimatedStartTime = installation.EstimatedStartTime,
            EstimatedFinishTime = installation.EstimatedFinishTime,
            ActualStartTime = installation.ActualStartTime,
            ActualFinishTime = installation.ActualFinishTime,
                    
            InstallationComment = installation.InstallationComment,
            FloorType = installation.FloorType,
            InstallationMetres = installation.InstallationMetres,
                    
            InstallationItems = _mapper.Map<List<InstallationItemCreateDto>>(installation.Items),
            // InstallationAddress = _mapper.Map<InstallationAddressDto>(@event.InstallationAddress)
        };
    }
}