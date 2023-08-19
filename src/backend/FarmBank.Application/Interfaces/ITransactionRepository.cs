using FarmBank.Application.Models;

namespace FarmBank.Application.Interfaces;

public interface ITransactionRepository
{
    Task InsertAsync(Transaction transaction, CancellationToken cancellationToken);
    Task UpdateAsync(Transaction transaction, CancellationToken cancellationToken);
    Task<Transaction> GetByTransacationIdAsync(string transactionId, CancellationToken cancellationToken);
}
