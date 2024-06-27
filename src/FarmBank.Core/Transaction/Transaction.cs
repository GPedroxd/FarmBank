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
        SetUpdateAt(updatedAt);
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
    public override IReadOnlyCollection<DomainEventBase> Events => _events.AsReadOnly();

    public override DateTime CreatedAt { get; init; }
    public override DateTime? UpdatedAt { get; set; }

    public void Pay(string payerId)
    {
        if (Status != TransactionStatus.PaidOut)
            return;

        Status = TransactionStatus.PaidOut;

        PayerId = payerId;
        PaymentDate = DateTime.Now;

        _events.Add(new TransactionPaidEvent(TransactionId));
    }

    public void SetUpdateAt()
        => UpdatedAt = DateTime.UtcNow;

    protected void SetUpdateAt(DateTime? updatedAt)
        => UpdatedAt = updatedAt;

    public override void AddEvent(DomainEventBase @event)
        => _events.Add(@event);

    public override void ClearEvents()
        => _events.Clear();

    public override void CommitChanges()
    {
        throw new NotImplementedException();
    }
}