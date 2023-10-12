using FarmBank.Application.Base;
using FarmBank.Application.Interfaces;
using FarmBank.Application.Models;

namespace FarmBank.Application.Commands.SendWppMessage;

public class SendWppMessageCommandHandler : ICommandHandler<SendWppMessageCommand>
{
    private readonly IWppService _wppService;
    public SendWppMessageCommandHandler(IWppService wppService)
    {
        _wppService = wppService;
    }
    
    public async Task Handle(SendWppMessageCommand request, CancellationToken cancellationToken)
    {
        var message = FormatMessage(request);

        await _wppService.SendMessagemAsync(message);
    }

    private string FormatMessage(SendWppMessageCommand command)
     => Message.Deposit.Replace("@NAME", command.UserName).
                            Replace("@AMMOUNT", command.AmountDeposit.ToString()).
                            Replace("@MEMBERAMOUNTTOTAL", command.MemberTotalAmount.ToString()).
                            Replace("@TOTALAMOUNT", command.TotalAmount.ToString()).
                            Replace("@LINK", "https://farmbank-front.vercel.app/" ?? "https://localhost:5000");
}
