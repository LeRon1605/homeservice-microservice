using MediatR;

namespace BuildingBlocks.Domain.Event;


public interface IEvent : INotification
{
    public Guid Id  => Guid.NewGuid();
    public DateTime CreatedAt => DateTime.UtcNow;
}