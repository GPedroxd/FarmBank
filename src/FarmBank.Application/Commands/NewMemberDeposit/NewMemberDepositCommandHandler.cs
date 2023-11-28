using FarmBank.Application.Base;
using FarmBank.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace FarmBank.Application.Commands.NewMemberDeposit;

public class NewMemberDepositCommandHandler : ICommandHandler<NewMemberDepositCommand>
{
    private readonly ILogger<NewMemberDepositCommandHandler> _logger;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IWppService _wppService;

    public NewMemberDepositCommandHandler(IMemberRepository memberRepository, ITransactionRepository transactionRepository, ILogger<NewMemberDepositCommandHandler> logger, IWppService wppService)
    {
        _memberRepository = memberRepository;
        _transactionRepository = transactionRepository;
        _logger = logger;
        _wppService = wppService;
    }

    public async Task Handle(NewMemberDepositCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByPhoneNumberAsync(request.PhoneNumber, cancellationToken);

        if (member is null) return;

        member.AddDeposit(new Models.Deposit(request.TransactionId, request.Amount));

        _logger.LogInformation($"new deposit made by {member.Name} of amount {request.Amount}");
        await _memberRepository.ReplaceAsync(member, cancellationToken);

        var totalAmmount = await _transactionRepository.GetTotalAmountAsync(cancellationToken);

        var message = new NewDepositWppMessage(member.Name, request.Amount, member.TotalDeposited, totalAmmount, null);

        await _wppService.SendMessagemAsync(message);
    }
}
