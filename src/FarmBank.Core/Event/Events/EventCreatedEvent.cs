using FarmBank.Core.Base;
using MediatR;

namespace FarmBank.Core.Event.Events;

public class EventCreatedEvent : DomainEventBase
{
    public EventCreatedEvent(Guid eventId, string eventName)
    {
        EventId = eventId;
        EventName = eventName;
    }

    public Guid EventId { get; init; } 
    public string EventName { get; init; }
}
