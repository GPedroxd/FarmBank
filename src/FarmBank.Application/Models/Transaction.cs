using FarmBank.Application.Base;
using FarmBank.Application.Commands.UpdateTransaction;
using FarmBank.Application.Dto;

namespace FarmBank.Application.Models;

public class Transaction : IBaseEntity
{
    public Transaction()
    {
    }

    public Transaction(
        string userPhoneNumber, 
        string userEmail, 
        string transactionId, 
        decimal amount, 
        string pixCopyPaste, 
        string qRCode,
        DateTime expirationDate)
    {
        UserPhoneNumber = userPhoneNumber;
        UserEmail = userEmail;
        TransactionId = transactionId;
        Amount = amount;
        Status = TransactinoStatus.Pending;
        PixCopyPaste = pixCopyPaste;
        QRCode = qRCode;
        
        ExpirationDate = expirationDate;
    }

    public Transaction(
        Guid id,
        DateTime createdAt,
        DateTime? updatedAt,
        string userPhoneNumber, 
        string userEmail, 
        string transactionId, 
        decimal amount, 
        TransactinoStatus status, 
        string pixCopyPaste, 
        string qRCode, 
        DateTime expirationDate, 
        DateTime? paymentDate, 
        string payerId) :
         this(userPhoneNumber,
          userEmail, 
          transactionId, 
          amount, 
          pixCopyPaste, 
          qRCode, 
          expirationDate)
    {
        Status = status;
        PaymentDate = paymentDate;
        PayerId = payerId;
        Id = id;
        CreatedAt = createdAt;
        SetUpdateAt(updatedAt);
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string UserPhoneNumber { get; init; }
    public string UserEmail { get; init; }
    public string TransactionId { get; init; }
    public decimal Amount { get; init; }
    public TransactinoStatus Status { get; private set; }
    public string PixCopyPaste { get; init; }
    public string QRCode { get; init; }
    public DateTime ExpirationDate{ get; set; }
    public DateTime? PaymentDate { get; private set; }
    public string PayerId { get; private set; }
    public Guid EventId { get; private set; }

    public void SetStatusTransaction(MarcadoPagoTransactionInfo update)
    {
        Status = GetStatus(update.Status);
        SetUpdateAt();

        if(Status != TransactinoStatus.PaidOut)
            return;

        PaymentDate = DateTime.UtcNow;
        PayerId = update.Payer.Id.ToString();
    }

    private TransactinoStatus GetStatus(string statusRaw)
    {
        if(statusRaw.Equals("approved"))
            return TransactinoStatus.PaidOut;

        return TransactinoStatus.Error;
    }

    public void SetUpdateAt()
        => UpdatedAt = DateTime.UtcNow;

    protected void SetUpdateAt(DateTime? updatedAt)
        => UpdatedAt = updatedAt;

    public void SetEventId(Guid eventId)
    {
        EventId = EventId;
    }
}

public enum TransactinoStatus {
    Pending,
    PaidOut,
    Error
}