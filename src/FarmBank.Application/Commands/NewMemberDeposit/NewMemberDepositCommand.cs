using FarmBank.Application.Base;

namespace FarmBank.Application.Commands.NewMemberDeposit;

public class NewMemberDepositCommand : ICommand
{
    public NewMemberDepositCommand(string transactionId, string phoneNumber, decimal amount)
    {
        PhoneNumber = phoneNumber;
        Amount = amount;
        TransactionId = transactionId;
    }

    public string PhoneNumber { get; init; }
    public decimal Amount { get; init; }
    public string TransactionId {get; init; }
}
