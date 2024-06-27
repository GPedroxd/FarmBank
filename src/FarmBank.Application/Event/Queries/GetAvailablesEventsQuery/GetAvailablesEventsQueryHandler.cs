using FarmBank.Application.Base;
using FarmBank.Core.Event;

namespace FarmBank.Application.Event.Queries.GetAvailablesEventsQuery;

public class GetAvailablesEventsQueryHandler : ICommandHandler<GetAvailablesEventsQuery, ResponseResult<IEnumerable<EventDto>>>
{
    private readonly IEventRepository _eventRepository;
    public async Task<ResponseResult<IEnumerable<EventDto>>> Handle(GetAvailablesEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.GetActivetedEventAsync(cancellationToken);

        var eventsDto = events.Select(e => new EventDto(e.Id, e.Name,e.StartedOn, e.EndsOn));

        return new ResponseResult<IEnumerable<EventDto>>(eventsDto);
    }
}
