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
    public UpdateTransactionCommandHandler(ITransactionRepository transactionRepository, IMediator mediator, ILogger<UpdateTransactionCommandHandler> logger)
    {
        _transactionRepository = transactionRepository;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByTransactionIdAsync(request.Data.Id, cancellationToken);

        if(transaction is null)
            return;

        transaction.SetAsPaidOut(request);

        await _transactionRepository.UpdateAsync(transaction, cancellationToken);

        _logger.LogInformation($"transaction {transaction.TransactionId} payed at {transaction.PaymentDate}");

        var newMemberDeposit = new NewMemberDepositCommand(transaction.TransactionId, transaction.UserPhoneNumber, transaction.Amount);

        await _mediator.Send(newMemberDeposit, cancellationToken);
    }
}
