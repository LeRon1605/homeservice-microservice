using BuildingBlocks.Application.IntegrationEvent;
using BuildingBlocks.EventBus.Interfaces;
using Installations.Application.IntegrationEvents.Events.Contracts;

namespace Installations.API.Extensions;

public static class EventBusExtension
{
    public static void UseEventBus(this WebApplication app)
    {
        var eventBus = app.Services.GetRequiredService<IEventBus>();

        eventBus.Subscribe<ContractCreatedIntegrationEvent, IIntegrationEventHandler<ContractCreatedIntegrationEvent>>();
    }
}