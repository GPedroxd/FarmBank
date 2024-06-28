using FarmBank.Application.Base;
using FarmBank.Application.Payment;
using FarmBank.Core.Transaction;
using FarmBank.Core.Transaction.Enums;
using Microsoft.Extensions.Logging;

namespace FarmBank.Application.Transaction.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler : ICommandHandler<UpdateTransactionCommand>
{
    private readonly ILogger<UpdateTransactionCommandHandler> _logger;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IPaymentGatewayService _paymentGatewayService;
    private readonly EventDispatcher _dispatcher;

    public async Task Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        if (!request.Action.Equals("payment.updated"))
            return;

        var transaction = await _transactionRepository.GetByTransactionIdAsync(request.Data.Id, cancellationToken);

        if (transaction is null)
            return;

        if (transaction.Status == TransactionStatus.PaidOut)
            return;

        var transactionUpdated = await _paymentGatewayService.GetTransactionAsync(transaction.TransactionId);
        
        await _transactionRepository.UpdateAsync(transaction, cancellationToken);

        _logger.LogInformation("transaction {transactionId} status {status}", transaction.TransactionId, transactionUpdated.Status);

        if (transaction.Status != TransactionStatus.PaidOut)
            return;

        await _dispatcher.DispatchEvents(transaction, cancellationToken);
    }
}
