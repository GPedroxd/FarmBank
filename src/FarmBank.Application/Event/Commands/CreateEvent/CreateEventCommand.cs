using FarmBank.Application.Base;

namespace FarmBank.Application.Event.Commands.CreateEvent;

public class CreateEventCommand : ICommand
{
    public string EventName { get; set; }
    public DateTime StartsOn { get; set; }
    public DateTime EndsOn { get; set; }
}
