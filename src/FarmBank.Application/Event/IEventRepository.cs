using FarmBank.Application.Models;

namespace FarmBank.Application.Event;

public interface IEventRepository
{
    Task InsertAsync(Event @event, CancellationToken cancellationToken);
    Task ReplaceAsync(Event @event, CancellationToken cancellationToken);
    Task<Event> GetByIdAsync(Guid eventId, CancellationToken cancellationToken);
    Task<IEnumerable<Event>> GetActivetedEventAsync(CancellationToken cancellationToken);
}