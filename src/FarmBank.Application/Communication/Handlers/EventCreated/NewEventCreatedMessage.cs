namespace FarmBank.Application.Communication.Handlers.EventCreated;

public record NewEventCreatedMessage : ICommunicationMessage
{
    public Guid EventId { get; init; }
    public string EventName { get; init; }
    public DateTime? StartsOn { get; init; }
    public DateTime? EndsOn { get; init; }

    public string GetFormatedMessage()
    {
        var period = string.Empty;

        if (StartsOn is null)
            period = "a ser definido";
        else
            period = $"{StartsOn?.ToString("dd/MM/yyyy")} até {EndsOn?.ToString("dd/MM/yyyy")}";

        var messageFullfield = MESSAGETEMPLATE.
            Replace("@EVENTNAME", EventName).
            Replace("@DATE", period);

        return messageFullfield;
    }

    private static string MESSAGETEMPLATE = "*@EVENTNAME*\r\n"+
                                            "esta entre nós !!!\r\n\r\n"+
                                            "Data: *@DATE*.\r\n\r\n" +
                                            "Mais informações em breve, "+
                                            "mas enquanto isso vc pode ir"+
                                            " depositando seu dinheiro no link a baixo \r\n"+
                                            "@LINK";         
}
