

using FarmBank.Application.Models;

namespace FarmBank.Application.Dto;

public struct QRCode
{
    private QRCode(Transaction transaction)
    {
        PixCopyPaste = transaction.PixCopyPaste;
        PixBase64 = transaction.QRCode;
        ExpirationDate = transaction.ExpirationDate;
        Amount = transaction.Amount;
    }
    public string PixCopyPaste { get; init; }
    public string PixBase64 { get; init; }
    public DateTime ExpirationDate { get; init; }
    public decimal Amount { get; init; }

    public static implicit operator QRCode(Transaction transaction)
        => new QRCode(transaction);
}
