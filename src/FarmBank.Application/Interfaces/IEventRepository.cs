using FarmBank.Application.Models;

namespace FarmBank.Application.Interfaces;

public interface IEventRepository
{
    Task InsertAsync(Event @event, CancellationToken cancellationToken);
    Task ReplaceAsync(Event @event, CancellationToken cancellationToken);
    Task<IEnumerable<Event>> GetActivetedEventAsync(CancellationToken cancellationToken);
}