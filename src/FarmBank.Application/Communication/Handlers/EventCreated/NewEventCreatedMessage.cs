namespace FarmBank.Application.Communication.Handlers.EventCreated;

public record NewEventCreatedMessage : ICommunicationMessage
{
    public Guid EventId { get; init; }
    public string EventName { get; init; } = default!;
    public DateTime? StartsOn { get; init; }
    public DateTime? EndsOn { get; init; }

    public string GetFormatedMessage()
    {
        var period = string.Empty;

        if (StartsOn is null)
            period = "*a ser definido* \r\n"
                     + "Mais informações em breve!!!";
        else
            period = $"*{StartsOn?.ToString("dd/MM/yyyy")} até {EndsOn?.ToString("dd/MM/yyyy")}*";

        var messageFullfield = MESSAGEHEADER.
            Replace("@EVENTNAME", EventName).
            Replace("@DATE", period);

        messageFullfield += SOONINFO.Replace("@PREEVENTNAME", EventName.Replace(" ", "-"));

        return messageFullfield;
    }

    private static string MESSAGEHEADER = "🚨🚨 *@EVENTNAME* 🚨🚨\r\n" +
                                            "esta entre nós !!!\r\n\r\n" +
                                            "Data: @DATE.\r\n\r\n";

    private static string SOONINFO = "vc já pode ir depositando seu dinheiro usando o comando:\r\n" +
                                     "!pix @PREEVENTNAME {valor}\r\n " +
                                     "ou no link abaixo:\r\n" +
                                     "@LINK";
}
