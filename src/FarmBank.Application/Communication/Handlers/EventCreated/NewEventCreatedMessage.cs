namespace FarmBank.Application.Communication.Handlers.EventCreated;

public record NewEventCreatedMessage : ICommunicationMessage
{
    public Guid EventId { get; init; }
    public string EventName { get; init; }
    public DateTime? StartsOn { get; init; }
    public DateTime? EndsOn { get; init; }

    public string GetFormatedMessage()
    {
        return MESSAGETEMPLATE;
    }

    private static string MESSAGETEMPLATE = $@"
            NÓIS DA O CU, PORRA
HAHAHAHAHAHAHAHAHAHAHAHAHAHAHHA
                    ";
}
