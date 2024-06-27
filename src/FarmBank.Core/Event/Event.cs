using FarmBank.Core.Base;

namespace FarmBank.Core.Event;

public class Event : AggregateRoot
{
    public Event() { }

    public Event(string name, DateTime startsOn, DateTime endsOn)
    {
        CreatedAt = DateTime.Now;
        Name = name;
        StartedOn = startsOn;
        EndsIn = endsOn;
        UpdatedAt = DateTime.Now;
        Active = true;
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; }
    public DateTime StartedOn { get; private set; }
    public DateTime EndsIn { get; private set; }
    public bool Active { get; private set; }
    public DateTime? DeactivatedAt { get; private set; }

    public List<Deposit> Deposits { get; set; } = new();
    public void Deposit(Deposit deposit)
    {
        if (!Active)
            return;

        if (Deposits.Any(a => a.TransactionId.Equals(deposit.TransactionId)))
            return;

        Deposits.Add(deposit);
    }
    public decimal TotalDeposited { get => Deposits.Sum(s => s.Amount); set => _ = value; }

    private List<DomainEventBase> _events = new();
    public override IReadOnlyCollection<DomainEventBase> Events => _events.AsReadOnly();

    public override DateTime CreatedAt { get ; init ; }
    public override DateTime? UpdatedAt { get ; set ; }

    public void Deactivate()
    {
        UpdatedAt = DateTime.Now;
        Active = false;
        DeactivatedAt = DateTime.Now;
    }

    public override void AddEvent(DomainEventBase @event)
        => _events.Add(@event);

    public override void ClearEvents()
        => _events.Clear();

    public override void CommitChanges()
    {
        throw new NotImplementedException();
    }
}
