using FarmBank.Core.Event;
using FarmBank.Core.Event.Events;
using MediatR;

namespace FarmBank.Application.Communication.Handlers.EventCreated;

public class EventCreatedEventHandler : INotificationHandler<EventCreatedEvent>
{
    private readonly IEventRepository _eventRepository;
    private readonly ICommunicationService _communicatonService;

    public EventCreatedEventHandler(IEventRepository eventRepository, ICommunicationService communicatonService)
    {
        _eventRepository = eventRepository;
        _communicatonService = communicatonService;
    }

    public async Task Handle(EventCreatedEvent @eventCreated, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(eventCreated.EventId, cancellationToken);

        var communicationMsg = new NewEventCreatedMessage()
        {
            EventName = @event.Name,
            EndsOn = @event.EndsOn,
            EventId = @event.Id,
            StartsOn = @event.StartsOn,
        };

        await _communicatonService.SendMessagemAsync(communicationMsg, cancellationToken);
    }
}
