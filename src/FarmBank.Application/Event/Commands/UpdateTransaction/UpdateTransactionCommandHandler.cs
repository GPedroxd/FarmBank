using System.Globalization;
using FarmBank.Application.Base;
using FarmBank.Application.Commands.NewMemberDeposit;
using FarmBank.Application.Member;
using FarmBank.Application.Transaction;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FarmBank.Application.Event.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler : ICommandHandler<UpdateTransactionCommand>
{
    private readonly ILogger<UpdateTransactionCommandHandler> _logger;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMediator _mediator;
    private readonly ITransactionService _transactionService;
    private readonly IMemberRepository _memberRepository;
    public UpdateTransactionCommandHandler(ITransactionRepository transactionRepository, IMediator mediator, ILogger<UpdateTransactionCommandHandler> logger, ITransactionService transactionService, IMemberRepository memberRepository)
    {
        _transactionRepository = transactionRepository;
        _mediator = mediator;
        _logger = logger;
        _transactionService = transactionService;
        _memberRepository = memberRepository;
    }

    public async Task Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        if (!request.Action.Equals("payment.updated"))
            return;

        var transaction = await _transactionRepository.GetByTransactionIdAsync(request.Data.Id, cancellationToken);

        if (transaction is null)
            return;

        if (transaction.Status == TransactinoStatus.PaidOut)
            return;

        var transactionUpdated = await _transactionService.GetTransactionAsync(transaction.TransactionId);

        transaction.SetStatusTransaction(transactionUpdated);

        await _transactionRepository.UpdateAsync(transaction, cancellationToken);

        _logger.LogInformation("transaction {transactionId} status {status}", transaction.TransactionId, transactionUpdated.Status);

        if (transaction.Status != TransactinoStatus.PaidOut)
            return;

        var member = await _memberRepository.GetByPhoneNumberAsync(transaction.UserPhoneNumber, cancellationToken);

        var newMemberDeposit = new NewMemberDepositCommand(transaction.EventId, transaction.TransactionId, member.Id, member.Name, transaction.Amount);

        await _mediator.Send(newMemberDeposit, cancellationToken);
    }
}
