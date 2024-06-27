namespace FarmBank.Core.Event;

public class Deposit
{
    internal Deposit()
    { }

    public Deposit(string transactionId, decimal amount, Guid memberId, string memberName)
    {
        Amount = amount;
        TransactionId = transactionId;
        MemberId = memberId;
        MemberName = memberName;
    }

    public string TransactionId { get; init; }
    public DateTime DepositDate { get; init; } = DateTime.UtcNow;
    public decimal Amount { get; init; }
    public Guid MemberId { get; init; }
    public string MemberName { get; init; }
}