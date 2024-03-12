using FarmBank.Application.Base;

namespace FarmBank.Application.Models;

public class Event : IBaseEntity
{
    public Event(){ }

    public Event(string name, DateTime startsOn, DateTime endsIn)
    {
        CreatedAt = DateTime.Now;
        Name = name;
        StartedOn = startsOn;
        EndsIn = endsIn;
        UpdatedAt = DateTime.UtcNow;
        Active = true;
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name {get; init; }
    public DateTime StartedOn {get; private set; }
    public DateTime EndsIn {get; private set; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get;  set; }
    public bool Active { get; private set; }
    public DateTime? DeactivatedAt { get; private set; }
}
