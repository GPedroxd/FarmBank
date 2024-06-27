namespace FarmBank.Application.Queries.GetAvailablesEventsQuery;

public class EventDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndsAt { get; set; }        
}
