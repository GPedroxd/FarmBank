using farmbank_api.Models;
using farmbank_api.Repositoies;
using MediatR;

namespace farmbank_api.Commands;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand>
{
    private readonly TransactionRepository _transactionRepository;

    public CreateTransactionCommandHandler(TransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transactionId = new PixTransaction
        (
            request.TransactionId,
            request.UserName,
            request.UserPhone,
            request.Value
        );

        await _transactionRepository.InsertAsync(transactionId, cancellationToken);
    }
}