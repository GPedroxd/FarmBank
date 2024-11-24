using FarmBank.Core.Base;

namespace FarmBank.Core.Transaction.Events;

public class TransactionPaidEvent : DomainEventBase
{
    public TransactionPaidEvent(string transactionId)
    {
        TransactionId = transactionId;
    }
    public string TransactionId { get; set; }
}
