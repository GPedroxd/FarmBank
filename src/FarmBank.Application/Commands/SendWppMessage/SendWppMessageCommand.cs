using FarmBank.Application.Base;

namespace FarmBank.Application.Commands.SendWppMessage;

public class SendWppMessageCommand:  ICommand
{
    public SendWppMessageCommand(string userName, decimal amountDeposit, decimal memberTotalAmount, decimal totalAmount)
    {
        UserName = userName;
        AmountDeposit = amountDeposit;
        MemberTotalAmount = memberTotalAmount;
        TotalAmount = totalAmount;
    }

    public string UserName { get; set; }
    public decimal AmountDeposit { get; init; }
    public decimal MemberTotalAmount { get; init; }
    public decimal TotalAmount { get;  init ; }
}
