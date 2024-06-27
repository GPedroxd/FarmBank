using FarmBank.Application.Base;
using FarmBank.Application.Dto;
using FarmBank.Application.Event;
using FarmBank.Application.Interfaces;
using FarmBank.Application.Models;
using FarmBank.Application.Transaction;
using FarmBank.Application.WppCommands;
using Microsoft.Extensions.Logging;

namespace FarmBank.Application.Member.Commands.NewMemberDeposit;

public class NewMemberDepositCommandHandler : ICommandHandler<NewMemberDepositCommand>
{
    private readonly ILogger<NewMemberDepositCommandHandler> _logger;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IWppService _wppService;
    private readonly GeneralConfigs _configs;

    public NewMemberDepositCommandHandler(ITransactionRepository transactionRepository,
        IWppService wppService, GeneralConfigs configs, ILogger<NewMemberDepositCommandHandler> logger, IEventRepository eventRepository)
    {
        _transactionRepository = transactionRepository;
        _logger = logger;
        _wppService = wppService;
        _configs = configs;
        _eventRepository = eventRepository;
    }

    public async Task Handle(NewMemberDepositCommand request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(request.EventId, cancellationToken);

        @event.AddDeposit(new Deposit(request.TransactionId, request.Amount, request.MemberId, request.MemberName));

        _logger.LogInformation("new deposit made by {name} of amount {amount}", request.MemberName, request.Amount);

        var totalAmmount = await _transactionRepository.GetTotalAmountAsync(cancellationToken);

        var message = new NewDepositWppMessage(request.MemberName, request.Amount, 2.3m, totalAmmount, _configs.FrontendUrl);

        await _wppService.SendMessagemAsync(message, cancellationToken);


        // var rankingMembers = new RankingWppMessage(members);

        // await Task.Delay(1000);

        // await _wppService.SendMessagemAsync(rankingMembers, cancellationToken);
    }
}
