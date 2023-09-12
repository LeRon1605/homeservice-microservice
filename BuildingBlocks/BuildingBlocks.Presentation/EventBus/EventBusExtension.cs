using BuildingBlocks.EventBus.Interfaces;
using BuildingBlocks.EventBus.RabbitMQ;
using BuildingBlocks.EventBus.SubscriptionManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace BuildingBlocks.Presentation.EventBus;

public static class EventBusExtension
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        // {
        //   "EventBus": {
        //     "HostName": "...",
        //     "SubscriptionClientName": "...",
        //     "UserName": "...",
        //     "Password": "...",
        //     "RetryCount": 1
        //   }
        // }
        
        var eventBusSection = configuration.GetSection("EventBus");
        
        services.AddSingleton<IEventBusSubscriptionManager, InMemoryEventBusSubscriptionManager>();
        
        services.AddSingleton<IRabbitMqPersistentConnection>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

            var factory = new ConnectionFactory()
            {
                HostName = eventBusSection["HostName"],
                UserName = eventBusSection["UserName"],
                Password = eventBusSection["Password"],
                Port = int.Parse(eventBusSection["Port"]),
                DispatchConsumersAsync = true
            };

            var retryCount = eventBusSection.GetValue("RetryCount", 5);

            return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
        });

        services.AddSingleton<IEventBus, EventBusRabbitMq>(serviceProvider =>
        {
            var subscriptionClientName = eventBusSection["SubscriptionClientName"];
            
            ArgumentNullException.ThrowIfNull(subscriptionClientName);
            
            var rabbitMqPersistentConnection = serviceProvider.GetRequiredService<IRabbitMqPersistentConnection>();
            var logger = serviceProvider.GetRequiredService<ILogger<EventBusRabbitMq>>();
            var eventBusSubscriptionsManager = serviceProvider.GetRequiredService<IEventBusSubscriptionManager>();
            var retryCount = eventBusSection.GetValue("RetryCount", 5);

            return new EventBusRabbitMq(
                rabbitMqPersistentConnection, 
                logger, 
                eventBusSubscriptionsManager,
                serviceProvider, 
                subscriptionClientName, 
                retryCount);
        });
        
        return services;
    }
}