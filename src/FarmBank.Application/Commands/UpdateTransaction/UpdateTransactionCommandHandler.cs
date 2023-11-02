using System.Globalization;
using FarmBank.Application.Base;
using FarmBank.Application.Commands.NewMemberDeposit;
using FarmBank.Application.Commands.SendWppMessage;
using FarmBank.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FarmBank.Application.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler : ICommandHandler<UpdateTransactionCommand>
{
    private readonly ILogger<UpdateTransactionCommandHandler> _logger;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMediator _mediator;
    private readonly ITransactionService _transactionService;
    public UpdateTransactionCommandHandler(ITransactionRepository transactionRepository, IMediator mediator, ILogger<UpdateTransactionCommandHandler> logger, ITransactionService transactionService)
    {
        _transactionRepository = transactionRepository;
        _mediator = mediator;
        _logger = logger;
        _transactionService = transactionService;
    }

    public async Task Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        if(!request.Action.Equals("payment.updated"))
            return;

        var transaction = await _transactionRepository.GetByTransactionIdAsync(request.Data.Id, cancellationToken);

        if(transaction is null)
            return;

        if(transaction.Status == Models.TransactinoStatus.PaidOut)
            return;

        var transactionUpdated = await _transactionService.GetTransactionAsync(transaction.TransactionId);

        transaction.SetStatusTransaction(transactionUpdated);

        await _transactionRepository.UpdateAsync(transaction, cancellationToken);

        _logger.LogInformation($"transaction {transaction.TransactionId} status {transactionUpdated.Status}");

        if(transaction.Status != Models.TransactinoStatus.PaidOut)
            return;

        var newMemberDeposit = new NewMemberDepositCommand(transaction.TransactionId, transaction.UserPhoneNumber, transaction.Amount);

        await _mediator.Send(newMemberDeposit, cancellationToken);
    }
}
