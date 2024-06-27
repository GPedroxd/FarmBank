namespace FarmBank.Application.Event.Queries.GetAvailablesEventsQuery;

public class EventDto
{
    public EventDto(Guid id, string name, DateTime startAt, DateTime endsAt)
    {
        Id = id;
        Name = name;
        StartAt = startAt;
        EndsAt = endsAt;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndsAt { get; set; }
}
