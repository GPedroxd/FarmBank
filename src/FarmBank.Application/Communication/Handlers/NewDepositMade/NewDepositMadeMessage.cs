namespace FarmBank.Application.Communication.Handlers.NewDepositMade;

public class NewDepositMadeMessage : ICommunicationMessage
{
    public string EventName { get; init; }
    public NewDeposit NewDepositInformation { get; init; }
    public IEnumerable<Deposit> Placements { get; init; }

    public string GetFormatedMessage()
    {
        var standingsString = string.Empty;

        var pos = 1;

        foreach (var standing in Placements.OrderByDescending(o => o.TotalDeposited))
        {
            var emoji = "🪙";
            if (pos == 1)
                emoji = "💎";
            else if (pos == 2)
                emoji = "💰";
            else if (pos == 3)
                emoji = "🤑";

            standingsString += $"{emoji} {pos++}° {standing.MemberName} - R${standing.TotalDeposited.ToString("#.##")}\r\n";
        }

        var massageFullfield = TEMPLATE.Replace("@NAME", NewDepositInformation.MemberName).
                Replace("@DEPOSIT", NewDepositInformation.AmountNewDosit.ToString("#.##")).
                Replace("@EVENT", EventName).
                Replace("@PLACEMENT", NewDepositInformation.Placement.ToString()).
                Replace("@TOTAL", NewDepositInformation.TotalDeposited.ToString("#.##")).
                Replace("@STANDINGS", standingsString);

        return massageFullfield;
    }

    private static string TEMPLATE = "O nosso querido *@NAME* contribuiu "+
                                    "com *R$@DEPOSIT* para o evento \r\n"+ 
                                    "*@EVENT*\r\n\r\n"+
                                    "Total depositado: R$@TOTAL\r\n" +
                                    "Posição atual: @PLACEMENT\r\n\r\n" +
                                    "@EVENT:\r\n"+
                                    "@STANDINGS\r\n"+
                                    "@LINK";
}

public record Deposit
{
    public string MemberName { get; init; }
    public decimal TotalDeposited { get; init; }
}

public record NewDeposit : Deposit
{
    public decimal AmountNewDosit { get; init; }
    public int Placement { get; init; }
}
