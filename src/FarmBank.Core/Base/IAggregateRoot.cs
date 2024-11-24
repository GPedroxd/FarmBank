namespace FarmBank.Core.Base;

public interface IAggregateRoot
{
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
    IReadOnlyCollection<DomainEventBase> Events { get; }
    void AddEvent(DomainEventBase @event);
    void CommitChanges();
    void ClearEvents();
}
