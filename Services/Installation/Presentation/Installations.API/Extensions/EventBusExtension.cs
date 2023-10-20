using BuildingBlocks.Application.IntegrationEvent;
using BuildingBlocks.EventBus.Interfaces;
using Installations.Application.IntegrationEvents.Events.Contracts;
using Installations.Application.IntegrationEvents.Events.Employees;
using Installations.Application.IntegrationEvents.Events.Materials;
using Installations.Domain.MaterialAggregate;

namespace Installations.API.Extensions;

public static class EventBusExtension
{
    public static void UseEventBus(this WebApplication app)
    {
        var eventBus = app.Services.GetRequiredService<IEventBus>();

        eventBus.Subscribe<ContractCreatedIntegrationEvent, IIntegrationEventHandler<ContractCreatedIntegrationEvent>>();
        // eventBus.Subscribe<ContractInstallationsUpdatedIntegrationEvent, IIntegrationEventHandler<ContractInstallationsUpdatedIntegrationEvent>>();
        
        eventBus.Subscribe<MaterialAddedIntegrationEvent, IIntegrationEventHandler<MaterialAddedIntegrationEvent>>();
        eventBus.Subscribe<MaterialDeletedIntegrationEvent, IIntegrationEventHandler<MaterialDeletedIntegrationEvent>>();
        eventBus.Subscribe<MaterialUpdatedIntegrationEvent, IIntegrationEventHandler<MaterialUpdatedIntegrationEvent>>();
        
        eventBus.Subscribe<EmployeeAddedIntegrationEvent, IIntegrationEventHandler<EmployeeAddedIntegrationEvent>>();
        eventBus.Subscribe<EmployeeDeactivatedIntegrationEvent, IIntegrationEventHandler<EmployeeDeactivatedIntegrationEvent>>();
        eventBus.Subscribe<EmployeeUpdatedIntegrationEvent, IIntegrationEventHandler<EmployeeUpdatedIntegrationEvent>>();
    }
}