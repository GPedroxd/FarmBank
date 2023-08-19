using FarmBank.Application.Base;
using FarmBank.Application.Commands.UpdateTransaction;

namespace FarmBank.Application.Models;

public class Transaction : BaseEntity
{
    public Transaction(
        string userPhoneNumber, 
        string userEmail, 
        string userName, 
        string transactionId, 
        double ammount, 
        string pixCopyPaste, 
        string qRCode,
        DateTime expirationDate)
    {
        UserPhoneNumber = userPhoneNumber;
        UserEmail = userEmail;
        UserName = userName;
        TransactionId = transactionId;
        Ammount = ammount;
        Status = TransactinoStatus.Pending;
        PixCopyPaste = pixCopyPaste;
        QRCode = qRCode;
        
        ExpirationDate = expirationDate;
    }

    public Transaction(
        string userPhoneNumber, 
        string userEmail, 
        string userName, 
        string transactionId, 
        double ammount, 
        TransactinoStatus status, 
        string pixCopyPaste, 
        string qRCode, 
        DateTime expirationDate, 
        DateTime? paymentDate, 
        string payerId) :
         this(userPhoneNumber,
          userEmail, 
          userName, 
          transactionId, 
          ammount, 
          pixCopyPaste, 
          qRCode, 
          expirationDate)
    {
        Status = status;
        PaymentDate = paymentDate;
        PayerId = payerId;
    }

    public string UserPhoneNumber { get; init; }
    public string UserEmail { get; init; }
    public string UserName { get; init; }
    public string TransactionId { get; init; }
    public double Ammount { get; init; }
    public TransactinoStatus Status { get; private set; }
    public string PixCopyPaste { get; init; }
    public string QRCode { get; init; }
    public DateTime ExpirationDate{ get; set; }
    public DateTime? PaymentDate { get; private set; }
    public string PayerId { get; private set; }

    public void SetAsPaidOut(UpdateTransactionCommand update)
    {
        PaymentDate = DateTime.UtcNow;
        PayerId = update.UserId;
        Status = TransactinoStatus.PaidOut;
        SetUpdateAt();
    }
}

public enum TransactinoStatus {
    Pending,
    PaidOut,
    TimeOut
}