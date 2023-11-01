using System.Numerics;
using FarmBank.Application.Base;
using FarmBank.Application.Commands.SendWppMessage;
using FarmBank.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FarmBank.Application.Commands.NewMemberDeposit;

public class NewMemberDepositCommandHandler : ICommandHandler<NewMemberDepositCommand>
{
    private readonly ILogger<NewMemberDepositCommandHandler> _logger;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IMediator _mediator;

    public NewMemberDepositCommandHandler(IMemberRepository memberRepository, IMediator mediator, ITransactionRepository transactionRepository, ILogger<NewMemberDepositCommandHandler> logger)
    {
        _memberRepository = memberRepository;
        _mediator = mediator;
        _transactionRepository = transactionRepository;
        _logger = logger;
    }

    public async Task Handle(NewMemberDepositCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByPhoneNumberAsync(request.PhoneNumber, cancellationToken);

        if (member is null) return;

        member.AddDeposit(new Models.Deposit(request.TransactionId, request.Amount));

        _logger.LogInformation($"new deposit made by {member.Name} of amount {request.Amount}");
        await _memberRepository.ReplaceAsync(member, cancellationToken);

        var totalAmmount = await _transactionRepository.GetTotalAmountAsync(cancellationToken);

        await _mediator.Send(new SendWppMessageCommand(member.Name, request.Amount, member.TotalDeposited, totalAmmount));
    }
}
