using FarmBank.Application.Interfaces;
using FarmBank.Application.Models;
using FarmBank.Integration.Database;

namespace FarmBank.Integration.Repository;

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

    public async Task<double> GetTotalAmountAsync(CancellationToken cancellationToken)
    {
        var trans = await _context.Transaction.FilterByAsync(f => f.Status == TransactinoStatus.PaidOut);
        return trans.Sum(s => s.Ammount);
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
