﻿using BuildingBlocks.Application.IntegrationEvent;

namespace BuildingBlocks.EventBus.SubscriptionManager;

public class InMemoryEventBusSubscriptionManager : IEventBusSubscriptionManager
{
    // Store event name: handlers
    private readonly Dictionary<string, List<Type>> _subscriptions;
    
    // Store event types so that we can retrieve event type by name
    private readonly List<Type> _eventTypes;

    public bool IsEmpty => _subscriptions.Count == 0;
    public event EventHandler<string>? OnEventRemoved;

    public InMemoryEventBusSubscriptionManager()
    {
        _subscriptions = new Dictionary<string, List<Type>>();
        _eventTypes = new List<Type>();
    }
    
    public void AddSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        var eventName = GetEventName<T>();
        var handlerType = typeof(TH);
        
        if (!HasSubscriptionsForEvent<T>())
        {
            _subscriptions.Add(eventName, new List<Type>());
        }
        
        if (HasAlreadyRegisteredForEvent(eventName, handlerType))
        {
            throw new ArgumentException($"Handler Type {handlerType} already registered for '{eventName}'");
        }
        
        AddHandlerForEvent(eventName, handlerType);
        StoreEventType(typeof(T));
    }

    public void RemoveSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        var eventName = GetEventName<T>();
        var handlerType = typeof(TH);

        if (!HasSubscriptionsForEvent<T>())
        {
            return;
        }

        var handlerTypeToRemove = GetHandlerForEvent(eventName, handlerType);
        if (handlerTypeToRemove == null)
        {
            return;
        }

        _subscriptions[eventName].Remove(handlerTypeToRemove);

        if (!_subscriptions[eventName].Any())
        {
            _subscriptions.Remove(eventName);
            OnEventRemoved?.Invoke(this, eventName);
            
            RemoveEventType(eventName);
        }
    }

    public IEnumerable<Type> GetHandlersForEvent<T>() where T : IntegrationEvent
    {
        var eventName = GetEventName<T>();
        return GetHandlersForEvent(eventName);
    }

    public IEnumerable<Type> GetHandlersForEvent(string eventName)
    {
        if (!HasSubscriptionsForEvent(eventName))
        {
            return Array.Empty<Type>();
        }

        return _subscriptions[eventName];
    }

    public Type? GetEventTypeByName(string eventName)
    {
        return _eventTypes.FirstOrDefault(t => t.Name == eventName);   
    }

    public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
    {
        var eventKey = GetEventName<T>();
        return HasSubscriptionsForEvent(eventKey);
    }

    public bool HasSubscriptionsForEvent(string eventName)
    {
        return _subscriptions.ContainsKey(eventName);
    }

    public void Clear()
    {
        foreach (var eventName in _subscriptions.Keys)
        {
            OnEventRemoved?.Invoke(this, eventName);
        }
        
        _subscriptions.Clear();
        _eventTypes.Clear();
    }

    public string GetEventName<T>()
    {
        return typeof(T).Name;
    }
    
    private void StoreEventType(Type eventType)
    {
        if (!_eventTypes.Contains(eventType))
        {
            _eventTypes.Add(eventType);
        }
    }

    private void AddHandlerForEvent(string eventName, Type handlerType)
    {
        _subscriptions[eventName].Add(handlerType);
    }

    private bool HasAlreadyRegisteredForEvent(string eventName, Type handlerType)
    {
        return _subscriptions[eventName].Any(s => s == handlerType);
    }
    
    private void RemoveEventType(string eventName)
    {
        var eventTypeToRemove = _eventTypes.FirstOrDefault(x => x.Name == eventName);
        if (eventTypeToRemove != null)
        {
            _eventTypes.Remove(eventTypeToRemove);
        }
    }

    private Type? GetHandlerForEvent(string eventName, Type handlerType)
    {
        return _subscriptions[eventName].FirstOrDefault(x => x == handlerType);
    }
}