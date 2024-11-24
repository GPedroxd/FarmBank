using FarmBank.Core.Event;
using FarmBank.Integration.DataAccess.Database;

namespace FarmBank.Integration.DataAccess.Repository;
public class EventRepository : IEventRepository
{
    private readonly MongoContext _context;

    public EventRepository(MongoContext context)
        => _context = context;


    public Task<IEnumerable<Event>> GetActivetedEventAsync(CancellationToken cancellationToken)
        => _context.Event.FilterByAsync(filter => filter.Active);


    public Task<Event> GetByIdAsync(Guid eventId, CancellationToken cancellationToken)
        => _context.Event.FindByIdAsync(eventId);


    public async Task InsertAsync(Event @event, CancellationToken cancellationToken)
    {
        var result = await _context.Event.InsertAsync(@event);

        if (result < 1)
            throw new Exception("Failed to Insert.");
    }

    public async Task ReplaceAsync(Event @event, CancellationToken cancellationToken)
    {
        var result = await _context.Event.ReplaceAsync(@event);

        if (result < 1)
            throw new Exception("Failed to Replace item.");
    }
}
