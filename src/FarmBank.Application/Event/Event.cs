using FarmBank.Application.Base;

namespace FarmBank.Application.Event;

public class Event : IBaseEntity
{
    public Event() { }

    public Event(string name, DateTime startsOn, DateTime endsIn)
    {
        CreatedAt = DateTime.Now;
        Name = name;
        StartedOn = startsOn;
        EndsIn = endsIn;
        UpdatedAt = DateTime.Now;
        Active = true;
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; }
    public DateTime StartedOn { get; private set; }
    public DateTime EndsIn { get; private set; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
    public bool Active { get; private set; }
    public DateTime? DeactivatedAt { get; private set; }

    public List<Deposit> Deposits { get; set; } = new();
    public void AddDeposit(Deposit deposit)
    {
        if (!Active)
            return;

        if (Deposits.Any(a => a.TransactionId.Equals(deposit.TransactionId)))
            return;

        Deposits.Add(deposit);
    }
    public decimal TotalDeposited { get => Deposits.Sum(s => s.Amount); set => _ = value; }

    public void Deactivate()
    {
        UpdatedAt = DateTime.Now;
        Active = false;
        DeactivatedAt = DateTime.Now;
    }
}
