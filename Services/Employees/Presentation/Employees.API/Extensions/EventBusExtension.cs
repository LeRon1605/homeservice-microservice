using BuildingBlocks.Application.IntegrationEvent;
using BuildingBlocks.EventBus.Interfaces;
using Employees.Application.IntegrationEvents.Events.Role;

namespace Employees.API.Extensions;

public static class EventBusExtension
{
    public static void UseEventBus(this WebApplication app)
    {
        var eventBus = app.Services.GetRequiredService<IEventBus>();

        eventBus.Subscribe<RoleCreatedIntegrationEvent, IIntegrationEventHandler<RoleCreatedIntegrationEvent>>();
    }
}