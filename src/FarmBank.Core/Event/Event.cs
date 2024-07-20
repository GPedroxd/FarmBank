using FarmBank.Core.Base;
using FarmBank.Core.Event.Events;

namespace FarmBank.Core.Event;

public class Event : AggregateRoot
{
    internal Event() { }

    public Event(string name, DateTime? startsOn, DateTime? endsOn)
    {
        CreatedAt = DateTime.Now;
        Name = name;
        StartsOn = startsOn;
        EndsOn = endsOn;
        UpdatedAt = DateTime.Now;
        _active = true;

        AddEvent(new EventCreatedEvent(this.Id, this.Name));
    }
    public string Name { get; init; }
    public DateTime? StartsOn { get; private set; }
    public DateTime? EndsOn { get; private set; }
    private bool? _active;
    public bool Active
    {
        get
        {
            if (_active != null)
                return _active.Value;

            if (DeactivatedAt is not null)
            {
                _active = false;

                return _active.Value;
            }

            if (DateTime.Now >= StartsOn)
            {
                Deactivate();
                _active = false;

                return _active.Value;
            }

            _active = true;
            return _active.Value;
        }
        private set
        {
            _ = value;
        }
    }
    public DateTime? DeactivatedAt { get; private set; }

    private List<Deposit> _deposits { get; set; } = new();
    public IReadOnlyCollection<Deposit> Deposits { get => _deposits.AsReadOnly(); }
    public void Deposit(Deposit deposit)
    {
        if (!Active)
            return;

        if (_deposits.Any(a => a.TransactionId.Equals(deposit.TransactionId)))
            return;

        _deposits.Add(deposit);

        var depositEvent = new DepositMadeEvent(deposit.MemberId, deposit.MemberName, this.Id,deposit.Amount);

        AddEvent(depositEvent);
    }

    public void deactivate()
    {
        DeactivatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        _active = false;
    }

    public decimal TotalDeposited { get => _deposits.Sum(s => s.Amount); }

    public void Deactivate()
    {
        UpdatedAt = DateTime.Now;
        _active = false;
        DeactivatedAt = DateTime.Now;
    }
}
