using FarmBank.Application.Base;
using FarmBank.Application.Dto;
using FarmBank.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace FarmBank.Application.Commands.NewMemberDeposit;

public class NewMemberDepositCommandHandler : ICommandHandler<NewMemberDepositCommand>
{
    private readonly ILogger<NewMemberDepositCommandHandler> _logger;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IWppService _wppService;
    private readonly GeneralConfigs _configs;

    public NewMemberDepositCommandHandler(IMemberRepository memberRepository, ITransactionRepository transactionRepository, 
        IWppService wppService, GeneralConfigs configs, ILogger<NewMemberDepositCommandHandler> logger)
    {
        _memberRepository = memberRepository;
        _transactionRepository = transactionRepository;
        _logger = logger;
        _wppService = wppService;
        _configs = configs;
    }

    public async Task Handle(NewMemberDepositCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByPhoneNumberAsync(request.PhoneNumber, cancellationToken);

        if (member is null) return;

        member.AddDeposit(new Models.Deposit(request.TransactionId, request.Amount));

        _logger.LogInformation($"new deposit made by {member.Name} of amount {request.Amount}");
        await _memberRepository.ReplaceAsync(member, cancellationToken);

        var totalAmmount = await _transactionRepository.GetTotalAmountAsync(cancellationToken);

        var message = new NewDepositWppMessage(member.Name, request.Amount, member.TotalDeposited, totalAmmount, _configs.FrontendUrl);

        await _wppService.SendMessagemAsync(message);
    }
}
