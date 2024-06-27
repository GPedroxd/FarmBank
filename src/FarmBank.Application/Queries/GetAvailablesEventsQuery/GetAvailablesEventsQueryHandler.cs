using FarmBank.Application.Base;
using FarmBank.Application.Event;

namespace FarmBank.Application.Queries.GetAvailablesEventsQuery;

public class GetAvailablesEventsQueryHandler : ICommandHandler<GetAvailablesEventsQuery, ResponseResult<IEnumerable<EventDto>>>
{
    private readonly IEventRepository _eventRepository;
    public async Task<ResponseResult<IEnumerable<EventDto>>> Handle(GetAvailablesEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.GetActivetedEventAsync(cancellationToken);

        var eventsDto = events.Select(e => new EventDto());

        return new ResponseResult<IEnumerable<EventDto>>(eventsDto);
    }
}
