namespace FarmBank.Application.Payment;

public class PaymentCreated
{
    public PaymentCreated(string transactionId, string email, string phoneNamber, decimal amount, decimal discountPercentage, string? pixQrCode, string? pixBase64, DateTime expirationDate)
    {
        Email = email;
        PhoneNamber = phoneNamber;
        TransactionId = transactionId;
        Amount = amount;
        DiscountPercentage = discountPercentage;
        PixQrCode = pixQrCode;
        PixBase64 = pixBase64;
        ExpirationDate = expirationDate;
    }

    public string TransactionId { get; set; }
    public string Email { get; set; }
    public string PhoneNamber { get; set; }
    public decimal Amount { get; set; }
    public decimal DiscountPercentage { get; set; }
    public string? PixQrCode { get; set; }
    public string? PixBase64 { get; set; }
    public DateTime ExpirationDate { get; set; }
}
