namespace FarmBank.Core.Base;

public abstract class AggregateRoot : IAggregateRoot
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    private List<DomainEventBase> _events = new();
    public  IReadOnlyCollection<DomainEventBase> Events { get => _events.AsReadOnly(); }
    public  DateTime CreatedAt { get ; init ; }
    public  DateTime? UpdatedAt { get ; set ; }

     public void AddEvent(DomainEventBase @event)
        => _events.Add(@event);

    public void ClearEvents()
        => _events.Clear();

    public void CommitChanges()
    {
        throw new NotImplementedException();
    }
}
