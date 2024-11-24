using FarmBank.Application.Transaction.Commands.NewPayment;
using FarmBank.Core.Transaction;
using FarmBank.Core.Transaction.Enums;

namespace FarmBank.Application.Payment;

public class PaymentCreated
{
    public PaymentCreated(Guid eventId, string transactionId, string email, string phoneNamber, decimal amount, decimal discountPercentage, string? pixQrCode, string? pixBase64, DateTime expirationDate)
    {
        EventId = eventId;
        Email = email;
        PhoneNamber = phoneNamber;
        TransactionId = transactionId;
        Amount = amount;
        DiscountPercentage = discountPercentage;
        PixQrCode = pixQrCode;
        PixBase64 = pixBase64;
        ExpirationDate = expirationDate;
    }
    public Guid EventId { get; init; }
    public string TransactionId { get; init; }
    public string Email { get; init; }
    public string PhoneNamber { get; init; }
    public decimal Amount { get; init; }
    public decimal DiscountPercentage { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
    public string? PixQrCode { get; init; }
    public string? PixBase64 { get; init; }
    public DateTime ExpirationDate { get; init; }

    public QRCode GetQrInformation()
    {
        return new QRCode()
        {
            PixBase64 = PixQrCode,
            PixCopyPaste = PixBase64,
            Amount = Amount,
            ExpirationDate = ExpirationDate
        };
    }

    public static implicit operator Core.Transaction.Transaction(PaymentCreated input)
    {
        return new(
            input.EventId, 
            input.PhoneNamber, 
            input.Email, 
            input.TransactionId, 
            input.Amount, 
            input.PaymentMethod, 
            input.ExpirationDate
            );
    }
}
