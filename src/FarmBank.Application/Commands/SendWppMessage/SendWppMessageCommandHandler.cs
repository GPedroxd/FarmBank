using FarmBank.Application.Base;
using FarmBank.Application.Interfaces;
using FarmBank.Application.Models;

namespace FarmBank.Application.Commands.SendWppMessage;

public class SendWppMessageCommandHandler : ICommandHandler<SendWppMessageCommand>
{
    private readonly IWppService _wppService;
    private readonly ITransactionRepository _transactionRepository;
    public SendWppMessageCommandHandler(IWppService wppService, ITransactionRepository transactionRepository)
    {
        _wppService = wppService;
        _transactionRepository = transactionRepository;
    }
    
    public async Task Handle(SendWppMessageCommand request, CancellationToken cancellationToken)
    {
        var totalAmmount = await _transactionRepository.GetTotalAmountAsync(cancellationToken);

        var message = FormatMessage(request, totalAmmount);

        await _wppService.SendMessagemAsync(message);
    }

    private string FormatMessage(SendWppMessageCommand command, decimal total)
     => Message.Deposit.Replace("@NAME", "o corno do gadelha").
                            Replace("@AMMOUNT", command.Ammount.ToString()).
                            Replace("@TOTALAMMOUNT", total.ToString()).
                            Replace("@LINK", "https://localhost");
}
