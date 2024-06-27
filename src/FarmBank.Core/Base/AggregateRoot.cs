

namespace FarmBank.Core.Base;

public abstract class AggregateRoot : IAggregateRoot
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public abstract IReadOnlyCollection<DomainEventBase> Events { get; }
    public abstract DateTime CreatedAt { get ; init ; }
    public abstract DateTime? UpdatedAt { get ; set ; }

    public abstract void AddEvent(DomainEventBase @event);

    public abstract void ClearEvents();

    public abstract void CommitChanges();
}
