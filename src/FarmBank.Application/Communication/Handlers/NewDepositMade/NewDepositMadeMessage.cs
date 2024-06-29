namespace FarmBank.Application.Communication.Handlers.NewDepositMade;

public class NewDepositMadeMessage : ICommunicationMessage
{
    public string EventName { get; init; }
    public NewDeposit NewDepositInformation { get; init; }
    public IEnumerable<Deposit> Placements { get; init; }

    public string GetFormatedMessage()
    {
        throw new NotImplementedException();
    }
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
