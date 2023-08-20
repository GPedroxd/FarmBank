using FarmBank.Application.Base;
using FarmBank.Application.Interfaces;
using FarmBank.Application.Models;

namespace FarmBank.Application.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler : ICommandHandler<UpdateTransactionCommand>
{
    private readonly ITransactionRepository _transactionRepository;

    public UpdateTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByTransactionIdAsync(request.Data.Id, cancellationToken);

        transaction.SetAsPaidOut(request);

        await _transactionRepository.UpdateAsync(transaction, cancellationToken);
    }
}
