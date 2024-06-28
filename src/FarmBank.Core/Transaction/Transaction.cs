using FarmBank.Core.Base;
using FarmBank.Core.Transaction.Enums;
using FarmBank.Core.Transaction.Events;

namespace FarmBank.Core.Transaction;

public class Transaction : AggregateRoot
{
    internal Transaction()
    {
    }

    public Transaction(
        Guid eventId,
        string userPhoneNumber,
        string userEmail,
        string transactionId,
        decimal amount,
        PaymentMethod paymentMethod,
        DateTime expirationDate)
    {
        EventId = eventId;
        UserPhoneNumber = userPhoneNumber;
        UserEmail = userEmail;
        TransactionId = transactionId;
        Amount = amount;
        Status = TransactionStatus.Pending;
        PaymentMethod = paymentMethod;

        ExpirationDate = expirationDate;
    }

    public Transaction(
        Guid id,
        DateTime createdAt,
        DateTime? updatedAt,
        Guid eventId,
        string userPhoneNumber,
        string userEmail,
        string transactionId,
        decimal amount,
        TransactionStatus status,
        PaymentMethod paymentMethod,
        DateTime expirationDate,
        DateTime? paymentDate,
        string payerId) :
         this(eventId, userPhoneNumber,
          userEmail,
          transactionId,
          amount,
          paymentMethod,
          expirationDate)
    {
        Status = status;
        PaymentDate = paymentDate;
        PayerId = payerId;
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public string UserPhoneNumber { get; init; }
    public string UserEmail { get; init; }
    public string TransactionId { get; init; }
    public decimal Amount { get; init; }
    public TransactionStatus Status { get; private set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime? PaymentDate { get; private set; }
    public string? PayerId { get; private set; }
    public Guid EventId { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
    private List<DomainEventBase> _events = new();

    public void Pay(string payerId, DateTime paymentDate)
    {
        if (Status != TransactionStatus.PaidOut)
            return;

        Status = TransactionStatus.PaidOut;

        PayerId = payerId;
        PaymentDate = paymentDate;
        UpdatedAt = DateTime.Now;

        _events.Add(new TransactionPaidEvent(TransactionId));
    }
}