using MediatR;

namespace farmbank_api.Commands;

public record CreateTransactionCommand(string TransactionId, string UserName, string UserPhone, decimal Value) : IRequest;
