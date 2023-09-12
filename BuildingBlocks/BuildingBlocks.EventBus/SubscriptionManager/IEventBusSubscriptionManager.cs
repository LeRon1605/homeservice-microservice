using BuildingBlocks.Application.IntegrationEvent;

namespace BuildingBlocks.EventBus.SubscriptionManager;

public interface IEventBusSubscriptionManager
{
    bool IsEmpty { get; }
    event EventHandler<string> OnEventRemoved;
    
    void AddSubscription<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;
    
    void RemoveSubscription<T, TH>()
        where TH : IIntegrationEventHandler<T>
        where T : IntegrationEvent;

    IEnumerable<Type> GetHandlersForEvent<T>()
        where T : IntegrationEvent;

    IEnumerable<Type> GetHandlersForEvent(string eventName);

    Type? GetEventTypeByName(string eventName);
    
    bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;
    bool HasSubscriptionsForEvent(string eventName);
    
    void Clear();
    string GetEventName<T>();
}