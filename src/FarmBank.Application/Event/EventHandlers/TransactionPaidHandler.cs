using FarmBank.Application.Base;
using FarmBank.Core.Event;
using FarmBank.Core.Member;
using FarmBank.Core.Transaction;
using FarmBank.Core.Transaction.Events;
using MediatR;

namespace FarmBank.Application.Event.EventHandlers;

public class TransactionPaidHandler : INotificationHandler<TransactionPaidEvent>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly EventDispatcher  _dispatcher;

    public async Task Handle(TransactionPaidEvent eventNotification, CancellationToken cancellationToken)
    {
        var transation = await _transactionRepository.GetByTransactionIdAsync(eventNotification.TransactionId, cancellationToken);

        var @event = await _eventRepository.GetByIdAsync(transation.EventId, cancellationToken);

        var member = await _memberRepository.GetByPhoneNumberAsync(transation.UserPhoneNumber, cancellationToken);

        var deposit = new Deposit(transation.TransactionId, transation.Amount, member.Id, member.Name);

        @event.Deposit(deposit);

        await _eventRepository.ReplaceAsync(@event, cancellationToken);

        await _dispatcher.DispatchEvents(@event);
    }
}
