using FarmBank.Application.Base;
using FarmBank.Core.Event;

namespace FarmBank.Application.Event.Commands.CreateEvent;

public class CreateEventCommandHandler : ICommandHandler<CreateEventCommand>
{
    private IEventRepository _eventRepository;
    private EventDispatcher _dispatcher;

    public CreateEventCommandHandler(IEventRepository eventRepository, EventDispatcher dispatcher)
    {
        _eventRepository = eventRepository;
        _dispatcher = dispatcher;
    }

    public async Task Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        //some validation

        var @event = new Core.Event.Event(request.EventName, request.StartsOn, request.EndsOn);

        await _eventRepository.InsertAsync(@event, cancellationToken);

        await _dispatcher.DispatchEvents(@event, cancellationToken);
    }
}
