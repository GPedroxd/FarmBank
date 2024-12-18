namespace FarmBank.Core.Transaction;

public interface ITransactionRepository
{
    Task InsertAsync(Transaction transaction, CancellationToken cancellationToken);
    Task UpdateAsync(Transaction transaction, CancellationToken cancellationToken);
    Task<decimal> GetTotalAmountAsync(CancellationToken cancellationToken);
    Task<Transaction> GetByTransactionIdAsync(string transactionId, CancellationToken cancellationToken);
}
