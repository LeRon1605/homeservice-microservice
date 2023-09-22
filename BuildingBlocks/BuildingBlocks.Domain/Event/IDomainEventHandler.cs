using MediatR;

namespace BuildingBlocks.Domain.Event;

public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEvent
{
}