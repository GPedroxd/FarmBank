using FarmBank.Application.Base;
using FarmBank.Application.Commands.SendWppMessage;
using FarmBank.Application.Interfaces;
using MediatR;

namespace FarmBank.Application.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler : ICommandHandler<UpdateTransactionCommand>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMediator _mediator;
    public UpdateTransactionCommandHandler(ITransactionRepository transactionRepository, IMediator mediator)
    {
        _transactionRepository = transactionRepository;
        _mediator = mediator;
    }

    public async Task Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByTransactionIdAsync(request.Data.Id, cancellationToken);

        transaction.SetAsPaidOut(request);

        await _transactionRepository.UpdateAsync(transaction, cancellationToken);

        var sendWppMessage = new SendWppMessageCommand(transaction.UserEmail, transaction.UserName, transaction.Amount);

        await _mediator.Send(sendWppMessage, cancellationToken);
    }
}
