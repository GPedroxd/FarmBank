using FarmBank.Application.Base;

namespace FarmBank.Application.Models;

public class Transaction : BaseEntity
{
    public string UserPhoneNumber { get; set; }
    public string UserEmail { get; set; }
    public string UserName { get;set; }
    public string TransactionId { get; set; }
    public double Ammount { get; set; }
    public TransactinoStatus Status { get; set; }
    public string PixCopyPaste { get; set; }
    public string QRCode { get; set; }
    public DateTime ExpirationDate{ get; set; }
    public DateTime? PaymentDate { get; set; }
    public string PayerId { get; set; }
}

public enum TransactinoStatus {
    Pending,
    PaidOut,
    TimeOut
}