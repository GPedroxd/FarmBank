using FarmBank.Application.Base;

namespace FarmBank.Application.Member.Commands.NewMemberDeposit;

public class NewMemberDepositCommand : ICommand
{
    public NewMemberDepositCommand(Guid eventId, string transactionId, Guid memberId, string memberName, decimal amount)
    {
        EventId = eventId;
        MemberName = memberName;
        Amount = amount;
        TransactionId = transactionId;
        MemberId = memberId;
    }

    public Guid EventId { get; init; }
    public string MemberName { get; init; }
    public Guid MemberId { get; init; }
    public decimal Amount { get; init; }
    public string TransactionId { get; init; }
}
