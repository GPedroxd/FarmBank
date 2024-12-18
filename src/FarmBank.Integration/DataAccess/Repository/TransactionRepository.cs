using FarmBank.Application.Transaction;
using FarmBank.Core.Transaction;
using FarmBank.Core.Transaction.Enums;
using FarmBank.Integration.DataAccess.Database;

namespace FarmBank.Integration.DataAccess.Repository;

public class TransactionRepository : ITransactionRepository
{
    private readonly MongoContext _context;

    public TransactionRepository(MongoContext context)
    {
        _context = context;
    }

    public async Task<Transaction> GetByTransactionIdAsync(string transactionId, CancellationToken cancellationToken)
    {
        return await _context.Transaction.FirstOrDefaultAsync(f => f.TransactionId == transactionId);
    }

    public async Task<decimal> GetTotalAmountAsync(CancellationToken cancellationToken)
    {
        var trans = await _context.Transaction.FilterByAsync(f => f.Status == TransactionStatus.PaidOut);
        return trans.Sum(s => s.Amount);
    }

    public async Task InsertAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        await _context.Transaction.InsertAsync(transaction);
    }

    public async Task UpdateAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        await _context.Transaction.ReplaceAsync(transaction);
    }
}
