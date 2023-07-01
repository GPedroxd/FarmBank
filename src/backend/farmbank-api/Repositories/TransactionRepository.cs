using farmbank_api.Models;

namespace farmbank_api.Repositoies;

public class TransactionRepository
{
    public async Task InsertAsync(PixTransaction transaction, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(PixTransaction transaction, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}